using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wendy_properties_VA_DOMAIN.data;

namespace wendy_properties_VA_DOMAIN.business {
    public class BLContact {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Agent { get; set; }
        public Dictionary<string, string> AgentDict { get; set; }

        public Dictionary<string, string> GetAgents() {

            var agentsDT = DALContacts.GetAgents();
            var agentsDic = new Dictionary<string, string>();

            if (agentsDT.Rows.Count > 0) {
                for (var i = 0; i < agentsDT.Rows.Count; i++) {
                    agentsDic.Add(
                        agentsDT.Rows[i]["AgentGUID"].ToString(),
                        agentsDT.Rows[i]["FirstName"] + " " +
                        agentsDT.Rows[i]["LastName"]);
                }
            }

            return agentsDic;
        }
        public void AddContact(
            string firstName, string lastName,
            string email, string agent,string message) {

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Agent = agent;
            Message = message;

            DALContacts.AddContact(this);
        }
    }
}
