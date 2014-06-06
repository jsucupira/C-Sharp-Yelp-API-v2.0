using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using YelpFeed.BusinessApi;
using YelpFeed.SearchApi;

namespace YelpFeed
{
    public class YelpOAuthUtil : OAuthBase
    {
        public YelpOAuthUtil(string oauthConsumerKey, string oauthConsumerSecret, string oauthToken, string oauthTokenSecret)
        {
            _oauthConsumerKey = oauthConsumerKey;
            _oauthConsumerSecret = oauthConsumerSecret;
            _oauthToken = oauthToken;
            _oauthTokenSecret = oauthTokenSecret;
        }

        private const string BUSINESS_URL = "http://api.yelp.com/v2/business/";
        private const string SEARCH_URL = "http://api.yelp.com/v2/search";
        private readonly string _oauthConsumerKey;
        private readonly string _oauthConsumerSecret;
        private readonly string _oauthNonce = GenerateNonce();
        private readonly string _oauthTimestamp = GenerateTimeStamp();
        private readonly string _oauthToken;
        private readonly string _oauthTokenSecret;

        /// <summary>
        ///     http://www.yelp.com/developers/documentation/v2/business
        ///     i.e yelp-san-francisco
        /// </summary>
        /// <param name="businessId">the id of the business</param>
        /// <returns></returns>
        public YelpBusinessObject BusinessId(string businessId)
        {
            string url = BUSINESS_URL + businessId;
            string oauthSignature = OauthSignature(url);
            // create the request header
            string authHeader = AuthHeader(oauthSignature);
            // make the request

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (WebResponse response = request.GetResponse())
            {
                HttpWebResponse status = (HttpWebResponse) response;
                if (HttpStatusCode.OK == status.StatusCode)
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        YelpBusinessObject result = JsonConvert.DeserializeObject<YelpBusinessObject>(stream.ReadToEnd());
                        return result;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///     http://www.yelp.com/developers/documentation/v2/business
        ///     i.e yelp-san-francisco
        /// </summary>
        /// <param name="businessId">the id of the business</param>
        /// <returns></returns>
        public string BusinessIdJson(string businessId)
        {
            string url = BUSINESS_URL + businessId;

            string oauthSignature = OauthSignature(url);

            // create the request header
            string authHeader = AuthHeader(oauthSignature);

            // make the request

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (WebResponse response = request.GetResponse())
            {
                HttpWebResponse status = (HttpWebResponse) response;
                if (HttpStatusCode.OK == status.StatusCode)
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                        return stream.ReadToEnd();
                }
            }
            return null;
        }

        /// <summary>
        ///     http://www.yelp.com/developers/documentation/v2/search_api
        ///     i.e term=food&location=San Francisco or term=german+food&location=Hayes&cll=37.77493,-122.419415
        /// </summary>
        public YelpSearchObject SearchApi(string queryString)
        {
            string url = SEARCH_URL + "?" + queryString;

            string oauthSignature = OauthSignature(url);

            // create the request header
            string authHeader = AuthHeader(oauthSignature);
            // make the request

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (WebResponse response = request.GetResponse())
            {
                HttpWebResponse status = (HttpWebResponse) response;
                if (HttpStatusCode.OK == status.StatusCode)
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        string result = stream.ReadToEnd();
                        return JsonConvert.DeserializeObject<YelpSearchObject>(result);
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///     http://www.yelp.com/developers/documentation/v2/search_api
        ///     i.e term=food&location=San Francisco or term=german+food&location=Hayes&cll=37.77493,-122.419415
        /// </summary>
        public string SearchApiJson(string queryString)
        {
            string url = SEARCH_URL + "?" + queryString;

            string oauthSignature = OauthSignature(url);
            // create the request header
            string authHeader = AuthHeader(oauthSignature);
            // make the request

            ServicePointManager.Expect100Continue = false;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            using (WebResponse response = request.GetResponse())
            {
                HttpWebResponse status = (HttpWebResponse) response;
                if (HttpStatusCode.OK == status.StatusCode)
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                        return stream.ReadToEnd();
                }
            }
            return null;
        }

        #region private methods

        private string AuthHeader(string oauthSignature)
        {
            const string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                                        "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                                        "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                                        "oauth_version=\"{6}\"";

            string authHeader = string.Format(headerFormat,
                Uri.EscapeDataString(_oauthNonce),
                Uri.EscapeDataString(Hmacsha1SignatureType),
                Uri.EscapeDataString(_oauthTimestamp),
                Uri.EscapeDataString(_oauthConsumerKey),
                Uri.EscapeDataString(_oauthToken),
                Uri.EscapeDataString(oauthSignature),
                Uri.EscapeDataString(OAuthVersion));
            return authHeader;
        }

        private string OauthSignature(string url)
        {
            string normalizeUrl;
            string normalizedString;
            string oauthSignature = GenerateSignature(new Uri(url), _oauthConsumerKey, _oauthConsumerSecret, _oauthToken, _oauthTokenSecret, "GET", _oauthTimestamp, _oauthNonce, out normalizeUrl, out normalizedString);
            return oauthSignature;
        }

        #endregion
    }
}