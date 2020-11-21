using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    public class UserReaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
       
        public int UserID { get; set; }

        public int TemplateID { get; set; }
        public User User { get; set; }
        public Template Template { get; set; }
        public bool Favourite { get; set; }
        public bool Download { get; set; }
    }
}
