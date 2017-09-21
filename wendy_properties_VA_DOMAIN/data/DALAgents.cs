using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_DOMAIN.data {
    public static class DALAgents {

        static readonly string wpConnectDB =
            ConfigurationManager
            .ConnectionStrings["wpConnectDB"]
            .ConnectionString;

        // Create an agent
        public static void AddAgent(BLAgent agent) {

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand("AddAgentBE", sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };


                sqlComm.Parameters.Add(new[] {
                        new SqlParameter("Avatar", agent.Avatar),
                        new SqlParameter("FirstName", agent.FirstName),
                        new SqlParameter("LastName", agent.LastName),
                        new SqlParameter("Cellphone", agent.Cellphone),
                        new SqlParameter("Email", agent.Email)
                        });

                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
