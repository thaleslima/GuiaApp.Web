using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaApp.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Rating { get; set; }
        public String Desciption { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [ForeignKey("Local")]
        public int IdLocal { get; set; }

        public bool Active { get; set; }

        public virtual User User { get; set; }
        public virtual Local Local { get; set; }
    }
}
