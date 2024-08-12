using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.S3;
using EasyPassportImage.Interfaces;
using EasyPassportImage.Models;
using EasyPassportImage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var awsConfig = builder.Configuration.GetSection("AwsConfig").Get<AwsConfig>();
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var config = new AmazonDynamoDBConfig
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(awsConfig.Region)
    };
    var credentials = new BasicAWSCredentials(awsConfig.AccessKey, awsConfig.SecretKey);
    return new AmazonDynamoDBClient(credentials, config);
});

// S3 config
builder.Services.AddSingleton<IAmazonS3>(s =>
{
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(awsConfig.Region)
    };
    var credentials = new BasicAWSCredentials(awsConfig.AccessKey, awsConfig.SecretKey);
    return new AmazonS3Client(credentials, config);
});

builder.Services.AddScoped<DynamoDBContext>();
builder.Services.AddScoped(typeof(IDynamoDbService<>), typeof(DynamoDbService<>));
builder.Services.AddTransient<IPassportOrderService, PassportOrderService>();
builder.Services.AddTransient<IS3BucketService, S3BucketService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();