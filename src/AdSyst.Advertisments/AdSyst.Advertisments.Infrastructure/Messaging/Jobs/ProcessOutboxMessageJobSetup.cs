using Microsoft.Extensions.Options;
using AdSyst.Advertisments.Infrastructure.Messaging.Settings;
using Quartz;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Jobs
{
    public class ProcessOutboxMessageJobSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly EventProcessOptions _options;

        public ProcessOutboxMessageJobSetup(EventProcessOptions options)
        {
            _options = options;
        }

        public void Configure(QuartzOptions options)
        {
            const string jobName = nameof(ProcessOutboxMessageJob);

            options
                .AddJob<ProcessOutboxMessageJob>(configure => configure.WithIdentity(jobName))
                .AddTrigger(
                    configure =>
                        configure
                            .ForJob(jobName)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule
                                        .WithIntervalInSeconds(_options.IntervalInSeconds)
                                        .RepeatForever()
                            )
                );
        }
    }
}
