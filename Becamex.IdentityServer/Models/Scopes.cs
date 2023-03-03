using IdentityServer4.Models;

namespace Becamex.IdentityServer.Models
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope(name: "customAPI.read",   displayName: "Access API Backend"),
                new ApiScope(name: "customAPI.write",   displayName: "Access API Backend")
            };
        }
    }
}
