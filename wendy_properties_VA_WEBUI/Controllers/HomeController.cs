using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wendy_properties_VA_DOMAIN.business;
using wendy_properties_VA_WEBUI.Models;

namespace wendy_properties_VA_WEBUI.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            var listing = new BLListing();

            var featured = new FeaturedListingVM {
                FeaturedListings =
                    listing.GetFeaturedListing()
            };
            return View(featured);
        }
    }
}