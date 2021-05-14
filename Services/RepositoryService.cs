using Cars_and_Manufacturers.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IDataReaderService _dataReaderService;
        public Dictionary<Guid, Car> _cars = null;
        public Dictionary<string, ManuFacturer> _manufacturers = null;

        //init
        public Dictionary<string, User> _users = new();
        public HashSet<UserCar> _usersCars = new();

        public RepositoryService(IDataReaderService dataReaderService)
        {
            _dataReaderService = dataReaderService;
        }


        // Operations for Car
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            await CheckForCarsExist();
            return _cars.Values.ToList();
        }
        public async Task<Car> GetCarById(Guid id)
        {
            await CheckForCarsExist();
            return _cars[id];
        }


        // Operations for ManuFacturer
        public async Task<IEnumerable<ManuFacturer>> GetAllManuFacturers()
        {
            await CheckForManufacturersExist();
            return _manufacturers.Values.ToList();
        }
        public async Task<ManuFacturer> GetManuFacturerByName(string name)
        {
            await CheckForManufacturersExist();
            return _manufacturers[name];
        }



        // CRUD operations for User
        public Task<List<User>> GetAllUsers()
        {
            return Task.FromResult(_users.Values.ToList());

        }
        public Task<User> GetUserByUsername(string username)
        {
            return Task.FromResult(_users[username]);
        }
        public Task<User> AddUser(User user)
        {
            if (_users.ContainsKey(user.UserName))
                throw new ArgumentException("The username: '" + user.UserName + "' is already exists.");

            _users.Add(user.UserName, user);
            return Task.FromResult(_users[user.UserName]);
        }
        public Task<User> ModifyUser(User user)
        {
            if (!_users.ContainsKey(user.UserName))
                throw new ArgumentException("The username: '" + user.UserName + "' isn't exists.");

            _users[user.UserName] = user;
            return Task.FromResult(_users[user.UserName]);
        }
        public Task DeleteUser(string username)
        {
            if (!_users.ContainsKey(username))
                throw new ArgumentException("The username: '" + username + "' isn't exists.");

            //better way to delete 
            _usersCars.RemoveWhere(item => item.Username == username);

            _users.Remove(username);
            return Task.CompletedTask;
        }



        // CRUD operations for UserCar 
        public Task<List<UserCar>> GetAllUsersCars()
        {
            return Task.FromResult(_usersCars.ToList());
        }
        public async Task<IEnumerable<Car>> GetAllCarsOfUser(string username)
        {
            await CheckForCarsExist();
            if (!_users.ContainsKey(username))
                    throw new ArgumentException($"The user: {username}, isn't exist.");

            return _usersCars.Where(cur => cur.Username == username)
                             .Select(userCar => _cars[userCar.CarId]);


        }
        public async Task<IEnumerable<UserCar>> GetAllUsersOfCar(Guid id)
        {
            await CheckForCarsExist();
            if (!_cars.ContainsKey(id))
                throw new ArgumentException($"The carId: {id}, isn't exist.");

            return _usersCars.Where(cur => cur.CarId == id);
        }
        public async Task<UserCar> AddUserCar(UserCar userCar)
        {
            await CheckForCarsExist();

            if (userCar.CarId == Guid.Empty)
                throw new ArgumentException("Wrong CarId Format - should be 'Guid' type");
            if (!_cars.ContainsKey(userCar.CarId))
                throw new ArgumentException($"The car: {userCar.CarId}, isn't exist.");
            if (!_users.ContainsKey(userCar.Username))
                throw new ArgumentException($"The user: {userCar.Username}, isn't exist.");
            if (_usersCars.Contains(userCar))
                throw new ArgumentException($"The item UserCar:({userCar.Username},{userCar.CarId}) already exist");

            _usersCars.Add(userCar);
            return userCar;
        }
        public async Task RemoveUserCar(UserCar userCar)
        {
            await CheckForCarsExist();

            if (!_cars.ContainsKey(userCar.CarId))
                throw new ArgumentException($"The car: {userCar.CarId}, isn't exist.");
            
            if (!_users.ContainsKey(userCar.Username))
                throw new ArgumentException($"The user: {userCar.Username}, isn't exist.");
            
            if (!_usersCars.Contains(userCar))
                throw new ArgumentException($"The item ({userCar.Username},{userCar.CarId}"
                                          + $") isn't exist.");
            
            _usersCars.Remove(userCar);
        }


        //take data for the first time the service need
        private async Task CheckForCarsExist()
        {
            if (_cars == null)
                _cars = (await _dataReaderService.GetAllCars())
                                                 .ToDictionary(c => c.Id);
        }
        private async Task CheckForManufacturersExist()
        {
            if (_manufacturers == null)
                _manufacturers = (await _dataReaderService.GetAllManufacturers())
                                                          .ToDictionary(m => m.Name);
        }
    }
}
