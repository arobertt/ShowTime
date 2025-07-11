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
    public class LineupService : ILineupService
    {
        private readonly IRepository<Lineup> _lineupRepository;
        public LineupService(IRepository<Lineup> lineupRepository)
        {
            _lineupRepository = lineupRepository;
        }
        public Task AddLineUpAsync(LineupDto lineupCreateDto)
        {
            try
            {
                var lineup = new Lineup
                {
                    FestivalId = lineupCreateDto.FestivalId,
                    ArtistId = lineupCreateDto.ArtistId,
                    Stage = lineupCreateDto.Stage,
                    StartTime = lineupCreateDto.StartTime
                };
                return _lineupRepository.AddAsync(lineup);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the lineup.", ex);
            }
        }

        public async Task<IList<LineupDto>> GetAllLineupsAsync()
        {
            try
            {
                var lineups = await _lineupRepository.GetAllAsync();
                return lineups.Select(l => new LineupDto
                {
                    FestivalId = l.FestivalId,
                    ArtistId = l.ArtistId,
                    Stage = l.Stage,
                    StartTime = l.StartTime,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving lineups.", ex);
            }
        }
    }
}
