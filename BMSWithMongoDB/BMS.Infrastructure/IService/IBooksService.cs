using BMS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Infrastructure.IService
{
    public interface IBooksService
    {
        Task<List<Book>> GetAsync();
        Task<Book> GetAsync(string id);
        Task CreateBook(Book newBook);
        Task UpdateBook(Book updateBook);
        Task DeleteBook(string id);
    }
}
