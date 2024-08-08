using Amazon.DynamoDBv2.DataModel;

namespace EasyPassportImage.Models;

[DynamoDBTable("CapacityTableName")]
public class Capacity
{
    [DynamoDBHashKey]
    public string DeliveryDate { get; set; }

    [DynamoDBProperty]
    public string Id { get; set; }

    [DynamoDBProperty]
    public int Count { get; set; }
}
