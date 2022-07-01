using LibraryProject.Controllers.Requests;
using LibraryProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryProject.Data;

public interface ILibraryDataStore
{
    Task<User> GetUser(long userId);
    Task Insert(User request);
    Task Update(User user);
    Task Delete(long userId);
}

public class LibraryDataStore : ILibraryDataStore
{
    private readonly IMongoCollection<User> _collection;

    public LibraryDataStore(IMongoClient mongoClient)
    {
        _collection = mongoClient.GetDatabase("local").GetCollection<User>("Users");
    }

    public async Task<User> GetUser(long userId)
    {
        return await _collection
            .Find(t => t.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task Insert(User request)
    {
        await _collection.InsertOneAsync(request);
    }

    public async Task Delete(long userId)
    {
        await _collection
            .DeleteOneAsync(t => t.UserId == userId);
    }

    public async Task Update(User user)
    {
        await _collection.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}