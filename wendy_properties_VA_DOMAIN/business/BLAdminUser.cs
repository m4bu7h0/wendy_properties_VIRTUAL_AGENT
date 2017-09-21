using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wendy_properties_VA_DOMAIN.data;

namespace wendy_properties_VA_DOMAIN.business {
    public class BLAdminUser {

        public string Username { get; set; }
        public string Password { get; set; }

        public void AddAdminUser(string username, string password) {

            Username = username;
            Password = password;

            DALAdminUsers.AddAdminUser(this);
        }
    }
}
