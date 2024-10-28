using System.Data;

namespace AdSyst.Common.Application.Abstractions.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection OpenConnection();
    }
}
