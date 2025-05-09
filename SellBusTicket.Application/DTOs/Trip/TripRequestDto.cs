using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SellBusTicket.Application.DTOs.Trip
{
    public class TripRequestDto
    {
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("route_id")]
        public Guid RouteId { get; set; }

        [JsonPropertyName("seat")]
        public int Seat { get; set; }
    }
}
