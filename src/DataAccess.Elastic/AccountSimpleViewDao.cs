using Nest;
using Queries.Core.dataaccess;
using Queries.Core.models;
using Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Elastic
{
    public class AccountSimpleViewDao : IAccountSimpleViewDao
    {
        private readonly IElasticClient elasticClient;

        public AccountSimpleViewDao(IElasticClient elasticClient) => this.elasticClient = elasticClient;

        public async Task<IReadOnlyCollection<Account>> GetAll()
        {
            var resp = await elasticClient.SearchAsync<Account>(s => s
                .Index(ElasticSettings.AccountsIndexName)
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchAll()
                )
            );

            return resp.Documents;
        }

        public Task Save(Account account) =>
            elasticClient.IndexDocumentAsync(account);
    }
}
