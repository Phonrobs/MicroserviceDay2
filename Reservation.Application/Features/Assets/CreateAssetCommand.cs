using Reservation.Domain.Models;
using Reservation.Domain.Types;
using Reservation.Infrastructure.Abstracts;
using ShareLib.Abstracts;

namespace Reservation.Application.Features.Approver;

public record CreateAssetCommand(string Name, int AllItemCount) : ICommand<Asset>;

public class CreateAssetCommandHandler : ICommandHandler<CreateAssetCommand, Asset>
{
    private readonly IDataContext _db;

    public CreateAssetCommandHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var newItem = new Asset(AssetId.Create(), request.Name, request.AllItemCount, request.AllItemCount);

        await _db.Assets.AddAsync(newItem, cancellationToken);
        await _db.SaveAsync(cancellationToken);

        return newItem;
    }
}