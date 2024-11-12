using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IDP.Models;

public class ConfigIdentityServer
{
    public static List<TestUser> GetUsers()
    {
        return new List<TestUser>
        {
            new IdentityServer4.Test.TestUser
            {
                SubjectId = "1",
                Username = "MohamadMahdi_Tvk",
                IsActive = true,
                Password = "12345",
                Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "mhtvk@gmail.com"),
                    new Claim(ClaimTypes.MobilePhone, "09102279344"),
                    new Claim("name", "MohamadMahdi"),
                    new Claim("website", "https://google.com")
                }
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Phone(),
        };
    }

    public static List<Client> GetClients()
    {
        return new List<Client>
        {
            new Client {

                        ClientId = "mthvclient",
                        ClientSecrets = new List<Secret>{new Secret("1234".Sha256())},
                        AllowedGrantTypes = GrantTypes.Implicit,
                        RedirectUris = {"https://localhost:7284/signin-oidc"},
                        PostLogoutRedirectUris = {"https://localhost:7284/signout-callback-oidc"},
                        AllowedScopes = new List<string>
                        {
                            IdentityModel.OidcConstants.StandardScopes.OpenId,
                            IdentityModel.OidcConstants.StandardScopes.Profile,
                            IdentityModel.OidcConstants.StandardScopes.Email,
                            IdentityModel.OidcConstants.StandardScopes.Phone,
                        },

                        RequireConsent = true

                    },

            new Client
            {
                ClientId = "ApiHavaShenasi",
                ClientSecrets = new List<Secret> {new Secret("12345".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new []{ "ApiHava" }

            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>()
        {
            new ApiScope("ApiHava","سرویس هواشناسی")
        };
    }

    public static List<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("ApiHava","سرویس هواشناسی")
        };
    }
}
