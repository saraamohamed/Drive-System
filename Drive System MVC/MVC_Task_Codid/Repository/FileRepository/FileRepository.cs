using Microsoft.EntityFrameworkCore;
using MVC_Task_Codid.Models;

namespace MVC_Task_Codid.Repository.FileRepository
{
    public class FileRepository:IFileRepository
    {
        DriveDbContext dbContext = new DriveDbContext();

        public void Delete(int id)
        {
            Models.File document = GetById(id);
            if (document != null)
            {
                document.IsDeleted = true;
                dbContext.SaveChanges();
            }

        }

        public List<Models.File>? GetAll()
        {
            return dbContext.Files.Where(c => c.IsDeleted != true).ToList();
        }

        public List<Models.File>? GetAllByUserId(int userId)
        {
            return dbContext.Files.Where(c => c.IsDeleted != true && c.UserId == userId).ToList();
        }

        public Models.File? GetById(int id)
        {
            return dbContext.Files.FirstOrDefault(c => c.DocumentId == id);
        }

        public void Insert(Models.File entity)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Models.File entity)
        {
            throw new NotImplementedException();
        }
    }
}
