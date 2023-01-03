using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly ICityInfoRepository _cityinforepository;
        private readonly IMapper _mapper;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository
            ,IMapper mapper)
        {
            _logger = logger;
            _cityinforepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            if(!await _cityinforepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation(
                    $"City with {cityId} does not exists");
                return NotFound();
            }
           var pointOfInterestForCity = await _cityinforepository.GetPointOfInterestForCityAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestForCity));
        }

        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetSinglePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterests.FirstOrDefault(c => c.Id == id);
            if(pointOfInterest is null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterestForCreation)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterests).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterestForCreation.Name,
                Description = pointOfInterestForCreation.Description
            };
            city.PointOfInterests.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    id = finalPointOfInterest.Id
                },finalPointOfInterest);
        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfinterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterestForUpdate)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterestForUpdate.Name;
            pointOfInterestFromStore.Description = pointOfInterestForUpdate.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.PointOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();


        }
    }
}
