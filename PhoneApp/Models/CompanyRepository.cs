using Microsoft.EntityFrameworkCore;

namespace PhoneApp.Models
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {

    }
    public class CompanyRepository : EFGenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
