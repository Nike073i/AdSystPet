using System.Globalization;
using ErrorOr;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentDetail;
using AdSyst.Advertisments.Application.Advertisments.Queries.GetAdvertismentSystemData;

namespace AdSyst.Advertisments.Api.Grpc
{
    public class AdvertismentServiceServer : AdvertismentService.AdvertismentServiceBase
    {
        private readonly IMediator _mediator;

        public AdvertismentServiceServer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetAdvertismentDetailsResponse> GetAdvertismentDetails(
            GetAdvertismentDetailsRequest request,
            ServerCallContext context
        )
        {
            var adId = Guid.Parse(request.AdvertismentId);
            var query = new GetAdvertismentDetailQuery(adId);

            var result = await _mediator.Send(query, context.CancellationToken);

            return result.MatchFirst(
                onValue: data =>
                    new GetAdvertismentDetailsResponse()
                    {
                        Id = data.Id.ToString(),
                        CreatedAt = Timestamp.FromDateTimeOffset(data.CreatedAt),
                        UserId = data.UserId.ToString(),
                        Status = data.Status.ToString(),
                        AdvertismentTypeId = data.AdvertismentTypeId.ToString(),
                        AdvertismentTypeName = data.AdvertismentTypeName,
                        CategoryId = data.CategoryId.ToString(),
                        CategoryName = data.CategoryName,
                        Description = data.Description,
                        ImageIds = { data.ImageIds.Select(guid => guid.ToString()) },
                        Price = data.Price.ToString(CultureInfo.InvariantCulture),
                        Title = data.Title,
                    },
                onFirstError: ThrowNotFoundException<GetAdvertismentDetailsResponse>
            );
        }

        public override async Task<GetAdvertismentSystemDataByIdResponse> GetAdvertismentSystemDataById(
            GetAdvertismentSystemDataByIdRequest request,
            ServerCallContext context
        )
        {
            var adId = Guid.Parse(request.AdvertismentId);
            var query = new GetAdvertismentSystemDataQuery(adId);

            var result = await _mediator.Send(query, context.CancellationToken);

            return result.MatchFirst(
                onValue: data =>
                    new GetAdvertismentSystemDataByIdResponse()
                    {
                        Id = data.Id.ToString(),
                        CreatedAt = Timestamp.FromDateTimeOffset(data.CreatedAt),
                        UserId = data.UserId.ToString(),
                        Status = data.Status.ToString()
                    },
                onFirstError: ThrowNotFoundException<GetAdvertismentSystemDataByIdResponse>
            );
        }

        private static TResponse ThrowNotFoundException<TResponse>(Error error) =>
            throw new RpcException(new Status(StatusCode.NotFound, error.Description));
    }
}
