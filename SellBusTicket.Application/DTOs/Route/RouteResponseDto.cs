namespace SellBusTicket.Application.DTOs.Route
{
    public class RouteResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arival { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public RouteResponseDto(Guid id, DateTime departure, DateTime arival, string origin, string destination)
        {
            Id = id;
            Departure = departure;
            Arival = arival;
            Origin = origin;
            Destination = destination;
        }
    }
}
