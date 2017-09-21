using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_DOMAIN.data {
    public static class DALContacts {

        static readonly string wpConnectDB =
           ConfigurationManager
           .ConnectionStrings["wpConnectDB"]
           .ConnectionString;

        public static DataTable GetAgents() {
            var dataTable = new DataTable();

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand("GetAgentsFE", sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                using (var dAdapter = new SqlDataAdapter(sqlComm)) {
                    dAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public static void AddContact(BLContact contact) {

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand("AddContactFE", sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                sqlComm.Parameters.Add(new[]{
                    new SqlParameter("FirstName", contact.FirstName),
                    new SqlParameter("LastName", contact.LastName),
                    new SqlParameter("AgentName", 
                        contact.Agent.Split('-')[0]),
                    new SqlParameter("AgentSurname",
                        contact.Agent.Split('-')[1]),
                    new SqlParameter("Email", contact.Email),
                    new SqlParameter("Message", contact.Message)
                });

                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
