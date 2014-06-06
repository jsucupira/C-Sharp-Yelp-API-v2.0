using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YelpFeed.BusinessApi;
using YelpFeed.SearchApi;

namespace YelpFeed.Test
{
    [TestClass]
    public class YelpTest
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["CONSUMER_KEY"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["CONSUMER_SECRET"];
        private readonly string _token = ConfigurationManager.AppSettings["TOKEN"];
        private readonly string _tokenSecret = ConfigurationManager.AppSettings["TOKEN_SECRET"];

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