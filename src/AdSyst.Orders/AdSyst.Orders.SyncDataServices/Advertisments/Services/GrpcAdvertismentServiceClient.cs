using System.Globalization;
using Grpc.Core;
using AdSyst.Advertisments.Api.Grpc;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Orders.SyncDataServices.Advertisments.Interfaces;
using AdSyst.Orders.SyncDataServices.Advertisments.Models;

namespace AdSyst.Orders.SyncDataServices.Advertisments.Services
{
    public class GrpcAdvertismentServiceClient : IAdvertismentServiceClient
    {
        private readonly AdvertismentService.AdvertismentServiceClient _advertsimentServiceClient;

        public GrpcAdvertismentServiceClient(
            AdvertismentService.AdvertismentServiceClient advertsimentServiceClient
        )
        {
            _advertsimentServiceClient = advertsimentServiceClient;
        }

        public async Task<AdvertismentSystemDto> GetAdvertismentSystemDataByIdAsync(
            Guid advertismentId,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var response = await _advertsimentServiceClient.GetAdvertismentSystemDataByIdAsync(
                    new GetAdvertismentSystemDataByIdRequest { AdvertismentId = advertismentId.ToString() },
                    cancellationToken: cancellationToken
                );
                var status = ConvertStatusToEnum(response.Status);
                return new(
                    Guid.Parse(response.Id),
                    Guid.Parse(response.UserId),
                    response.CreatedAt.ToDateTimeOffset(),
                    status
                );
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new NotFoundException(nameof(AdvertismentSystemDto), advertismentId);
            }
        }

        public async Task<AdvertismentDetailDto> GetAdvertismentDetailsAsync(
            Guid advertismentId,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var response = await _advertsimentServiceClient.GetAdvertismentDetailsAsync(
                    new GetAdvertismentDetailsRequest { AdvertismentId = advertismentId.ToString() },
                    cancellationToken: cancellationToken
                );
                var status = ConvertStatusToEnum(response.Status);

                decimal.TryParse(response.Price, CultureInfo.InvariantCulture, out decimal price);

                return new(
                    Guid.Parse(response.Id),
                    response.Title,
                    response.Description,
                    Guid.Parse(response.AdvertismentTypeId),
                    response.AdvertismentTypeName,
                    Guid.Parse(response.CategoryId),
                    response.CategoryName,
                    price,
                    response.CreatedAt.ToDateTimeOffset(),
                    status,
                    Guid.Parse(response.UserId),
                    response.ImageIds.Select(Guid.Parse).ToArray()
                );
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                throw new NotFoundException(nameof(AdvertismentDetailDto), advertismentId);
            }
        }

        private static AdvertismentStatus ConvertStatusToEnum(string advertismentStatusGrpc)
        {
            bool convertStatusResult = Enum.TryParse<AdvertismentStatus>(
                advertismentStatusGrpc,
                out var status
            );
            return !convertStatusResult
                ? throw new InvalidOperationException(
                    "Полученный статус объявления неизвестен системе"
                )
                : status;
        }
    }
}
