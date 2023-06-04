using DataAccess.DataAccess;
using EventBus.Kafka.Abstraction;
using Messages.Account;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Account
{
    public class AccountProjector : IAccountProjector
    {
        private readonly IPersonDao _personDao;
        private readonly IAccountSimpleViewDao _accountSimpleViewDao;
        private readonly IKafkaProducer<UpdateAccountProjectionMessage> _kafkaUpdateAccountProducer;
        private readonly IKafkaProducer<SaveAccountProjectionMessage> _kafkaSaveAccountProducer;
        private readonly IKafkaProducer<DeleteAccountProjectionMessage> _kafkaDeleteAccountProducer;

        public AccountProjector(
            IPersonDao personDao, 
            IAccountSimpleViewDao accountSimpleViewDao,
            IKafkaProducer<UpdateAccountProjectionMessage> kafkaAccountProducer,
            IKafkaProducer<SaveAccountProjectionMessage> kafkaSaveAccountProducer,
            IKafkaProducer<DeleteAccountProjectionMessage> kafkaDeleteAccountProducer)
        {
            _personDao = personDao;
            _accountSimpleViewDao = accountSimpleViewDao;
            _kafkaUpdateAccountProducer = kafkaAccountProducer;
            _kafkaSaveAccountProducer = kafkaSaveAccountProducer;
            _kafkaDeleteAccountProducer = kafkaDeleteAccountProducer;
        }
        public async Task ProjectOne(UpdateAccountProjectionMessage message)
        {
            var personMongo = await _personDao.GetById(message.PersonId);

            var account = new Queries.Core.models.Account
            {
                Id = message.AccountId,
                Name = message.Name,
                PersonId = message.PersonId,
                PersonName = personMongo?.Name,
                TimeStamp= message.TimeStamp,
            };
            try
            {
                await _accountSimpleViewDao.Update(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaUpdateAccountProducer.ProduceAsync(message);
        }

        public async Task ProjectOne(SaveAccountProjectionMessage message)
        {
            var personMongo = await _personDao.GetById(message.PersonId);

            var account = new Queries.Core.models.Account
            {
                Id = message.AccountId,
                Name = message.Name,
                PersonId = message.PersonId,
                PersonName = personMongo?.Name,
                TimeStamp = message.TimeStamp,
            };
            try
            {
                await _accountSimpleViewDao.Save(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaSaveAccountProducer.ProduceAsync(message);
        }

        public async Task ProjectOne(DeleteAccountProjectionMessage message)
        {
            try
            {
                await _accountSimpleViewDao.Delete(message.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaDeleteAccountProducer.ProduceAsync(message);
        }
    }
}
