using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        [Required]
        public string  UserName { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string  Email { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimun 6 characters required")]

        public string Password { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public int Coins { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<UserReaction> UserReactions { get; set; }
      //  public ICollection<UserDownload> UserDownloads { get; set; }
    }
}
