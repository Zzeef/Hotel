﻿using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface ICategoryService
    {
        IEnumerable<Category> GetAll { get; }
        OperationDetails Add(Category item);
        OperationDetails Delete(Guid id);
        Task<Category> FindByIdAsync(Guid id);
    }
}
