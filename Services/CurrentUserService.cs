using Cars_and_Manufacturers.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IRepositoryService _repositoryService;

        public bool DataIsUpdate { get; private set; }
        private string CurrentUserName;
        private List<Car> CarListOfCurrentUser;
        private User currentUser;


        public CurrentUserService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
            DataIsUpdate = false;
        }
        
        public async Task<User> getCurrentUserAsync()
        {
            currentUser = await _repositoryService.GetUserByUsername(CurrentUserName);
            return currentUser;
        }
        public async Task<List<Car>> getAllCarsOfCurrentUserAsync()
        {
            CarListOfCurrentUser = (await _repositoryService.GetAllCarsOfUser(CurrentUserName)).ToList();
            return CarListOfCurrentUser.ToList();
        }
        public Task setCurrentUserName(string userName)
        {
            CurrentUserName = userName;
            DataIsUpdate = true;
            return Task.CompletedTask;
        }

        public Task<string> getCurrentUserName()
        {
            return Task.FromResult(CurrentUserName);
        }
    }
}
