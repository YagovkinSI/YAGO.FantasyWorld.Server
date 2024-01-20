using Microsoft.AspNetCore.Identity;
using System;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    public class User : IdentityUser
    {
        public DateTimeOffset Registration { get; set; }
        public DateTimeOffset LastActivity { get; set; }

        internal Domain.User ToDomain()
        {
            return new Domain.User
            (
                Id,
                UserName,
                Registration,
                LastActivity,
                null
            );
        }
    }
}
