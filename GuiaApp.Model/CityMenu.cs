using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaApp.Model
{
    public class CityMenu
    {
        [Key, Column(Order = 0), ForeignKey("City")]
        public int IdCity { get; set; }

        [Key, Column(Order = 1), ForeignKey("Menu")]
        public int IdMenu { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }

        public virtual City City { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
