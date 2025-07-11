using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.DTOs;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;

namespace ShowTime.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        public BookingService(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task AddBookingAsync(BookingDto bookingDto)
        {
            try
            {
                var booking = new Booking
                {
                    Id = bookingDto.Id,
                    FestivalId = bookingDto.FestivalId,
                    UserId = bookingDto.UserId,
                    Type = bookingDto.Type,
                    Price = bookingDto.Price,
                    Quantity = bookingDto.Quantity 

                };
                await _bookingRepository.AddAsync(booking);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the artist.", ex);
            }
        }

        public async Task<IList<BookingDto>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                return bookings.Select(b => new BookingDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    FestivalId = b.FestivalId,
                    Type = b.Type,
                    Price = b.Price,
                    Quantity = b.Quantity 
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving bookings.", ex);
            }
        }
    }
}
