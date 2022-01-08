using System;
using InfectedBackEnd.Application.UseCases.User.DTO.Request;
using InfectedBackEnd.Application.UseCases.User.DTO.Response;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Application.UseCases.User.Utils
{
    public static class MapToInterRelationalEntities
    {
        public static UserLocation ToUserLocation(this Domain.User user, Location location, DateTime datetime)
        {
            return new(
                user!.Id,
                location!.Id,
                datetime,
                null);
        }

        public static UserDisease
            ToUserDisease(this AddDiseaseToUserDto diseaseToUserDto, Guid userId, Guid diseaseId)
        {
            return new(
                null,
                false,
                diseaseToUserDto!.ShowSymptoms,
                diseaseToUserDto.StartDate,
                diseaseToUserDto.EndDate,
                userId,
                diseaseId);
        }

        public static AddUserLocationResponseDto ToUserLocationResponse(this UserLocation userLocation,
            Location location)
        {
            return new(
                userLocation!.Id,
                location!.Latitude,
                location.Longitude);
        }

        public static AddDiseaseToUserResponseDto ToUserDiseaseResponse(this UserDisease userDisease)
        {
            return new(userDisease.Id, userDisease.User_Id, userDisease.Disease_Id, userDisease.Cured);
        }
    }
}