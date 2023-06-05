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

        public async Task<Account> GetById(Guid Id)
        {
            var resp = await elasticClient.SearchAsync<Account>(
                x => x
                .Index(ElasticSettings.AccountsIndexName)
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
        /// <param name="account"></param>
        /// <returns></returns>
        public Task Save(Account account) =>
            elasticClient.IndexDocumentAsync(account);

        public Task Update(Account account) => elasticClient.UpdateByQueryAsync<Account>(q =>
                q.Query(q1 =>
                   q1.Bool(b => b.Must(m =>
                       m.Match(x => x.Field(f =>
                        f.Id == account.Id)))))
                   .Script(s =>
                        s.Source(
                            @"ctx._source.name = params.name;
                            ctx._source.personName = params.personName;
                            ctx._source.timeStamp = params.timeStamp;")
                        .Lang("painless")
                        .Params(p => p
                            .Add("name", account.Name)
                            .Add("personName", account.PersonName)
                            .Add("timeStamp", account.TimeStamp)
                )).Conflicts(Conflicts.Proceed));

        public Task Delete(string Id) => elasticClient.DeleteByQueryAsync<Account>(q => q
                                                   .Index(ElasticSettings.AccountsIndexName)
                                                   .Query(rq => rq
                                                       .Term(f => f.Id, Id)));
    }
}
