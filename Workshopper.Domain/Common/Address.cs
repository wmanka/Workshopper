namespace Workshopper.Domain.Common;

public class Address(string line1, string? line2, string city, string country, string postCode)
{
    public string Line1 { get; set; } = line1;

    public string? Line2 { get; set; } = line2;

    public string City { get; set; } = city;

    public string Country { get; set; } = country;

    public string PostCode { get; set; } = postCode;
}