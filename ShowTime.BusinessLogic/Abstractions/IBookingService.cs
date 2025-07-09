using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.DTOs;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IBookingService
    {
        Task AddBookingAsync(BookingDto booking);
        Task<IList<BookingDto>> GetAllBookingsAsync();
    }
}
