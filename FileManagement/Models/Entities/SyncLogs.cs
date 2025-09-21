using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("SyncLogs" , Schema = "dbo")]
public class SyncLogs
{
    [Key]
    [Column(name:"Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set;}
    public Guid UserId { get; set; }
    [MaxLength(50)]
    public required string SyncType { get; set; }
    public DateTime SyncTime { get; set; } = DateTime.UtcNow;
    [MaxLength(50)]
    public required string Status { get; set; }
    
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
    
    [ForeignKey("UserId")]
    public virtual Users User { get; set; }
}