using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Markplace.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    //Propriedade de navegação
    public Cliente? Cliente { get; private set; }
    public Vendedor? Vendedor { get; private set; }
}
