using System.Collections.Generic;

namespace YelpFeed.BusinessApi
{
    public class User
    {
        public string id { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
    }

    public class Review
    {
        public string excerpt { get; set; }
        public string id { get; set; }
        public int rating { get; set; }
        public string rating_image_large_url { get; set; }
        public string rating_image_small_url { get; set; }
        public string rating_image_url { get; set; }
        public int time_created { get; set; }
        public User user { get; set; }
    }

    public class Location
    {
        public List<string> address { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public List<string> display_address { get; set; }
        public List<string> neighborhoods { get; set; }
        public string postal_code { get; set; }
        public string state_code { get; set; }
    }

    public class YelpBusinessObject
    {
        public List<List<string>> categories { get; set; }
        public string display_phone { get; set; }
        public string id { get; set; }
        public string image_url { get; set; }
        public bool is_claimed { get; set; }
        public bool is_closed { get; set; }
        public Location location { get; set; }
        public int menu_date_updated { get; set; }
        public string menu_provider { get; set; }
        public string mobile_url { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public double rating { get; set; }
        public string rating_img_url { get; set; }
        public string rating_img_url_large { get; set; }
        public string rating_img_url_small { get; set; }
        public int review_count { get; set; }
        public List<Review> reviews { get; set; }
        public string snippet_image_url { get; set; }
        public string snippet_text { get; set; }
        public string url { get; set; }
    }
}