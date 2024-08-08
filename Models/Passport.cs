using System.ComponentModel.DataAnnotations;

namespace EasyPassportImage.Models;

public class Passport
{
    [Required]
    public string CustomerName { get; set; }

    [Required]
    public IFormFile Image { get; set; }
}
