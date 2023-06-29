using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Reservation.Application.Features.Approver;
using Reservation.Application.Features.Assets;
using Reservation.Domain.Models;

namespace Reservation.WebApi.Controllers.Assets
{
    [Authorize]
    [Route(ApiRoutes.Assets.Route)]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ISender _sender;

        public AssetsController(ISender sender)
        {
            _sender = sender;
        }

        [RequiredScope("access_as_user")]
        [HttpPost]
        public Task<Asset> CreateAsset(CreateAssetCommand command)
        {
            return _sender.Send(command);
        }

        [HttpGet]
        public Task<List<Asset>> GetAssets([FromQuery] string keyword)
        {
            return _sender.Send(new GetAssetsQuery(keyword));
        }

        [HttpGet(ApiRoutes.Assets.GetAssetById)]
        public Task<Asset> GetAssetById([FromRoute] string id)
        {
            return _sender.Send(new GetAssetByIdQuery(id));
        }
    }
}
