using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneApp.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Display(Name="Название компании")]
        public string Name { get; set; }

        public List<Phone> Phones { get; set; }
        public Company()
        {
            Phones = new List<Phone>();
        }
    }
}
