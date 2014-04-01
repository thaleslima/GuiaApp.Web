using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaApp.Model
{
    public class Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Image")]
        public string PathImage { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("Menu")]
        [Display(Name = "Menu")]
        public int IdMenu { get; set; }

        [ForeignKey("City")]
        [Display(Name = "City")]
        public int IdCity { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual City City { get; set; }
    }
}
