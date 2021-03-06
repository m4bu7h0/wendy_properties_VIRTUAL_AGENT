﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wendy_properties_VA_WEBUI.Models {
    public class ContactVM {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public SelectList Agents { get; set; }
        public string Agent { get; set; }
    }
}