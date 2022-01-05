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

        public void CreateTodoItem(TodoItem todoItem)
        {
            todoItemsCollection.InsertOne(todoItem);
        }

        public void DeleteTodoItem(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            todoItemsCollection.DeleteOne(filter);
        }

        public TodoItem GetTodoItem(Guid id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, id);
            return todoItemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return todoItemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            var filter = filterDefinitionBuilder.Eq(existingitem => existingitem.Id, todoItem.Id);
            todoItemsCollection.ReplaceOne(filter, todoItem);
        }
    }
}