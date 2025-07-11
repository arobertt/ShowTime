using ShowTime.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface ILineupService
    {
        Task<IList<LineupDto>> GetAllLineupsAsync();
        Task AddLineUpAsync (LineupDto lineupCreateDto);
    }
}
