using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.Models
{
    public class Actor

    {
        [Key]
        public int ID { get; set; }
        
        public string Name { get; set; }
       
       public virtual ICollection<ActorTemplates> ActorTemp { get; set; }

      //  public virtual Template Template { get; set; }

    }
}
