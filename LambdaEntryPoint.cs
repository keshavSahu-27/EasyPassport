using Amazon.Lambda.AspNetCoreServer;

namespace EasyPassportImage;
public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseLambdaServer();
    }
}
