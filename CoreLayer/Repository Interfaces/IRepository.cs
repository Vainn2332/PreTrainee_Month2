namespace PreTrainee_Month2.CoreLayer.Repository_Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAsync();
        public Task<T> Get(int id);
        public Task AddAsync(T entity);
        public Task UpdateAsync(int id, T newEntity);
        public Task DeleteAsync(int id);
    }
}
