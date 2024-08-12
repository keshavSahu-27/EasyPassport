using Amazon.DynamoDBv2.Model;

namespace EasyPassportImage.Interfaces;

public interface IDynamoDbService<T> where T : class,new()
{
    Task<bool> SaveItemAsync(Dictionary<string, AttributeValue> items,string table);
    Task<T> GetItemAsync(string id);
}