using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("FileRecommendations" , Schema = "dbo")]
public class FileRecommendations
{
    [Key]
    [Column(name:"Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public string? RecommendedReason { get; set; } = null;
    public DateTime RecommendedAt { get; set; } = DateTime.UtcNow;
    public Boolean IsUnused { get; set; } = false;
    
    [ForeignKey("fileId")]
    public virtual Files File { get; set; }
    
}