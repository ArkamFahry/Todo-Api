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

        public async Task<TodoItem> GetTodoItemAsync(Guid id)
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
    }
}