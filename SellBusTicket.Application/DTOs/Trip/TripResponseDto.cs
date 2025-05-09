using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.DTOs.Trip
{
    public class TripResponseDto
    {
        public Guid Id { get; set; }

        public TripResponseDto(Guid id)
        {
            Id = id;
        }
    }
}
