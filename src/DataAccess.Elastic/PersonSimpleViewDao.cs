using Nest;
using Queries.Core.dataaccess;
using Queries.Core.models;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Elastic
{
    public class PersonSimpleViewDao : IPersonSimpleViewDao
    {
        private readonly IElasticClient elasticClient;

        public PersonSimpleViewDao(IElasticClient elasticClient) => this.elasticClient = elasticClient;

        public async Task<IReadOnlyCollection<Person>> GetAll()
        {
            var resp = await elasticClient.SearchAsync<Person>(s => s
                .Index(ElasticSettings.PersonsIndexName)
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchAll()
                )
            );

            return resp.Documents;
        }

        public async Task Save(Person person)
        {
            var res = await elasticClient.IndexDocumentAsync(person);
        }
    }
}
