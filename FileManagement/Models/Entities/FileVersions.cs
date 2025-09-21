using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("FileVersions" , Schema = "dbo")]
public class FileVersions
{
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public int VersionNumber { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } 
    [MaxLength(150)]
    public string ChangeSummary {get; set;}
    [MaxLength(150)]
    public string FileContent { get; set; }
    
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
    
    [ForeignKey("CreatedBy")]
    public virtual Users CreateBy { get; set; }    
    
    
}