using System;
using System.Linq;
using System.Web.Mvc;
using wendy_properties_VA_WEBUI.Models;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_WEBUI.Controllers {
    public class ListingController : Controller {
        // GET: Listing
        public ActionResult SearchListings() {

            var listing = new BLListing();

            var searchListings = new SearchListingVW {
                Suburbs =
                    new SelectList(listing.GetSuburbs()),
                Bedrooms =
                    new SelectList(listing.GetBedrooms()),
                PriceRanges = 
                    new SelectList(listing.GetPriceRanges())
            };

            return View(searchListings);
        }

        [HttpGet]
        public ActionResult GetListings(SearchListingVW searchVM) {

            var listingModel = new BLListing();

            var splitted =
                searchVM.PriceRange.Split('-');

            var minPrice = 
                Convert.ToDecimal(splitted[0].Trim());
            var maxPrice =
                 Convert.ToDecimal(splitted[1].Trim());

            var searchListings =
                listingModel.GetSearchListings(
                searchVM.Suburb, searchVM.BedroomsNumber,
                minPrice, maxPrice);

            ViewData["Suburb"] = searchVM.Suburb;
            return View(searchListings);
        }
    }
}