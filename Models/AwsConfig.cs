using Amazon.DynamoDBv2.DataModel;

namespace EasyPassportImage.Models;

public class AwsConfig
{
    public string Region { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
}
