﻿using Cars_and_Manufacturers.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_and_Manufacturers.Services
{
    public interface ICurrentUserService2
    {
        bool DataIsUpdate { get; }

        Task<List<Car>> getAllCarsOfCurrentUserAsync();
        Task<User> getCurrentUserAsync();
        Task setCurrentUserName(string userName);
    }
}