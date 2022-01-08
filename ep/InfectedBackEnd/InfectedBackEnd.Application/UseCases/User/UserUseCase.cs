using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.User.Dto;
using InfectedBackEnd.Application.UseCases.User.DTO.Request;
using InfectedBackEnd.Application.UseCases.User.DTO.Response;
using InfectedBackEnd.Application.UseCases.User.Utils;
using InfectedBackEnd.Domain;
using InfectedBackEnd.Infrastructure.Database;

namespace InfectedBackEnd.Application.UseCases.User
{
    public interface IUserUseCase
    {
        Task<GetUserResponseDto> GetUser(string document, Guid token);
        Task<Domain.User> GetUserByToken(Guid token);
        Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto requestDto);

        Task<AddUserLocationResponseDto> AddLocationToUser(AddLocationToUserDto locationToUserDto, Guid token,
            Guid userId);

        Task<AddDiseaseToUserResponseDto> AddDiseaseToUser(AddDiseaseToUserDto diseaseToUserDto, Guid token,
            Guid userId);

        Task<UserDisease> GetUserDiseaseById(Guid token, Guid userDiseaseId);

        Task<AddDiseaseToUserResponseDto> UpdateUserDisease(Guid token, Guid userId, Guid userDiseaseId,
            PatchDiseaseToUserDto diseaseToUserDto);

        Task<IList<UserDisease>> GetUserDiseases(Guid token, Guid userId);
        Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto requestDto);

        Task<bool?> IsSick(Guid token, Guid userId, DateTime? date);
    }

    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserLocationRepository _userLocationRepository;
        private readonly IUserDiseaseRepository _userDiseaseRepository;
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly IAggregationRepository _aggregationRepository;

        public UserUseCase(
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IUserLocationRepository userLocationRepository,
            IDiseasesRepository diseasesRepository,
            IUserDiseaseRepository userDiseaseRepository,
            IAggregationRepository aggregationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            _userLocationRepository = userLocationRepository;
            _diseasesRepository = diseasesRepository;
            _userDiseaseRepository = userDiseaseRepository;
            _aggregationRepository = aggregationRepository;
        }

        public async Task<GetUserResponseDto> GetUser(string document, Guid token)
        {
            var user = await _userRepository.GetUser(token);

            return user switch
            {
                null => null,
                _ => user.Document == document
                    ? new GetUserResponseDto(user.Id, user.Name, user.Email)
                    : new GetUserResponseDto()
            };
        }

        public async Task<Domain.User> GetUserByToken(Guid token)
        {
            return await _userRepository.GetUser(token);
        }

        public async Task<CreateUserResponseDto> CreateUser(CreateUserRequestDto requestDto)
        {
            var user = requestDto.ToUserDomain();
            var result = await _userRepository.CreateUser(user);
            if (result == Guid.Empty)
                return new CreateUserResponseDto();
            return user.ToCreateResponse();
        }

        public async Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto requestDto)
        {
            var user = await _userRepository.GetUser(requestDto.Document);
            if (user == null || user.Password != requestDto.Password) return new LoginUserResponseDto();

            return user.ToLoginResponse();
        }

        public async Task<AddUserLocationResponseDto> AddLocationToUser(AddLocationToUserDto locationToUserDto,
            Guid token, Guid userId)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null)
                return null;

            var location =
                await _locationRepository.GetLocationByCoordinates(locationToUserDto.Lat, locationToUserDto.Long) ??
                await CreateLocation(locationToUserDto);

            var userLocation = user.ToUserLocation(location, locationToUserDto.Datetime);

            var result = await _userLocationRepository.AddUserLocation(userLocation);

            return result switch
            {
                true => userLocation.ToUserLocationResponse(location),
                _ => new AddUserLocationResponseDto()
            };
        }

        public async Task<AddDiseaseToUserResponseDto> AddDiseaseToUser(AddDiseaseToUserDto diseaseToUserDto,
            Guid token, Guid userId)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null)
                return null;

            var disease = await _diseasesRepository.GetDiseaseByName(diseaseToUserDto.DiseaseName);

            if (disease is null)
                throw new Exception("Disease not found. Please, contact support to add this disease");

            var userDisease = diseaseToUserDto.ToUserDisease(userId, disease.Id);
            var result = await _userDiseaseRepository.AddUserDisease(userDisease);
            return result switch
            {
                true => userDisease.ToUserDiseaseResponse(),
                _ => new AddDiseaseToUserResponseDto()
            };
        }

        public async Task<AddDiseaseToUserResponseDto> UpdateUserDisease(Guid token, Guid userId, Guid userDiseaseId,
            PatchDiseaseToUserDto diseaseToUserDto)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null)
                return null;

            var oldUserDisease = await _userDiseaseRepository.GetUserDiseaseById(userDiseaseId);

            if (oldUserDisease is null)
                throw new ArgumentException("Disease associated with user not found.");

            if (CheckForDifferences(diseaseToUserDto, oldUserDisease))
                return oldUserDisease.ToUserDiseaseResponse();

            var newUserDisease = CreateUpdateUserDiseaseObject(diseaseToUserDto, oldUserDisease);

            var result = await _userDiseaseRepository.UpdateUserDisease(newUserDisease);
            return result.ToUserDiseaseResponse();
        }

        private bool CheckForDifferences(PatchDiseaseToUserDto diseaseToUserDto, UserDisease oldUserDisease)
        {
            return diseaseToUserDto.Cured is not null &&
                   (bool) diseaseToUserDto.Cured == oldUserDisease.Cured ||
                   diseaseToUserDto.EndDate is not null &&
                   (DateTime) diseaseToUserDto.EndDate == oldUserDisease.EndDate ||
                   diseaseToUserDto.ShowSymptoms is not null &&
                   (bool) diseaseToUserDto.ShowSymptoms == oldUserDisease.Show_Symptoms;
        }

        public async Task<IList<UserDisease>> GetUserDiseases(Guid token, Guid userId)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null)
                return null;

            var userDiseases = await _userDiseaseRepository.GetAllUserDiseases(userId);

            return userDiseases;
        }

        public async Task<UserDisease> GetUserDiseaseById(Guid token, Guid userDiseaseId)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null)
                return null;

            var userDisease = await _userDiseaseRepository.GetUserDiseaseById(userDiseaseId);

            return userDisease ?? throw new ArgumentException("Disease associated with user is not found.");
        }

        private UserDisease CreateUpdateUserDiseaseObject(PatchDiseaseToUserDto patchDto, UserDisease oldUserDisease)
        {
            if (patchDto.Cured is not null)
                oldUserDisease.Cured = (bool) patchDto.Cured;
            if (patchDto.EndDate is not null)
                oldUserDisease.EndDate = (DateTime) patchDto.EndDate;
            if (patchDto.ShowSymptoms is not null)
                oldUserDisease.Show_Symptoms = (bool) patchDto.ShowSymptoms;

            return oldUserDisease;
        }

        private async Task<Location> CreateLocation(AddLocationToUserDto locationToUserDto)
        {
            var location = new Location(null, locationToUserDto.Lat, locationToUserDto.Long);
            await _locationRepository.CreateLocation(location);
            return location;
        }

        public async Task<bool?> IsSick(Guid token, Guid userId, DateTime? date)
        {
            var user = await _userRepository.GetUser(token);
            if (user is null || token != userId)
                return null;

            var dataReturned = DataReturned(userId, out var userLocations);

            var locations =
                await this._locationRepository.GetAllLocationByIds(userLocations.Select(x => x.Location_Id).ToList());

            var groupedLocationsUserLocations = GroupedLocationsUserLocations(userLocations, locations);

            return await IsUserSick(groupedLocationsUserLocations, dataReturned, userId,
                (await this._diseasesRepository.GetDiseaseByName("Covid")).Id);
        }

        private async Task<bool> IsUserSick(List<IGrouping<int, LocationUserLocation>> groupedLocationsUserLocations, IEnumerable<Aggregation> dataReturned, Guid userId, Guid diseaseId)
        {
            foreach (var grouped in groupedLocationsUserLocations)
            {
                foreach (var locationUserLocation in grouped)
                {
                    if (dataReturned.Select(x => x.LocationId).Contains(locationUserLocation.Location_Id))
                    {
                        await this._userDiseaseRepository.AddUserDisease(new UserDisease(null, false, false, DateTime.Now,
                            DateTime.MinValue, locationUserLocation.User_Id,
                            diseaseId));
                        return true;
                    }
                }
            }

            return false;
        }

        private static List<IGrouping<int, LocationUserLocation>> GroupedLocationsUserLocations(IList<UserLocation> userLocations, IList<Location> locations)
        {
            var groupedLocationsUserLocations = userLocations.Join<UserLocation, Location, Guid, LocationUserLocation>(
                    locations,
                    ul => ul.Location_Id,
                    x => x.Id,
                    (ul, l) => new(
                        ul.Id,
                        l.Latitude,
                        l.Longitude,
                        l.Name,
                        ul.User_Id,
                        ul.Location_Id,
                        ul.DateAndTime))
                .GroupBy(x => x.DateAndTime.Hour)
                .ToList();
            return groupedLocationsUserLocations;
        }

        private IEnumerable<Aggregation> DataReturned(Guid userId, out IList<UserLocation> userLocations)
        {
            var dataReturnedTask = this._aggregationRepository.GetRelevantData(userId);
            var userLocationsTask = this._userLocationRepository.GetAllUserLocationByUserId(userId);
            var taskList = new List<Task>() {dataReturnedTask, userLocationsTask};

            Task.WaitAll(taskList.ToArray());

            var dataReturned = dataReturnedTask.Result.Where(ag => ag.UserId != userId);
            userLocations = userLocationsTask.Result;
            return dataReturned;
        }
    }
}
