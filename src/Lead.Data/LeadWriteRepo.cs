using Common.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Dapper;
using Lead.Data.DataModel;

namespace Lead.Data
{
    public class LeadWriteRepo: IWriteRepository<LeadModel>
    {
        private readonly ILogger<LeadWriteRepo> _logger;
        private readonly IConnectionProvider _provider;

        public LeadWriteRepo(ILoggerFactory loggerFactory, IConnectionProvider provider)
        {
            _logger = loggerFactory.CreateLogger<LeadWriteRepo>();
            _provider = provider;
        }
        public async Task<int> Add(LeadModel entity)
        {
            if (entity.LeadId == Guid.Empty)
                entity.LeadId = Guid.NewGuid();

            _logger.LogInformation("invoking Add(): LeadId: {0}", entity.LeadId);

            int result;
            using (var connection = await _provider.GetOpenDbConnection().ConfigureAwait(false))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"INSERT INTO Lead (
                                        LeadId, 
                                        Email, 
                                        FirstName, 
                                        LastName,
                                        StatusDate)";
                        query += @"VALUES (
                                        @LeadId, 
                                        @Email, 
                                        @FirstName, 
                                        @LastName,
                                        @StatusDate)";

                        result = await connection.ExecuteAsync(query, entity, transaction)
                            .ConfigureAwait(false);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }

            return result;
        }

        public Task<int> Delete(Guid id)
        {
            // TODO: Implement
            throw new System.NotImplementedException();
        }

        public Task<int> Update(LeadModel entity)
        {
            throw new NotImplementedException();
        }
    }
}