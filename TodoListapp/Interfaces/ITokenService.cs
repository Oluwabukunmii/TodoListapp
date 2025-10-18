using Microsoft.AspNetCore.Identity;

namespace TodoListapp.Interfaces
{
    public interface ITokenService 
    {
        string CreateJWTToken(IdentityUser user);


    }
}
