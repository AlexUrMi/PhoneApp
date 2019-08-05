using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApp.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name="Цена")]
        public decimal Price { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
