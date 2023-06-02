using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.dataaccess
{
    public interface IDao<T>
    {
        public Task<List<T>> GetAll();

        public Task<(int totalPages, IReadOnlyList<T> data)> GetPage(int page, int pageSize);

        public Task<T> GetById(Guid id);

        public Task DeleteById(Guid id);

        public Task CreateAsync(T model);

        public Task<T> UpdateAsync(T model);
    }
}
