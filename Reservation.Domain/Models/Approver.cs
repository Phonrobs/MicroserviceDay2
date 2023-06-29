using Reservation.Domain.Types;

namespace Reservation.Domain.Models;

public class Approver
{
    public ApproverId ApproverId { get; private set; }
    public string EmailAddress { get; private set; }
    public List<ReservationItem> ReservationItems { get; private set; }
}
