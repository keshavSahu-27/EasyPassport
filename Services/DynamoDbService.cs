using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using EasyPassportImage.Interfaces;
using EasyPassportImage.Models;

namespace EasyPassportImage.Services;

public class DynamoDbService<T> : IDynamoDbService<T> where T : class, new()
{
    private readonly DynamoDBContext _context;

    public DynamoDbService(DynamoDBContext context)
    {
        _context = context;
    }

    public async Task<T> GetItemAsync(string id)
    {
        return await Task.FromResult(new T());
    }

    public async Task<bool> SaveItemAsync(T item)
    {
        return await Task.FromResult(true);
    }
}
