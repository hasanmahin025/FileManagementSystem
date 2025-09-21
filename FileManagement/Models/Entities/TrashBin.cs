using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("TrashBins" , Schema = "dbo")]
public class TrashBin
{
    [Key]
    [Column ("Id")]
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public Guid DeletedBy { get; set; }
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
    public Boolean Restored { get; set;}
    
    [ForeignKey("FileId")]
    public virtual Files File { get; set; }
    [ForeignKey("DeletedBy")]
    public virtual Users Deleted { get; set; }
    
    
}