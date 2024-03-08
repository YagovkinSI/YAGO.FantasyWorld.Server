using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    public class User : IdentityUser
    {
        public DateTimeOffset Registration { get; set; }
        public DateTimeOffset LastActivity { get; set; }

        public virtual List<Organization> Organizations { get; set; }

        internal Yago.FantasyWorld.ApiContracts.Domain.User ToDomain()
        {
            return new Yago.FantasyWorld.ApiContracts.Domain.User
            (
                Id,
                UserName,
                Registration,
                LastActivity,
                Organizations.SingleOrDefault()?.Id
            );
        }
    }
}
