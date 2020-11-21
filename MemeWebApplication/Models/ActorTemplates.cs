using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    public class ActorTemplates
    {
        [Key]
        public int ID { get; set; }
        /* [ForeignKey("Template")]
          public Template Template { get; set; }*/
        public int TemplateID { get; set; }
        public int ActorID { get; set; }
        /*     [ForeignKey ("Actor")]
            public Actor   Actor { get; set; }
         */
    }
}
