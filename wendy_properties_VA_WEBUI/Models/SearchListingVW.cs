using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_WEBUI.Models {
    public class SearchListingVW {

        public SelectList Suburbs { get; set; }

        [Required(ErrorMessage = "Please select Suburb")]
        public string Suburb { get; set; }
        public SelectList Bedrooms { get; set; }

        [Required(ErrorMessage = "Please Number of Bedrooms")]
        public int BedroomsNumber { get; set; }
        public SelectList PriceRanges { get; set; }

        [Required(ErrorMessage = "Please select Price Range")]
        public string PriceRange { get; set; }
        public IEnumerable<BLListing> SearchListings { get; set; }
    }
}