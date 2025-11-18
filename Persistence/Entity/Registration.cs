using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSecurity.Persistence.entity
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }


        [EmailAddress(ErrorMessage = "Please enter a valid E-mail address.")] //validate the input as an Email Address
        public string Email { get; set; }


        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public string PhoneNumber { get; set; }

        [ForeignKey("role")]
        public int roleId { get; set; }
        public RoleEntity role { get; set; }
    }
}
