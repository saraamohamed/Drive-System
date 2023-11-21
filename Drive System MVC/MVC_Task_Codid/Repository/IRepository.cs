namespace MVC_Task_Codid.Repository
{
    public interface IRepository<T>
    {
        List<T>? GetAll();
        T? GetById(int id);
        void Insert(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}
