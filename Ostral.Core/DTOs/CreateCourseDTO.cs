using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ostral.Core.DTOs
{
    public class CreateCourseDTO
    {
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
