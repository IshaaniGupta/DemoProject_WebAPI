using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Domain.Model
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [Required]
        public Guid UserId { get; set; } // Foreign key

        [ForeignKey("UserId")]
        public virtual UserLogin UserLogins { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
