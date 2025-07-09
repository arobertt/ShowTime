using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.DTOs;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly IRepository<Festival> _fesivalRepository;
        public FestivalService(IRepository<Festival> festivalRepository)
        {
            _fesivalRepository = festivalRepository;
        }
        public async Task AddFestivalAsync(FestivalCreateDto festival)
        {
            try
            { 
                var newFestival = new Festival
                {
                    Name = festival.Name,
                    Location = festival.Location,
                    Capacity = festival.Capacity,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt
                };
                await _fesivalRepository.AddAsync(newFestival);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the festival.", ex);
            }
        }
        public async Task DeleteFestivalAsync(int id)
        {
            try
            {
                var festival = await _fesivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                await _fesivalRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the festival with ID {id}.", ex);
            }
        }

        public async Task<IList<FestivalGetDto>> GetAllFestivalsAsync()
        {
            try
            {
                var festivals = await _fesivalRepository.GetAllAsync();
                return festivals.Select(f => new FestivalGetDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Location = f.Location,
                    StartDate = f.StartDate,
                    EndDate = f.EndDate,
                    SplashArt = f.SplashArt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all festivals.", ex);
            }
        }

        public async Task<FestivalGetDto?> GetFestivalByIdAsync(int id)
        {
            try
            {
                var festival = await _fesivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    return null; // or throw an exception if preferred
                }
                return new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the festival with ID {id}.", ex);
            }
        }

        public async Task<IList<FestivalGetDto>> GetFestivalsByArtistIdAsync(int artistId)
        {
            try
            {
                var festivals = await _fesivalRepository.GetAllAsync();
                var artistFestivals = festivals.Where(f => f.Artists.Any(a => a.Id == artistId)).ToList();
                return artistFestivals.Select(f => new FestivalGetDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Location = f.Location,
                    StartDate = f.StartDate,
                    EndDate = f.EndDate,
                    SplashArt = f.SplashArt
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving festivals for artist with ID {artistId}.", ex);
            }
        }

        public async Task UpdateFestivalAsync(FestivalCreateDto festival, int id)
        {
            try
            {
                var existingFestival = await _fesivalRepository.GetByIdAsync(id);
                if (existingFestival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                existingFestival.Name = festival.Name;
                existingFestival.Location = festival.Location;
                existingFestival.StartDate = festival.StartDate;
                existingFestival.EndDate = festival.EndDate;
                existingFestival.SplashArt = festival.SplashArt;
                await _fesivalRepository.UpdateAsync(existingFestival);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the festival with ID {id}.", ex);
            }
        }
    }
}
