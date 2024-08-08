namespace EasyPassportImage.Interfaces;

public interface IDynamoDbService<T> where T : class
{
    Task<bool> SaveItemAsync(T item);
    Task<T> GetItemAsync(string id);
}