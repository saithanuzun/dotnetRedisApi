using System.ComponentModel.DataAnnotations;

namespace dotnetRedis.Models
{
    public class User
    {   
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Number { get; set; } = string.Empty;

    }
}
