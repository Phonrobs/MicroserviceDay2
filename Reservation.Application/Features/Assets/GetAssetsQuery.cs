using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Models;
using Reservation.Infrastructure.Abstracts;
using ShareLib.Abstracts;

namespace Reservation.Application.Features.Assets
{
    public record GetAssetsQuery(string Keyword) : IQuery<List<Asset>>;

    public class GetAssetsQueryHandler: IQueryHandler<GetAssetsQuery, List<Asset>>
    {
        private readonly IDataContext _db;

        public GetAssetsQueryHandler(IDataContext db)
        {
            _db = db;
        }

        public Task<List<Asset>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var q = _db.Assets.AsQueryable();

            if(!string.IsNullOrEmpty(request.Keyword))
            {
                q = q.Where(x => x.Name.Contains(request.Keyword));
            }

            return q.AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
