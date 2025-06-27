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

        public override Task<IEnumerable<Festival>> GetAllAsync()
        {
            try
            {
                return base.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all festivals with artists and bookings: {ex.Message}", ex);
            }
        }
        public override Task<Festival?> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }
    }
}
