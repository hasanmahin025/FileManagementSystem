using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("Analytics" , Schema = "dbo")]
public class Analytics
{
    [Key]
    [Column("Id")]
    public required Guid Id { get; set; }
    public required int TotalStorageUsed { get; set; }
    public required int FileCount { get; set; } = 0; 
    public required DateTime LastAnalyzed { get; set; }  = DateTime.UtcNow;
    public required Guid FolderId { get; set; }
    
    [ForeignKey("FolderId")]
    public virtual Folders Folder { get; set;}
   
    //one to one
    
    public virtual Files File { get; set; }
    
   
    
}