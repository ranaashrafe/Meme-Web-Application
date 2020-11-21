using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    public class Phrase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Text { get; set; }

        [ForeignKey("Template")]
       
        public  int? TemplateID { get; set; }
        public virtual Template Template { get; set; }

    }
}
