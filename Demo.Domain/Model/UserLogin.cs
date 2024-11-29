using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Domain.Model
{
    public class UserLogin
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for related UserProfile
        public virtual UserProfile UserProfile { get; set; }
    }
}
