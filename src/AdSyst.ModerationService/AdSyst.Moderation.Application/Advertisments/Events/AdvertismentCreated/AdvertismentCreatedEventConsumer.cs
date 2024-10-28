using ErrorOr;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using AdSyst.Common.Contracts.Advertisments;
using AdSyst.Moderation.Application.Advertisments.Commands.CreateAdvertisment;

namespace AdSyst.Moderation.Application.Advertisments.Events.AdvertismentCreated
{
    public class AdvertismentCreatedEventConsumer : IConsumer<AdvertismentCreatedEvent>
    {
        private readonly ILogger<AdvertismentCreatedEventConsumer> _logger;
        private readonly IMediator _mediator;

        public AdvertismentCreatedEventConsumer(
            IMediator mediator,
            ILogger<AdvertismentCreatedEventConsumer> logger
        )
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AdvertismentCreatedEvent> context)
        {
            var eventModel = context.Message;
            _logger.AdvertismentCreatedEventConsumedEventLog(eventModel.AdvertismentId);

            var command = new CreateAdvertismentCommand(
                eventModel.AdvertismentId,
                eventModel.UserId
            );
            await _mediator
                .Send(command, context.CancellationToken)
                .SwitchFirst(
                    _ => _logger.AdvertismentAddedEventLog(eventModel.AdvertismentId),
                    (error) =>
                        _logger.AdvertismentAlreadyExistsEventLog(
                            eventModel.AdvertismentId,
                            eventModel.UserId,
                            error.Description
                        )
                );
        }
    }
}
