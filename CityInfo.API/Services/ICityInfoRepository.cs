﻿using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId, bool includePointOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int cityId);

        Task<bool> CityExistsAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int city, int pointOfInterestId);


    }
}
