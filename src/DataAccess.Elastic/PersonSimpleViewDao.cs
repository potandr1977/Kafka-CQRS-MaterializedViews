﻿using Elasticsearch.Net;
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

        public async Task<Person> GetById(Guid Id)
        {
            var resp = await elasticClient.SearchAsync<Person>(
                x => x
                .Index(ElasticSettings.PersonsIndexName)
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
        public Task Save(Person person) =>
            elasticClient.IndexDocumentAsync(person);


        public Task Update(Person person) =>
            elasticClient.UpdateByQueryAsync<Person>(q =>
                q.Query(q1 =>
                   q1.Bool(b => b.Must(m =>m
                    .Match(x => x
                       .Field(f =>
                        f.Id == person.Id)))))
                    .Script(s => s
                        .Source(
                            @"ctx._source.name = params.name;
                            ctx._source.inn = params.inn;
                            ctx._source.timeStamp = params.timeStamp;")
                        .Lang("painless")
                        .Params(p => p
                            .Add("name", person.Name)
                            .Add("inn", person.Inn)
                            .Add("timeStamp", person.TimeStamp)
                )).Conflicts(Conflicts.Proceed));


        public Task Delete(string Id) => elasticClient.DeleteByQueryAsync<Person>(q => q
                                                    .Index(ElasticSettings.PersonsIndexName)
                                                    .Query(rq => rq
                                                        .Term(f => f.Id, Id)));
    }
}
