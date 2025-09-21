using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
[Table("Files" , Schema = "dbo")]
public class Files
{
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(100)]
    public required string Type { get; set; } = "unknown";
    
    public int size { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public Guid OwnerId { get; set; }
    public Boolean IsArchived { get; set; } = false;
    public Boolean IsDeleted { get; set; } = false;
    public Boolean IsFavorite { get; set; } = false;
    [MaxLength(100)] 
    public string StorageLocation { get; set; } = "local";
    public Guid? AnalyticsId { get; set; }
    public Guid? ParentFileId { get; set; }
    
    [Key]
    [Column("Id")]
    public required Guid Id { get; set; }
    public Guid? FolderId { get; set; }
    
    [ForeignKey("OwnerId")]
    public virtual Users Owner { get; set; }
    
    [ForeignKey("ParentFileId")]
    public virtual Files ParentFile { get; set; }
      
    [ForeignKey("analyticsId")]
    public virtual Analytics Analytics { get; set; }
    
    [ForeignKey("FolderId")]
    public virtual Folders Folder { get; set; }
    
    public virtual ICollection<Files> SubFiles { get; set; } = new List<Files>();
    
    public virtual ICollection<FileSecurity> FileSecurities { get; set; } = new List<FileSecurity>();
    
    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
    
    //public virtual ICollection<FileRecommendations> FileRecommendations { get; set; }
    public virtual ICollection<Backups> Backup { get; set; } = new List<Backups>();
    public virtual ICollection<FileRecommendations> Recommendations { get; set; } = new List<FileRecommendations>();
    
    public virtual ICollection<SyncLogs>  SyncLogs { get; set; } = new List<SyncLogs>();
    
    public virtual ICollection<FileVersions> FileVersions { get; set; } = new List<FileVersions>();
    
}
 