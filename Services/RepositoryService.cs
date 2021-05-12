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
        public List<UserCar> _usersCars = new();

        public RepositoryService(IDataReaderService dataReaderService)
        {
            _dataReaderService = dataReaderService;
        }


        // Operations for Car
        public async Task<List<Car>> GetAllCars()
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
        public async Task<List<ManuFacturer>> GetAllManuFaturers()
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
                throw new Exception("The username: '" + user.UserName + "' is already exists.");

            _users.Add(user.UserName, user);
            return Task.FromResult(_users[user.UserName]);
        }
        public Task<User> ModifyUser(User user)
        {
            if (!_users.ContainsKey(user.UserName))
                throw new Exception(user.UserName + " isn't exist.");

            _users[user.UserName] = user;
            return Task.FromResult(_users[user.UserName]);
        }
        public Task DeleteUser(string username)
        {
            if (!_users.ContainsKey(username))
                throw new Exception(username + " isn't exist.");

            // remove user from all is cars
            for (int i = _usersCars.Count - 1; i >= 0; i--)
                if (_usersCars[i].Username == username)
                    _usersCars.RemoveAt(i);

            _users.Remove(username);
            return Task.CompletedTask;
        }



        // CRUD operations for UserCar 
        public Task<List<UserCar>> GetAllUsersCars()
        {
            return Task.FromResult(_usersCars.ToList());
        }
        public Task<List<UserCar>> GetAllCarsOfUser(string userName)
        {
            var retVal = _usersCars.Where(cur => cur.Username == userName).ToList();
            return Task.FromResult(retVal);
        }
        public Task<List<UserCar>> GetAllUsersOfCar(Guid id)
        {
            var retVal = _usersCars.Where(cur => cur.CarId == id).ToList();
            return Task.FromResult(retVal);
        }
        public Task<UserCar> AddUserCar(UserCar userCar)
        {

            _cars.GetValueOrDefault(userCar.CarId);

            _users.GetValueOrDefault(userCar.Username);

            _usersCars.Add(userCar);

            return Task.FromResult(userCar);
        }
        public Task RemoveUserCar(UserCar userCar)
        {
            _usersCars.Remove(userCar);
            return Task.CompletedTask;

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
