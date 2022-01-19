using Domain.DataAccess;
using Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Account
{
    public class AccountProjector : IAccountProjector
    {

        private readonly IAccountDao accountDao;
        private readonly IAccountSimpleViewDao accountSimpleViewDao;

        public AccountProjector()
        { 

        }
        public Task ProjectOne(UpdateAccountProjectionMessage message)
        {
            throw new NotImplementedException();

            /*
            var author = await authorDao.GetAuthorById(message.AuthorId);
            var authorMessage = new AuthorMessage
            {
                Id = message.Id,
                AuthorId = message.AuthorId,
                AuthorName = author.Name,
                MessageText = message.Text
            };

            await authorMessagesDao.Save(authorMessage);
            */
        }
    }
}
