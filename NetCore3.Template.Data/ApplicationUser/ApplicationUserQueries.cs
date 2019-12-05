using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace NetCore3.Template.Data.ApplicationUser
{
    public static class ApplicationUserQueries
    {
        public static List<Domain.Entities.ApplicationUser> GetApplicationUsers()
        {
            var strConnectionString = @"data source=(localdb)\MSSQLLocalDb; Database=NetCore3Template; Integrated Security = SSPI;";
            var applicationUsers = new List<Domain.Entities.ApplicationUser>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                applicationUsers = con.Query<Domain.Entities.ApplicationUser>("SELECT * FROM ApplicationUser").ToList();
            }

            return applicationUsers;
        }
    }
}
