
namespace Core.Interface
{
    public interface IAuthorizedUserManager
    {
        bool IsAllowedUser(string email);
    }
}