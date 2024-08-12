using Amazon.S3;
using Amazon.S3.Model;
using EasyPassportImage.Interfaces;

namespace EasyPassportImage.Services
{
    public class S3BucketService : IS3BucketService
    {
        private readonly IAmazonS3 _s3Client;
        private const string BucketName = "orders-image"; //Need to move to constant

        public S3BucketService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        public async Task<string> UploadImageAsync(IFormFile image)
        {
            try
            {
                if (image.Length > 0)
                {
                    using (var stream = image.OpenReadStream())
                    {
                        var putRequest = new PutObjectRequest
                        {
                            BucketName = BucketName, // Need to move to constants
                            Key = image.FileName,
                            InputStream = stream,
                            ContentType = image.ContentType
                        };

                        var response = await _s3Client.PutObjectAsync(putRequest);
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return GeneratePreSignedURL(image.FileName);
                        }
                    }
                }
                throw new Exception("Error uploading file to S3");
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading file to S3");
            }
        }

        private string GeneratePreSignedURL(string keyName)
        {
            try
            {
                var preSignRequest = new GetPreSignedUrlRequest
                {
                    BucketName = BucketName,
                    Key = keyName,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                return _s3Client.GetPreSignedURL(preSignRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error While creating url");
            }
        }
    }
}