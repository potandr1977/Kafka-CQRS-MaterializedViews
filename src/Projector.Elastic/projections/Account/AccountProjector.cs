using DataAccess.DataAccess;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Account
{
    public class AccountProjector : IAccountProjector
    {
        private readonly IPersonDao _personDao;
        private readonly IAccountSimpleViewDao _accountSimpleViewDao;
        private readonly IKafkaAccountProducer _kafkaAccountProducer;

        public AccountProjector(
            IPersonDao personDao, 
            IAccountSimpleViewDao accountSimpleViewDao,
            IKafkaAccountProducer kafkaAccountProducer)
        {
            _personDao = personDao;
            _accountSimpleViewDao = accountSimpleViewDao;
            _kafkaAccountProducer = kafkaAccountProducer;
        }
        public async Task ProjectOne(UpdateAccountProjectionMessage message)
        {
            var personMongo = await _personDao.GetById(message.PersonId);

            var account = new Queries.Core.models.Account
            {
                Id = message.AccountId,
                Name = message.Name,
                PersonId = message.PersonId,
                PersonName = personMongo?.Name
            };
            try
            {
                await _accountSimpleViewDao.Save(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaAccountProducer.ProduceAsync(message, (int)PartitionEnum.QueriesApiFirst);
            await _kafkaAccountProducer.ProduceAsync(message, (int)PartitionEnum.QueriesApiSecond);
        }
    }
}
