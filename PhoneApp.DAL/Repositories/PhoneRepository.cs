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


}
