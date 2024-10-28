using System.Data;
using Dapper;
using AdSyst.Advertisments.Infrastructure.Messaging.Models;
using AdSyst.Common.Application.Abstractions.Clock;

namespace AdSyst.Advertisments.Infrastructure.Messaging.Persistence
{
    public abstract class EventMessageRepository : IEventMessageRepository
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        protected abstract string TableName { get; }

        public EventMessageRepository(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<IReadOnlyList<EventMessageResponse>> GetWithLock(
            int batchSize,
            IDbTransaction transaction,
            IDbConnection connection
        )
        {
            string sql = $"""
                SELECT 
                    TOP (@batchSize)
                    id, content
                FROM {TableName} WITH (UPDLOCK)
                WHERE ProcessedOnUtc IS NULL
                ORDER BY OccurredOnUtc
            """;
            var messages = await connection.QueryAsync<EventMessageResponse>(
                sql,
                new { batchSize },
                transaction
            );
            return messages.ToList();
        }

        public async Task UpdateMessageStatus(
            Guid id,
            string? error,
            IDbTransaction transaction,
            IDbConnection connection
        )
        {
            string sql = $"""
                UPDATE {TableName}
                SET Error = @error, 
                    ProcessedOnUtc = @processedOnUtc
                WHERE Id = @id
            """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    error,
                    id,
                    processedOnUtc = _dateTimeProvider.UtcWithOffsetNow
                },
                transaction
            );
        }
    }
}
