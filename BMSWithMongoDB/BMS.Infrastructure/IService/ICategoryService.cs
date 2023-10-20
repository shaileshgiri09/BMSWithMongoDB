using BMS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Infrastructure.IService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAsync();
        Task<Category> GetAsync(string id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(string id);
    }
}
