namespace EasyPassportImage.Interfaces
{
    public interface IS3BucketService
    {
        public Task<string> UploadImageAsync(IFormFile image);
    }
}