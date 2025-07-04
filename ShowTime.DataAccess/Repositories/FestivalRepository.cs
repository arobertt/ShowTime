using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories
{
    public class FestivalRepository : BaseRepository<Festival>
    {
        public FestivalRepository(ShowTimeDbContext context) : base(context)
        {
        }

        public async override Task<IEnumerable<Festival>> GetAllAsync()
        {
            try
            {
                return await _context.Festivals
                    .Include(f => f.Artists)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all festivals with artists and bookings: {ex.Message}", ex);
            }
        }
        public async override Task<Festival?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Festivals
                    .Include(f => f.Artists)
                    .FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving the festival with artists and bookings: {ex.Message}", ex);
            }
        }
    }
}
