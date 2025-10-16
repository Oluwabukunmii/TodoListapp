using Microsoft.AspNetCore.Identity;

namespace TodoListapp.Interfaces
{
    public interface ITokenRepository 
    {
        string CreateJWTToken(IdentityUser user);


    }
}
