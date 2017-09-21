using System.Collections.Generic;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_WEBUI.Models {
    public class FeaturedListingVM {

        public IEnumerable<BLListing> FeaturedListings { get; set; }
    }
}