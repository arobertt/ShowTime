using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories
{
    public class ArtistReposistory : BaseRepository<Artist>
    {
        public ArtistReposistory(ShowTimeDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Artist>> GetAllAsync()
        {
            try
            {
                return await _context.Artists
                    .Include(a => a.Festivals)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all artists with festivals: {ex.Message}", ex);
            }
        }
        public override async Task<Artist?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Artists
                    .Include(a => a.Festivals)
                    .FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving the artist with festivals: {ex.Message}", ex);
            }
        }
    }
}
