using Amazon.DynamoDBv2.DataModel;
using EasyPassportImage.Enums;

namespace EasyPassportImage.Models;

[DynamoDBTable("OrderTableName")]
public class Order
{
    [DynamoDBHashKey]
    public string Id { get; set; }

    [DynamoDBProperty]
    public string CustomerName { get; set; }

    [DynamoDBProperty]
    public DateTime OrderDate { get; set; }

    [DynamoDBProperty]
    public DateTime DeliveryDate { get; set; }

    [DynamoDBProperty]
    public string ImageUrl { get; set; }

    [DynamoDBProperty]
    public State State {get;set;}

    [DynamoDBIgnore]
    public string Status => State.ToString();
}
