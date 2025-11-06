namespace APIMMwithoutJunctionModel.Interface
{
    public interface IDocPatient<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);

    }
}
