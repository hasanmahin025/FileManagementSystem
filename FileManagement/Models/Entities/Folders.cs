using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("Folders" , Schema = "dbo")]
public class Folders
{
   [MaxLength(150)]
   public string Name { get; set; }
   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   public DateTime? UpdatedAt { get; set; } = null;
   public Guid? ParentFolderId { get; set; } = null;
   public Guid OwnerId { get; set; }
   [Key]
   [Column("Id")]
   public Guid Id { get; set;}
   
   [ForeignKey("OwnerId")]
   public virtual Users Owner { get; set; }
   
   [ForeignKey("parentFolderId")]
   public virtual Folders? ParentFolder { get; set; }
   //one to many
   public virtual ICollection<Files> Files { get; set; } = new List<Files>();
   //one to many
   public virtual ICollection<Analytics>  Analytics { get; set; } = new List<Analytics>();
   
   public virtual ICollection<Folders> SubFolders { get; set; } = new List<Folders>();

   
}