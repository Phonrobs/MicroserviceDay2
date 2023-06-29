using Reservation.Domain.Types;

namespace Reservation.Domain.Models
{
    public class Reserver
    {
        public ReserverId ReserverId { get; private set; }
        public string EmailAddress { get; private set; }
        public List<ReservationItem> ReservationItems { get; private set; }
    }
}
