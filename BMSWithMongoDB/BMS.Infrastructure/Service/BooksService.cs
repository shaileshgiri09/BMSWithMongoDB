using BMS.Domain.Model;
using BMS.Infrastructure.IService;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Service
{
    public class BooksService : IBooksService
    {
        private readonly IMongoCollection<Book> _bookCollection;
        public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _bookCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
        }
        public async Task CreateBook(Book newBook)
        {
            await _bookCollection.InsertOneAsync(newBook);
        }

        public async Task DeleteBook(string id)
        {
            await _bookCollection.DeleteOneAsync(id);
        }

        public Task<List<Book>> GetAsync()
        {
            return _bookCollection.Find(_=> true).ToListAsync();
        }

        public Task<Book> GetAsync(string id)
        {
            return _bookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateBook(Book updateBook)
        {
            return _bookCollection.ReplaceOneAsync(x => x.Id == updateBook.Id, updateBook);
        }
    }
}
