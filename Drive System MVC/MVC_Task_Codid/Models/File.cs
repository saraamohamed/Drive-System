using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Task_Codid.Models
{
    public class File
    {
        [Key]
        public int DocumentId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength]
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }

        [ForeignKey("User")]
        public int  UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
