using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.DTOs;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;

namespace ShowTime.BusinessLogic.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;
        public ArtistService(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }
        public async Task AddArtistAsync(ArtistCreateDto artistCreateDto)
        {
            try
            {
                var artist = new Artist
                {
                    Name = artistCreateDto.Name,
                    Genre = artistCreateDto.Genre,
                    ImageUrl = artistCreateDto.ImageUrl
                };
                await _artistRepository.AddAsync(artist);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the artist.", ex);
            }
        }
        public async Task DeleteArtistAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                await _artistRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the artist with ID {id}.", ex);
            }
        }
        public async Task<IList<ArtistGetDto>> GetAllArtistsAsync()
        {
            try
            {
                var artists = await _artistRepository.GetAllAsync();
                return artists.Select(a => new ArtistGetDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Genre = a.Genre,
                    ImageUrl = a.ImageUrl
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving artists.", ex);
            }
        }

        public async Task<ArtistGetDto?> GetArtistByIdAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                return new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    ImageUrl = artist.ImageUrl
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the artist with ID {id}.", ex);
            }
        }
        public async Task UpdateArtistAsync(ArtistUpdateDto artistUpdateDto, int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }

                artist.Name = string.IsNullOrEmpty(artistUpdateDto.Name) ? artist.Name : artistUpdateDto.Name;
                artist.Genre = string.IsNullOrEmpty(artistUpdateDto.Genre) ? artist.Genre: artistUpdateDto.Genre;
                artist.ImageUrl = string.IsNullOrEmpty(artistUpdateDto.ImageUrl) ? artist.ImageUrl : artistUpdateDto.ImageUrl;

                var updatedArtist = await _artistRepository.UpdateAsync(artist);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the artist with ID {id}.", ex);
            }
        }
    }
}
