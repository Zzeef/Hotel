using System;
using DLL.Entities;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories(); 
        OperationDetails AddCategory(CategoryDTO item);
        OperationDetails DeleteCategory(Guid id);
        Task<CategoryDTO> FindCategoryByIdAsync(Guid id);
        bool ExistCategory(CategoryDTO item);
        void Dispose();
    }
}
    