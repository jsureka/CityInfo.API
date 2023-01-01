using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; set; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with statue of liberty",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited Urban park in united states"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-storied skyscraper located in Midtown Manhattan"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Paris",
                    Description = "This is Paris City of France",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Eiffel Tower",
                            Description = "One of the seven wonders of the world"
                        }
                    }
                }
            };
        }
    }
}
