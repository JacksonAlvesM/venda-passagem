using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.DTOs.Place
{
    public class PlaceResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PlaceResponseDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
