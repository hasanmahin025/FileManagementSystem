using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagement.Models.Entities;
 [Table("Backups" , Schema = "dbo")]
public class Backups
{
    [Key]
    [Column(name:"Id")]
    public Guid Id { get; set; }
    public  required Guid FileId{get;set;}
    public DateTime BackupTime{get;set;} = DateTime.Now;
    [MaxLength(100)]
    public required string BackupLocation{get;set;}
    public Boolean Restored{get;set;}
    
    [ForeignKey("FileId")]
    public virtual Files File{get;set;}
    
}