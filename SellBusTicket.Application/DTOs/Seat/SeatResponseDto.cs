using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.DTOs.Seat
{
    public class SeatResponseDto
    {
        public int Number { get; set; }
        public bool Available { get; set; }

        public SeatResponseDto(int number, bool available)
        {
            Number = number;
            Available = available;
        }
    }
}
