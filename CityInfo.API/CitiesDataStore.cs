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
                    Description = "The one with statue of liberty"
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "This is antwerp city"
                }
            };
        }
    }
}
