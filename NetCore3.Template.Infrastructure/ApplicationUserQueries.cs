using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore3.Template.Infrastructure
{
    public static class ApplicationUserQueries
    {
        public static List<Domain.Entities.ApplicationUser> GetApplicationUsers()
        {
            return Data.ApplicationUser.ApplicationUserQueries.GetApplicationUsers();
        }
    }
}
