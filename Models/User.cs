using System.ComponentModel.DataAnnotations;

namespace dotnetRedis.Models
{
    public class User
    {   
        [Required]
        public string Id { get; set; } = $"user:{Guid.NewGuid().ToString()}";

        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
