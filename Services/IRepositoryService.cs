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
        Task<List<Car>> GetAllCars();
        Task<List<UserCar>> GetAllCarsOfUser(string userName);
        Task<List<ManuFacturer>> GetAllManuFaturers();
        Task<List<User>> GetAllUsers();
        Task<List<UserCar>> GetAllUsersCars();
        Task<List<UserCar>> GetAllUsersOfCar(Guid id);
        Task<Car> GetCarById(Guid id);
        Task<ManuFacturer> GetManuFacturerByName(string name);
        Task<User> GetUserByUsername(string username);
        Task<User> ModifyUser(User user);
        Task RemoveUserCar(UserCar userCar);
    }
}