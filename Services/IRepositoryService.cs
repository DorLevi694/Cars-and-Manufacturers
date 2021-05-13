using Cars_and_Manufacturers.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public interface IRepositoryService
    {
        Task<User> AddUser(User user);
        Task<UserCar> AddUserCar(UserCar userCar);
        Task DeleteUser(string username);
        Task<IEnumerable<Car>> GetAllCars();
        Task<IEnumerable<Car>> GetAllCarsOfUser(string userName);
        Task<IEnumerable<ManuFacturer>> GetAllManuFaturers();
        Task<List<User>> GetAllUsers();
        Task<List<UserCar>> GetAllUserCars();
        Task<IEnumerable<UserCar>> GetAllUsersOfCar(Guid id);
        Task<Car> GetCarById(Guid id);
        Task<ManuFacturer> GetManuFacturerByName(string name);
        Task<User> GetUserByUsername(string username);
        Task<User> ModifyUser(User user);
        Task RemoveUserCar(UserCar userCar);


    }
}