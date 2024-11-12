using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Controllers;

public class ClientsController : Controller
{
    private readonly ConfigurationDbContext _configuration;
    public ClientsController(ConfigurationDbContext configuration)
    {
        _configuration = configuration;
    }
    public IActionResult Index()
    {
        var clients = _configuration.Clients.ToList();
        return View(clients);
    }

    public IActionResult Add()
    {
        _configuration.Clients.Add(new IdentityServer4.EntityFramework.Entities.Client
        {
            ClientId = "ApiHavaShenasi",
            ClientSecrets = new List<ClientSecret>()
                 {
                      new ClientSecret
                      {
                           Value = "123456".Sha256()
                      }
                 },
            AllowedGrantTypes = new List<ClientGrantType>()
                 {
                      new ClientGrantType()
                      {
                           GrantType ="ClientCredentials"
                      }
                 },
            AllowedScopes = new List<ClientScope>()
                 {
                     new ClientScope()
                     {
                          Scope="ApiHava"
                     }
                 }

        });

        _configuration.SaveChanges();

        return Ok();
    }
}
