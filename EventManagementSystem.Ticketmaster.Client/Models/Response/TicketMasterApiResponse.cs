// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
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
    public string locale { get; set; }
    public List<Image> images { get; set; }
    public List<Classification> classifications { get; set; }
    public Links _links { get; set; }
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
    public Links _links { get; set; }
    public Embedded _embedded { get; set; }
}

public class Genre
{
    public string id { get; set; }
    public string name { get; set; }
}

public class Image
{
    public string ratio { get; set; }
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public bool fallback { get; set; }
}

public class Links
{
    public Self self { get; set; }
    public Next next { get; set; }
    public List<Attraction> attractions { get; set; }
    public List<Venue> venues { get; set; }
}

public class Location
{
    public string longitude { get; set; }
    public string latitude { get; set; }
}

public class Market
{
    public string id { get; set; }
}

public class Next
{
    public string href { get; set; }
    public bool templated { get; set; }
}

public class Page
{
    public int size { get; set; }
    public int totalElements { get; set; }
    public int totalPages { get; set; }
    public int number { get; set; }
}

public class Promoter
{
    public string id { get; set; }
}

public class Public
{
    public DateTime startDateTime { get; set; }
    public bool startTBD { get; set; }
    public DateTime endDateTime { get; set; }
}

public class TicketMasterApiResponse
{
    public Links _links { get; set; }
    public Embedded _embedded { get; set; }
    public Page page { get; set; }
}

public class Sales
{
    public Public @public { get; set; }
}

public class Segment
{
    public string id { get; set; }
    public string name { get; set; }
}

public class Self
{
    public string href { get; set; }
    public bool templated { get; set; }
}

public class Start
{
    public string localDate { get; set; }
    public bool dateTBD { get; set; }
    public bool dateTBA { get; set; }
    public bool timeTBA { get; set; }
    public bool noSpecificTime { get; set; }
}

public class State
{
    public string name { get; set; }
    public string stateCode { get; set; }
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

public class Venue
{
    public string href { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string id { get; set; }
    public bool test { get; set; }
    public string locale { get; set; }
    public string postalCode { get; set; }
    public string timezone { get; set; }
    public City city { get; set; }
    public State state { get; set; }
    public Country country { get; set; }
    public Address address { get; set; }
    public Location location { get; set; }
    public List<Market> markets { get; set; }
    public Links _links { get; set; }
}
