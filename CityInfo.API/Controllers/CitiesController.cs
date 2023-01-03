using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntries = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntries));
            /*return Ok(
               CitiesDataStore.Current.Cities
                ) ;*/
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  GetCity(int id, bool includePointOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync( id, includePointOfInterest );
            if(city == null)
            {
                return NotFound();
            }
            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
            /* var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
             if (cityToReturn == null)
                 return NotFound();
             return Ok(cityToReturn);*/
        }
    }
}
