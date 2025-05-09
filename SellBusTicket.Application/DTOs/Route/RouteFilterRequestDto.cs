using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SellBusTicket.Application.DTOs.Route
{
    public class RouteFilterRequestDto
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("origin_id")]
        public Guid OriginId { get; set; }

        [JsonPropertyName("destination_id")]
        public Guid DestinationId { get; set; }
    }
}
