using System.Collections.Generic;

namespace PhoneApp.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Phone> Phones { get; set; }
        public Company()
        {
            Phones = new List<Phone>();
        }
    }
}
