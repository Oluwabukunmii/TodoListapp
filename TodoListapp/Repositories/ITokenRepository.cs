using Microsoft.AspNetCore.Identity;

namespace TodoListapp.Repositories
{
    public interface ITokenRepository 
    {
        string CreateJWTToken(IdentityUser user);


    }
}
