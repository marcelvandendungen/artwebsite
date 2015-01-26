
namespace Core.Model
{
    public interface User
    {
        string Email { get; set; }
        string Nickname { get; set; }
        string FullName { get; set; }
        bool IsSignedByProvider { get; set; }
        string ClaimedIdentifier { get; set; }
    }
}
