using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using wendy_properties_VA_DOMAIN.business;

namespace wendy_properties_VA_DOMAIN.data {
    public static class DALListings {

        static readonly string wpConnectDB =
            ConfigurationManager
            .ConnectionStrings["wpConnectDB"]
            .ConnectionString;

        // 1. Get Featured Listings
        public static DataTable GetFeaturedListings() {

            return GetDataTable(
                "GetFeaturedListingsFE");
        }

        // 2. Search  Listings
        public static DataTable SearchListings(
            string suburb, int bedrooms,
            decimal minPrice, decimal maxPrice) {

            return GetDataTable(
                "SearchListingsFE", new[]{
                    new SqlParameter("Suburb",suburb),
                    new SqlParameter("MinPrice", minPrice),
                    new SqlParameter("MaxPrice", maxPrice),
                    new SqlParameter("Bedrooms", bedrooms)
                });
        }
        public static DataTable GetListingDetails(string listingGUID) {

            return GetDataTable(
                "GetListingDetailsFE", new[]{
                new SqlParameter("ListingGUID", listingGUID)
            });
        }

        public static DataTable GetListingsSuburbs() {

            return GetDataTable("GetListingsSuburbsFE");
        }
        public static DataTable GetListingsPrices() {

            return GetDataTable("GetListingsPricesFE");
        }
        public static DataTable GetListingsBedrooms() {

            return GetDataTable("GetListingsBedroomsFE");
        }

        // Create a  new listing
        public static void AddListing(BLListing listing) {

            UpdateTransact(
                "AddListingBE", new[] {
                    new SqlParameter("Price", listing.Price),
                    new SqlParameter("Bedrooms", listing.Bedrooms),
                    new SqlParameter(
                        "ReferenceNumber", listing.Reference),
                    new SqlParameter(
                        "MarketingHeading", listing.MarketingHeading),
                    new SqlParameter(
                        "Description", listing.Description),
                    new SqlParameter("Suburb", listing.Suburb),
                    new SqlParameter("AgentGUID", listing.AgentGUID)
                });
        }

        // Update featured listings
        public static void UpdateFeaturedListings(string listingGUID) {

            UpdateTransact("", new[]{
                new SqlParameter("ListingGUID", listingGUID),
            });
        }
        // Universal method to get a datatable
        private static DataTable GetDataTable(
            string storedProc, SqlParameter[] parameters = null) {
            var data = new DataTable();

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand(storedProc, sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                if (parameters != null) {
                    sqlComm.Parameters.AddRange(parameters);
                }

                using (var dAdapter = new SqlDataAdapter(sqlComm)) {
                    dAdapter.Fill(data);
                }
                return data;
            }
        }

        // Universal Update
        private static void UpdateTransact(
            string storedProc, SqlParameter[] parameters) {

            using (var sqlConn = new SqlConnection(wpConnectDB)) {

                sqlConn.Open();

                var sqlComm =
                    new SqlCommand(storedProc, sqlConn) {
                        CommandType =
                            CommandType.StoredProcedure,
                        CommandTimeout = 180
                    };

                if (parameters != null) {
                    sqlComm.Parameters.Add(parameters);
                }

                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
