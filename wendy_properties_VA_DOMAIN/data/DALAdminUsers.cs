using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_DOMAIN.data {
    public static class DALAdminUsers {

        static readonly string wpConnectDB =
           ConfigurationManager
           .ConnectionStrings["wpConnectDB"]
           .ConnectionString;

        public static void AddAdminUser(BLAdminUser adminUser) {

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand("AddAdminUserBE", sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                sqlComm.Parameters.Add(new[]
                {
                    new SqlParameter("Username", adminUser.Username),
                    new SqlParameter("Password", adminUser.Password)
                });

                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
