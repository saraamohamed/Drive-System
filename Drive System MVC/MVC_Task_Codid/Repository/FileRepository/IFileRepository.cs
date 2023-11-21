namespace MVC_Task_Codid.Repository.FileRepository
{
    public interface IFileRepository:IRepository<Models.File>
    {
       List<Models.File> GetAllByUserId(int userId);
    }
}
