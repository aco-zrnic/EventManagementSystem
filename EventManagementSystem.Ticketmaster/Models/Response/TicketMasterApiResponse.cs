using Newtonsoft.Json;

namespace EventManagementSystem.Ticketmaster.Models.Response
{
    public class TicketMasterApiResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Access
        {
            public DateTime startDateTime { get; set; }
            public bool startApproximate { get; set; }
            public bool endApproximate { get; set; }
        }

        public class Address
        {
            public string line1 { get; set; }
        }

        public class Attraction
        {
            public string href { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public bool test { get; set; }
            public string url { get; set; }
            public string locale { get; set; }
            public List<Image> images { get; set; }
            public List<Classification> classifications { get; set; }
            public UpcomingEvents upcomingEvents { get; set; }
            public Links _links { get; set; }
            public ExternalLinks externalLinks { get; set; }
        }

        public class City
        {
            public string name { get; set; }
        }

        public class Classification
        {
            public bool primary { get; set; }
            public Segment segment { get; set; }
            public Genre genre { get; set; }
            public SubGenre subGenre { get; set; }
            public bool family { get; set; }
            public Type type { get; set; }
            public SubType subType { get; set; }
        }

        public class Country
        {
            public string name { get; set; }
            public string countryCode { get; set; }
        }

        public class Dates
        {
            public Start start { get; set; }
            public string timezone { get; set; }
            public Status status { get; set; }
            public bool spanMultipleDays { get; set; }
            public Access access { get; set; }
        }

        public class Embedded
        {
            public List<Event> events { get; set; }
            public List<Venue> venues { get; set; }
            public List<Attraction> attractions { get; set; }
        }

        public class Event
        {
            public string name { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public bool test { get; set; }
            public string url { get; set; }
            public string locale { get; set; }
            public List<Image> images { get; set; }
            public Sales sales { get; set; }
            public Dates dates { get; set; }
            public List<Classification> classifications { get; set; }
            public Promoter promoter { get; set; }
            public List<Promoter> promoters { get; set; }
            public Links _links { get; set; }
            public Embedded _embedded { get; set; }
            public List<PriceRange> priceRanges { get; set; }
            public Seatmap seatmap { get; set; }
        }

        public class ExternalLinks
        {
            public List<Youtube> youtube { get; set; }
            public List<Twitter> twitter { get; set; }
            public List<Itune> itunes { get; set; }
            public List<Facebook> facebook { get; set; }
            public List<Wiki> wiki { get; set; }
            public List<Spotify> spotify { get; set; }
            public List<Musicbrainz> musicbrainz { get; set; }
            public List<Instagram> instagram { get; set; }
            public List<Homepage> homepage { get; set; }
        }

        public class Facebook
        {
            public string url { get; set; }
        }

        public class First
        {
            public string href { get; set; }
        }

        public class Genre
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Homepage
        {
            public string url { get; set; }
        }

        public class Image
        {
            public string ratio { get; set; }
            public string url { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool fallback { get; set; }
            public string attribution { get; set; }
        }

        public class Instagram
        {
            public string url { get; set; }
        }

        public class Itune
        {
            public string url { get; set; }
        }

        public class Last
        {
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
            public List<Attraction> attractions { get; set; }
            public List<Venue> venues { get; set; }
            public First first { get; set; }
            public Next next { get; set; }
            public Last last { get; set; }
        }

        public class Location
        {
            public string longitude { get; set; }
            public string latitude { get; set; }
        }

        public class Musicbrainz
        {
            public string id { get; set; }
        }

        public class Next
        {
            public string href { get; set; }
        }

        public class Page
        {
            public int size { get; set; }
            public int totalElements { get; set; }
            public int totalPages { get; set; }
            public int number { get; set; }
        }

        public class PriceRange
        {
            public string type { get; set; }
            public string currency { get; set; }
            public double min { get; set; }
            public double max { get; set; }
        }

        public class Promoter
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Promoter2
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Public
        {
            public DateTime startDateTime { get; set; }
            public bool startTBD { get; set; }
            public bool startTBA { get; set; }
            public DateTime endDateTime { get; set; }
        }

        public class Root
        {
            public Embedded _embedded { get; set; }
            public Links _links { get; set; }
            public Page page { get; set; }
        }

        public class Sales
        {
            public Public @public { get; set; }
        }

        public class Seatmap
        {
            public string staticUrl { get; set; }
        }

        public class Segment
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Spotify
        {
            public string url { get; set; }
        }

        public class Start
        {
            public string localDate { get; set; }
            public bool dateTBD { get; set; }
            public bool dateTBA { get; set; }
            public bool timeTBA { get; set; }
            public bool noSpecificTime { get; set; }
            public string localTime { get; set; }
            public DateTime? dateTime { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
        }

        public class SubGenre
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class SubType
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Twitter
        {
            public string url { get; set; }
        }

        public class Type
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class UpcomingEvents
        {
            [JsonProperty("mfx-de")]
            public int mfxde { get; set; }
            public int _total { get; set; }
            public int _filtered { get; set; }
            public int? ticketmaster { get; set; }
            public int? tmr { get; set; }
            public int? ticketweb { get; set; }
        }

        public class Venue
        {
            public string href { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public bool test { get; set; }
            public string url { get; set; }
            public string locale { get; set; }
            public string postalCode { get; set; }
            public string timezone { get; set; }
            public City city { get; set; }
            public Country country { get; set; }
            public Address address { get; set; }
            public Location location { get; set; }
            public UpcomingEvents upcomingEvents { get; set; }
            public Links _links { get; set; }
        }

        public class Wiki
        {
            public string url { get; set; }
        }

        public class Youtube
        {
            public string url { get; set; }
        }
    }
}
