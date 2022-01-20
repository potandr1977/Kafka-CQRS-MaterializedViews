using Nest;
using Queries.Core.dataaccess;
using Queries.Core.models;
using Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Elastic
{
    public class PaymentSimpleViewDao : IPaymentSimpleViewDao
    {
        private readonly IElasticClient elasticClient;

        public PaymentSimpleViewDao(IElasticClient elasticClient) => this.elasticClient = elasticClient;

        public async Task<IReadOnlyCollection<Payment>> GetAll()
        {
            var resp = await elasticClient.SearchAsync<Payment>(s => s
                .Index(ElasticSettings.PaymentsIndexName)
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchAll()
                )
            );

            return resp.Documents;
        }

        public Task Save(Payment payment) =>
            elasticClient.IndexDocumentAsync(payment);
    }
}
