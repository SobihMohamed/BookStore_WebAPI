using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public Customer? Customer { get; set; }
    }
}