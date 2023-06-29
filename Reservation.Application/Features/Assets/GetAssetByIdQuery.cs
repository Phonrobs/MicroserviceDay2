using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Models;
using Reservation.Domain.Types;
using Reservation.Infrastructure.Abstracts;
using ShareLib.Abstracts;

namespace Reservation.Application.Features.Assets
{
    public record GetAssetByIdQuery(string Id) : IQuery<Asset>;

    public class GetAssetByIdQueryHandler : IQueryHandler<GetAssetByIdQuery, Asset>
    {
        private readonly IDataContext _db;

        public GetAssetByIdQueryHandler(IDataContext db)
        {
            _db = db;
        }

        public Task<Asset> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return _db.Assets
                .Where(x => x.AssetId == new AssetId(request.Id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
