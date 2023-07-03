using Elasticsearch.Net;
using Nest;
using Queries.Core.dataaccess;
using Queries.Core.models;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Payment> GetById(Guid Id)
        {
            var resp = await elasticClient.SearchAsync<Payment>(
                x => x
                .Index(ElasticSettings.PaymentsIndexName)
                .Query(
                    q1 => q1.Bool(
                            b => b.Must(
                                m => m.Terms(
                                    t => t.Field(f => f.Id)
                    .Terms(Id))))));

            return resp.Documents.FirstOrDefault();
        }

        /// <summary>
        /// Вызывается только из проектора. Can be invoked from projector only.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Task Save(Payment payment) =>
                    elasticClient.IndexDocumentAsync(payment);

        public Task Update(Payment payment) => elasticClient.UpdateByQueryAsync<Payment>(q =>
                q.Query(q1 =>
                   q1.Bool(b => b.Must(m =>
                       m.Match(x => x.Field(f =>
                        f.Id == payment.Id)))))
                   .Script(s =>
                        s.Source(
                            @"ctx._source.accountName = params.accountName;
                            ctx._source.sum = params.sum;
                            ctx._source.paymentType = params.paymentType;
                            ctx._source.timeStamp = params.timeStamp;")
                        .Lang("painless")
                        .Params(p => p
                             .Add("accountName", payment.AccountName)
                             .Add("sum", payment.Sum)
                             .Add("paymentType", payment.PaymentTypeName)
                             .Add("timeStamp", payment.TimeStamp)
                )).Conflicts(Conflicts.Proceed));

        public Task Delete(string Id) => elasticClient.DeleteByQueryAsync<Payment>(q => q
                                                   .Index(ElasticSettings.PaymentsIndexName)
                                                   .Query(rq => rq
                                                       .Term(f => f.Id, Id)));
    }
}
