using ShowTime.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IFestivalService
    {
        Task<FestivalGetDto?> GetFestivalByIdAsync(int id);
        Task<IList<FestivalGetDto>> GetAllFestivalsAsync();
        Task AddFestivalAsync(FestivalCreateDto festival);
        Task UpdateFestivalAsync(FestivalCreateDto festival, int id);
        Task DeleteFestivalAsync(int id);
        Task<IList<FestivalGetDto>> GetFestivalsByArtistIdAsync(int artistId);
    }
}
