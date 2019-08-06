namespace PhoneApp.Models
{
    public interface IPhoneAsyncRepository : IGenericAsyncRepository<Phone>
    {

    }
    public class PhoneAsyncRepository : GenericAsyncRepository<Phone>, IPhoneAsyncRepository
    {
        public PhoneAsyncRepository(AppDbContext context) : base(context)
        {
        }
    }
}
