using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using EasyPassportImage.Interfaces;
using EasyPassportImage.Models;

namespace EasyPassportImage.Services;

public class DynamoDbService<T> : IDynamoDbService<T> where T :class ,new()
{
    private readonly IAmazonDynamoDB _context;

    public DynamoDbService(IAmazonDynamoDB context)
    {
        _context = context;
    }

    public async Task<T> GetItemAsync(string id)
    {
        
        return await Task.FromResult(new T());
    }

    public async Task<bool> SaveItemAsync(Dictionary<string,AttributeValue> items,string table)
    {
        try
        {
            var tableName = table;
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = items
            };
            var response = await _context.PutItemAsync(request);
            if (response != null && response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
        catch(Exception ex)
        {
            return await Task.FromResult(false);

        }
    }
}
