namespace PhoneApp.Models
{
    public interface ICompanyAsyncRepository : IGenericAsyncRepository<Company>
    {

    }
    public class CompanyAsyncRepository : GenericAsyncRepository<Company>, ICompanyAsyncRepository
    {
        public CompanyAsyncRepository(AppDbContext context) : base(context)
        {
        }
    }
}
