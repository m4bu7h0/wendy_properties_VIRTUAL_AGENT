using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using wendy_properties_VA_DOMAIN.data;

namespace wendy_properties_VA_DOMAIN.business {
    public class BLListing {

        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public string ImageString { get; set; }
        public string Reference { get; set; }
        public string MarketingHeading { get; set; }
        public string Description { get; set; }
        public string Suburb { get; set; }
        public string AgentGUID { get; set; }

        public IEnumerable<BLListing> GetFeaturedListing() {

            return AddModelToList(
                DALListings.GetFeaturedListings());
        }
        public IEnumerable<BLListing> GetSearchListings(
            string suburb, int bedrooms,
            decimal minprice, decimal maxprice) {

            return AddModelToList(
                DALListings.SearchListings(suburb, bedrooms,
                minprice, maxprice));
        }
        public IEnumerable<string> GetSuburbs() {

            var suburbsDT = DALListings.GetListingsSuburbs();

            return (
                from DataRow drow in suburbsDT.Rows
                select drow["Suburb"].ToString()).ToList();
        }
        public IEnumerable<int> GetBedrooms() {

            var bedroomsDT =
                DALListings.GetListingsBedrooms();

            return (
                from DataRow drow in bedroomsDT.Rows
                select (int)drow["Bedrooms"]).ToList();
        }

        public IEnumerable<string> GetPriceRanges() {

            var rangesDT =
                DALListings.GetListingsPrices();
            var priceList = new List<string>();
            var range = new StringBuilder();
            var priceStr = "";


            for (var i = 0; i < rangesDT.Rows.Count; i++) {

                var price =
                    Convert.ToDecimal(rangesDT.Rows[i]["Price"]);

                var roundedPrice =
                       Math.Round(price / 50) * 50;

                if (i == 0) {

                    var firstStrRange =
                        "0 - " + roundedPrice;
                    range.Append(firstStrRange);
                    priceList.Add(firstStrRange);

                } else {

                    if (range.Length > 0) {

                        var firstRange =
                            range.ToString().Split('-').Length == 3 ?
                            range.ToString().Split('-')[2].Trim() :
                            range.ToString().Split('-')[1].Trim();

                        priceStr =
                            firstRange +
                            " - " + Math.Round(price / 50) * 50;

                        if (Convert.ToDecimal(firstRange) !=
                            Math.Round(price / 50) * 50) {

                            priceList.Add(priceStr);
                        }
                    } else {

                        var priceR =
                            priceStr.Split('-').Length == 3
                                ? priceStr.Split('-')[2].Trim()
                                : priceStr.Split('-')[1].Trim();

                        if (Convert.ToDecimal(priceR) < roundedPrice) {

                            priceStr =
                                priceR + " - " + roundedPrice;
                        }

                        if (price > roundedPrice) {
                            priceStr =
                                    roundedPrice + " - " +
                                    (roundedPrice + 50m);
                        }

                        priceList.Add(priceStr);

                    }
                    range.Clear();
                }
            }

            return priceList;
        }
        private IEnumerable<BLListing> AddModelToList(DataTable dt) {

            return (from DataRow dtRow in dt.Rows
                    select new BLListing {
                        Price = (decimal)dtRow["Price"],
                        Bedrooms = (int)dtRow["Bedrooms"],
                        ImageString =
                            "data:image/png;base64," +
                            Convert.ToBase64String((byte[])dtRow["Image"]),
                        Reference =
                            dtRow["ReferenceNumber"].ToString(),
                        MarketingHeading =
                            dtRow["MarketingHeading"].ToString(),
                        Description =
                            dtRow["Description"].ToString(),
                        Suburb = dtRow["Suburb"].ToString(),
                        AgentGUID = dtRow["AgentGUID"].ToString()
                    }).ToList();
        }
    }
}
