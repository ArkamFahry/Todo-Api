using ProjectX.Api.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProjectX.Api.Repositories
{
    public class MongoDBTodoItemRepository : IInMemTodoItemRepository
    {
        private const string dataBaseName = "ProjectXDataBase";

        private const string collectionName = "ProjectXItems";

        private readonly IMongoCollection<TodoItem> todoItemsCollection;

        private readonly FilterDefinitionBuilder<TodoItem> filterDefinitionBuilder = Builders<TodoItem>.Filter;
        
        private readonly IndexKeysDefinitionBuilder<TodoItem> filterIndexDefinitionBuilder = Builders<TodoItem>.IndexKeys;

        public MongoDBTodoItemRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);
            todoItemsCollection = database.GetCollection<TodoItem>(collectionName);
        }

        public async Task CreateTodoItemAsync(TodoItem todoItem)
        {
            await todoItemsCollection.InsertOneAsync(todoItem);
        }

        public async Task DeleteTodoItemAsync(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            await todoItemsCollection.DeleteOneAsync(filter);
        }

        public async Task<TodoItem> GetTodoItem(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return await todoItemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await todoItemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            var filter = filterDefinitionBuilder.Eq(existingitem => existingitem.Id, todoItem.Id);
            await todoItemsCollection.ReplaceOneAsync(filter, todoItem);
        }
        
        public async Task<IEnumerable<TodoItem>> GetTodoSearchItemsAsync(String s)
        {
            var keys = filterIndexDefinitionBuilder.Text("Name");
            todoItemsCollection.Indexes.CreateOneAsync(keys);
            var filter = filterDefinitionBuilder.Text(s);
            return await todoItemsCollection.Find(filter).ToListAsync();
        }
    }
}