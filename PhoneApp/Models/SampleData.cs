using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApp.Models
{
    public static class SampleData
    {
        public static void Initialize(AppDbContext dbContext)
        {
            if (!dbContext.Companies.Any())
            {
                dbContext.AddRange(
                    new Company { Name="Microsoft"},
                    new Company { Name="Apple"},
                    new Company { Name="Samsung"},
                    new Company { Name="Xiaomi"}
                    );
                dbContext.SaveChanges();
            }

            if (!dbContext.Phones.Any())
            {
                dbContext.Phones.AddRange(
                    new Phone { Name = "win phone 7", Company = dbContext.Companies.FirstOrDefault(c=>c.Name == "Microsoft"), CompanyId=1, Price=80},
                    new Phone { Name="iphone 4", Company = dbContext.Companies.FirstOrDefault(c=>c.Name== "Apple"), CompanyId=2, Price = 100},
                    new Phone { Name="Galaxy 3", Company=dbContext.Companies.FirstOrDefault(c=>c.Name== "Samsung"), CompanyId=3, Price = 90}
                    );
                dbContext.SaveChanges();
            }

            
        }
    }
}
