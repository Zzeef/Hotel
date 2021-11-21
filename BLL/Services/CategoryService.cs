using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DLL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork Database { get; set; }

        public CategoryService(IUnitOfWork uow) 
        {
            Database = uow;
        }

        public OperationDetails AddCategory(CategoryDTO item)
        {        
            if (ExistCategory(item)) 
            {
                return new OperationDetails(false, "Данная категория существует");
            }

            Category category = new Category()
            {
                Id = item.Id,
                Name = item.Name,
                Bed = item.Bed,
                Price = item.Price
            };
            Database.Categories.Create(category);
            Database.Save();

            return new OperationDetails(true, "Категория создана");
        }

        public OperationDetails DeleteCategory(Guid id)
        {
            Category category = Database.Categories.Get(id);
            if (category == null)
            {
                return new OperationDetails(false, "Данной категории не существует");
            }

            Database.Categories.Delete(id);
            Database.Save();

            return new OperationDetails(true, "Категория удалена");
        }

        public async Task<CategoryDTO> FindCategoryByIdAsync(Guid id)
        {
            Category category = await Task.Run(() => Database.Categories.Get(id));

            CategoryDTO categoryDTO = new CategoryDTO() 
            {
                Id = category.Id,
                Name = category.Name,
                Bed = category.Bed,
                Price = category.Price
            };

            return categoryDTO;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public bool ExistCategory(CategoryDTO item) 
        {
            Category category = Database.Categories.Get(item.Id);
            if (category != null)
                return false;
            var list = Database.Categories.GetAll().Where(x => x.Name == item.Name && x.Bed == item.Bed).ToList();
            if (list != null)
                return false;
            return true;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
