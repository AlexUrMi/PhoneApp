namespace PhoneApp.Models
{
    public interface IPhoneRepository : IGenericRepository<Phone>
    {

    }

    public class PhoneRepository :  EFGenericRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(AppDbContext context) : base(context)
        {
        }
    }

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
