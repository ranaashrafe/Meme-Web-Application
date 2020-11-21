using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    
    public class Template
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string FilmName { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Phrase> TempPhrases { get; set; }
        public virtual ICollection<ActorTemplates> ActorTemp { get; set; }

        public ICollection<UserReaction> UserReactions { get; set; }


    }
}
