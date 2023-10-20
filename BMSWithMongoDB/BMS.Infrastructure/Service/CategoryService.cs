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
    public class CategoryService : ICategoryService
    {
        public IMongoCollection<Category> _categoryCollection;
        public CategoryService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings) 
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var databaseName = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _categoryCollection = databaseName.GetCollection<Category>(bookStoreDatabaseSettings.Value.CategoryCollectionName);
        }

        public async Task CreateCategory(Category category)
        {
             await _categoryCollection.InsertOneAsync(category);
        }

        public async Task DeleteCategory(string id)
        {
            await _categoryCollection.DeleteOneAsync(id);
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _categoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Category> GetAsync(string id)
        {
            return await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryCollection.ReplaceOneAsync(x => x.Id == category.Id, category);
        }
    }
}
