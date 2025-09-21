using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("FileSecurity" , Schema = "dbo")]
public class FileSecurity
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    [MaxLength(100)]
    public required string PermissionLevel { get; set; } = "read";
    [MaxLength(100)]
    public Guid SharedWithUserId { get; set; }
    
    [ForeignKey("fileId")]
    public virtual Files File { get; set; }
    
    [ForeignKey("sharedWithUserId")]
    public virtual Users SharedWithUser { get; set; }
}