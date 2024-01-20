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

        public virtual IEnumerable<Organization> Organizations { get; set; }

        internal Domain.User ToDomain()
        {
            return new Domain.User
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
