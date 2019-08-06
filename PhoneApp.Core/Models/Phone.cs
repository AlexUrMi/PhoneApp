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
        [Required(ErrorMessage ="Введите название")]
        [StringLength(maximumLength:100, MinimumLength =2, ErrorMessage ="Введите название от 2 до 100 символов длинной")]
        public string Name { get; set; }

        [Display(Name="Цена")]
        [Required(ErrorMessage ="Введите цену")]
        [Range(minimum:0, maximum:1000000, ErrorMessage ="Введите цену в виде не отрицательного числа")]
        public decimal Price { get; set; }

        [Display(Name="Компания")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
