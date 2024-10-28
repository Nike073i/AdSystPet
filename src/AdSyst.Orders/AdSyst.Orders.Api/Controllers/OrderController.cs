using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdSyst.Common.BusinessLayer.Exceptions;
using AdSyst.Common.Presentation.Consts;
using AdSyst.Common.Presentation.Controllers;
using AdSyst.Common.Presentation.Models;
using AdSyst.Orders.Api.Authorization.Orders.OrderCancellation;
using AdSyst.Orders.Api.Authorization.Orders.OrderCreation;
using AdSyst.Orders.Api.Authorization.Orders.OrderDetails;
using AdSyst.Orders.Api.Authorization.Orders.OrderMoving;
using AdSyst.Orders.Api.Logging.OrderEvents;
using AdSyst.Orders.Api.Models;
using AdSyst.Orders.Application.Advertisments.Queries.GetAdvertismentSystemData;
using AdSyst.Orders.Application.Orders.Commands.CancelOrder;
using AdSyst.Orders.Application.Orders.Commands.CreateOrder;
using AdSyst.Orders.Application.Orders.Commands.MoveOrder;
using AdSyst.Orders.Application.Orders.Queries.GetOrderDetails;
using AdSyst.Orders.Application.Orders.Queries.GetOrderList;
using AdSyst.Orders.Application.Orders.Queries.GetOrderSystemData;
using AdSyst.Orders.BusinessLayer.Exceptions;
using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Exceptions;

namespace AdSyst.Orders.Api.Controllers
{
    [Route("api/orders")]
    [Authorize(Roles = RoleNames.Client)]
    public class OrderController : BaseApiController
    {
        public OrderController(
            IMediator mediator,
            ILogger<OrderController> logger,
            IAuthorizationService authorizationService
        )
            : base(mediator, logger, authorizationService) { }

        [HttpGet("{id:mongoId}")]
        [ProducesResponseType(typeof(GetOrderDetailsQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                await AuthorizeAccessToOrderByOrderDetails(CanReadOrderDetailsPolicy.Name, id);

                var query = new GetOrderDetailsQuery(id, UserId);
                var response = await Mediator.Send(query, HttpContext.RequestAborted);
                Logger.OrderReceivedEventLog(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                Logger.OrderNotFoundEventLog(id);
                return NotFound(ex.Message);
            }
            catch (ForbiddenActionException ex)
            {
                Logger.OrderWasNotReceivedDueToAccessDeniedEventLog(id, UserId, ex.Message);
                return ForbiddenAction(ex.Message);
            }
        }

        [HttpGet("shoppings")]
        [ProducesResponseType(typeof(GetOrderListQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByBuyerId([FromQuery] PageRequest pageRequest)
        {
            var query = GetOrderListQuery.CreateQuery(
                pageNumber: pageRequest.PageNumber,
                pageSize: pageRequest.PageSize,
                buyerId: UserId
            );
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            Logger.UserShoppingsPageReceivedEventLog(
                response.PageNumber,
                response.Orders.Count(),
                UserId
            );
            return Ok(response);
        }

        [HttpPost("shoppings")]
        [ProducesResponseType(typeof(GetOrderListQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByBuyerIdWithOptions(
            [FromBody] GetOrdersWithOptionsRequest? request
        )
        {
            var query = GetOrderListQuery.CreateQuery(
                pageNumber: request?.PageNumber,
                pageSize: request?.PageSize,
                sortField: request?.SortField,
                sortDirection: request?.SortDirection,
                buyerId: UserId
            );
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            Logger.UserShoppingsPageByOptionsReceivedEventLog(UserId, request);
            return Ok(response);
        }

        [HttpGet("sales")]
        [ProducesResponseType(typeof(GetOrderListQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetBySellerId([FromQuery] PageRequest pageRequest)
        {
            var query = GetOrderListQuery.CreateQuery(
                pageNumber: pageRequest.PageNumber,
                pageSize: pageRequest.PageSize,
                sellerId: UserId
            );
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            Logger.UserSalesPageReceivedEventLog(
                response.PageNumber,
                response.Orders.Count(),
                UserId
            );
            return Ok(response);
        }

        [HttpPost("sales")]
        [ProducesResponseType(typeof(GetOrderListQueryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetBySellerIdWithOptions(
            [FromBody] GetOrdersWithOptionsRequest? request
        )
        {
            var query = GetOrderListQuery.CreateQuery(
                pageNumber: request?.PageNumber,
                pageSize: request?.PageSize,
                sortField: request?.SortField,
                sortDirection: request?.SortDirection,
                sellerId: UserId
            );
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            Logger.UserSalesPageByOptionsReceivedEventLog(UserId, request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                await AuthorizeAccessToOrderByAdvertisment(
                    OrderCreationPolicy.Name,
                    request.AdvertismentId
                );
                var command = new CreateOrderCommand(
                    request.AdvertismentId,
                    UserId,
                    request.AddressTo
                );
                string response = await Mediator.Send(command, HttpContext.RequestAborted);
                Logger.OrderCreatedEventLog(response, UserId);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                Logger.OrderWasNotCreatedDueToResourceNotFoundEventLog(
                    UserId,
                    request.AdvertismentId
                );
                return NotFound(ex.Message);
            }
            catch (OrderManagementException ex)
            {
                Logger.OrderWasNotCreatedDueToBusinessLogicViolationEventLog(
                    UserId,
                    request.AdvertismentId,
                    ex.Message
                );
                return UnprocessableEntity(ex.Message);
            }
            catch (ForbiddenActionException ex)
            {
                Logger.OrderWasNotCreatedDueToAccessDeniedEventLog(
                    UserId,
                    request.AdvertismentId,
                    ex.Message
                );
                return ForbiddenAction(ex.Message);
            }
        }

        [HttpPut("move/{orderId:mongoId}")]
        [ProducesResponseType(typeof(OrderStatus), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> MoveOrderStatus(string orderId)
        {
            try
            {
                await AuthorizeAccessToOrderByOrderDetails(CanMoveOrderPolicy.Name, orderId);
                var command = new MoveOrderCommand(UserId, orderId);
                var status = await Mediator.Send(command, HttpContext.RequestAborted);
                Logger.OrderStatusMovedEventLog(orderId, UserId);
                return Ok(status);
            }
            catch (NotFoundException ex)
            {
                Logger.OrderStatusWasNotMovedDueToResourceNotFoundEventLog(orderId, UserId);
                return NotFound(ex.Message);
            }
            catch (ForbiddenActionException ex)
            {
                Logger.OrderStatusWasNotMovedDueToAccessDeniedEventLog(orderId, UserId);
                return ForbiddenAction(ex.Message);
            }
            catch (UnmodifiableOrderStatusException ex)
            {
                Logger.OrderStatusWasNotMovedDueToIncorrectStateEventLog(
                    orderId,
                    UserId,
                    ex.Message
                );
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut("cancel/{orderId:mongoId}")]
        [ProducesResponseType(typeof(OrderStatus), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            try
            {
                await AuthorizeAccessToOrderByOrderDetails(OrderCancellationPolicy.Name, orderId);
                var command = new CancelOrderCommand(UserId, orderId);
                var status = await Mediator.Send(command, HttpContext.RequestAborted);
                Logger.OrderCanceledEventLog(orderId, UserId);
                return Ok(status);
            }
            catch (NotFoundException ex)
            {
                Logger.OrderWasNotCanceledDueToResourceNotFoundEventLog(orderId, UserId);
                return NotFound(ex.Message);
            }
            catch (ForbiddenActionException ex)
            {
                Logger.OrderWasNotCanceledDueToAccessDeniedEventLog(orderId, UserId);
                return ForbiddenAction(ex.Message);
            }
            catch (UnmodifiableOrderStatusException ex)
            {
                Logger.OrderWasNotCanceledDueToIncorrectStateEventLog(orderId, UserId, ex.Message);
                return UnprocessableEntity(ex.Message);
            }
        }

        private async Task AuthorizeAccessToOrderByOrderDetails(string policyName, string orderId)
        {
            var query = new GetOrderSystemDataQuery(orderId);
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            await AuthorizeAccessByResource(policyName, response.Order);
        }

        private async Task AuthorizeAccessToOrderByAdvertisment(
            string policyName,
            Guid advertismentId
        )
        {
            var query = new GetAdvertismentSystemDataQuery(advertismentId);
            var response = await Mediator.Send(query, HttpContext.RequestAborted);
            await AuthorizeAccessByResource(policyName, response.Advertisment);
        }
    }
}
