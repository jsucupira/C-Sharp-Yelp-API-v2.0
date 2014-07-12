using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YelpFeed.BusinessApi;
using YelpFeed.SearchApi;

namespace YelpFeed.Test
{
    [TestClass]
    public class YelpTest
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["customer_key"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["customer_secret"];
        private readonly string _token = ConfigurationManager.AppSettings["access_token"];
        private readonly string _tokenSecret = ConfigurationManager.AppSettings["access_token_secret"];

        [TestMethod]
        public void test_yelp_search_api()
        {
            YelpOAuthUtil result = new YelpOAuthUtil(_consumerKey, _consumerSecret, _token, _tokenSecret);
            YelpSearchObject yelpResult = result.SearchApi("term=food&location=Tampa&state=FL");
            Assert.IsNotNull(yelpResult);
        }
        [TestMethod]
        public void test_yelp_search_api_json()
        {
            YelpOAuthUtil result = new YelpOAuthUtil(_consumerKey, _consumerSecret, _token, _tokenSecret);
            var yelpJson = result.SearchApiJson("term=food&location=Tampa&state=FL");
Assert.IsTrue(!string.IsNullOrEmpty(yelpJson));        }

        [TestMethod]
        public void test_yelp_business()
        {
            YelpOAuthUtil result = new YelpOAuthUtil(_consumerKey, _consumerSecret, _token, _tokenSecret);
            YelpBusinessObject yelpResult = result.BusinessId("yelp-san-francisco");
            Assert.IsNotNull(yelpResult);
        }

        [TestMethod]
        public void test_yelp_business_Json()
        {
            YelpOAuthUtil result = new YelpOAuthUtil(_consumerKey, _consumerSecret, _token, _tokenSecret);
            var yelpJson = result.BusinessIdJson("yelp-san-francisco");
            Assert.IsTrue(!string.IsNullOrEmpty(yelpJson));
        }
    }
}