using FileManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManagement.Data;

public class ApplicationDbContext : DbContext
{
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
     {
          
     }
     public DbSet<FileVersions> FileVersions { get; set; }
     public DbSet<SyncLogs> SyncLogs { get; set; }
     public DbSet<Files> Files { get; set; }
     public DbSet<Users> Users { get; set; }
     public DbSet<FileRecommendations> FileRecommendations { get; set; }
     public DbSet<Backups> Backups { get; set; }
     public DbSet<Analytics>  Analytics { get; set; }
     public DbSet<TrashBin> TrashBins { get; set; }
     public DbSet<Folders> Folders { get; set; }
     public DbSet<FileSecurity> FileSecurity { get; set; }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {



         base.OnModelCreating(modelBuilder);
         {
             modelBuilder.Entity<Users>( entity =>
             {
                 entity.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
                 entity.Property(u => u.IsActive).HasDefaultValue(true);
             });

             modelBuilder.Entity<Files>(entity =>
             {
                
                 entity.Property(f => f.Type).HasDefaultValue("unknown");
                 entity.Property(f => f.StorageLocation).HasDefaultValue("local");
                 entity.Property(f => f.CreatedAt).HasDefaultValueSql("now()");
                 entity.Property(f => f.size).HasDefaultValue(0);
                 entity.Property(f => f.IsArchived).HasDefaultValue(false);
                 entity.Property(f => f.IsDeleted).HasDefaultValue(false);
                 entity.Property(f => f.IsFavorite).HasDefaultValue(false);
                 
                 
                 entity.HasOne(f => f.Owner)
                       .WithMany(u => u.Files)
                       .HasForeignKey(f => f.OwnerId)
                       .OnDelete(DeleteBehavior.Cascade);
                 entity
                     .HasOne(f => f.Analytics)
                     .WithOne(a => a.File)
                     .HasForeignKey<Files>(f => f.AnalyticsId)
                     .OnDelete(DeleteBehavior.Cascade);
                 entity
                     .HasOne(f =>f.ParentFile)
                     .WithMany(f => f.SubFiles)
                     .HasForeignKey(f=> f.ParentFileId)
                     .OnDelete(DeleteBehavior.Cascade);
                 entity
                     .HasOne(f => f.Folder)
                     .WithMany(f => f.Files)
                     .HasForeignKey(f => f.FolderId)
                     .OnDelete(DeleteBehavior.Cascade);
             });
             
             modelBuilder.Entity<FileVersions>( entity =>
             {
               entity.Property(fv => fv.VersionNumber).HasDefaultValue(0);
               entity.Property(fv => fv.CreatedAt).HasDefaultValueSql("now()");

               entity.HasOne(fv => fv.File)
                   .WithMany(fv => fv.FileVersions)
                   .HasForeignKey(fv => fv.FileId)
                   .OnDelete(DeleteBehavior.Cascade);

               entity.HasOne(fv => fv.CreateBy)
                   .WithMany(fv => fv.FileVersions)
                   .HasForeignKey(fv => fv.CreatedBy)
                   .OnDelete(DeleteBehavior.Cascade);

             });

             modelBuilder.Entity<FileSecurity>(entity =>
             {
                 entity.Property(fs => fs.PermissionLevel)
                     .HasDefaultValue("read");
                 
                 entity.HasOne(fs => fs.SharedWithUser)
                     .WithMany(fs => fs.FileSecurity)
                     .HasForeignKey(fs => fs.SharedWithUserId)
                     .OnDelete(DeleteBehavior.Cascade);
                 entity.HasOne(fs => fs.File)
                     .WithMany(fs => fs.FileSecurities)
                     .HasForeignKey(fs => fs.FileId)
                     .OnDelete(DeleteBehavior.Cascade);
             });

             modelBuilder.Entity<FileRecommendations>( entity =>
             {
                 entity.Property(fr => fr.RecommendedReason)
                     .HasDefaultValue(null)
                     .HasMaxLength(200);
                 entity.Property(fr => fr.RecommendedAt)
                     .HasDefaultValueSql("now()");
                 entity.Property(fr => fr.IsUnused)
                     .HasDefaultValue(false); 

                 entity.HasOne(fr => fr.File)
                     .WithMany(fr => fr.Recommendations)
                     .HasForeignKey(fr => fr.FileId)
                     .OnDelete(DeleteBehavior.Cascade);
                 

             });

             modelBuilder.Entity<Backups>(entity =>
             {
                 entity.Property(b => b.BackupTime)
                     .HasDefaultValueSql("now()");

                 entity.HasOne(b => b.File)
                     .WithMany(b => b.Backup)
                     .HasForeignKey(b => b.FileId);
             });

             modelBuilder.Entity<Analytics>( entity =>
             {
                 entity.Property(a => a.FileCount)
                      .HasDefaultValue(0);
                 entity.Property(a => a.LastAnalyzed)
                     .HasDefaultValueSql("now()");

                 entity.HasOne(a => a.Folder)
                     .WithMany(a => a.Analytics)
                     .HasForeignKey(a => a.FolderId)
                     .OnDelete(DeleteBehavior.Cascade);
                 
             });
             modelBuilder.Entity<Folders>( entity =>
             {
                 entity.Property(fo => fo.CreatedAt)
                     .HasDefaultValueSql("now()");
                 entity.Property(fo => fo.UpdatedAt)
                      .HasDefaultValueSql("now()");
                  entity.Property(fo => fo.ParentFolderId)
                     .HasDefaultValue(null);
                 
                 entity.HasOne(fo => fo.Owner)
                     .WithMany(fo => fo.Folders)
                     .HasForeignKey(fo => fo.OwnerId)
                     .OnDelete(DeleteBehavior.Cascade);
                 
                 entity.HasOne(fo =>  fo.ParentFolder)
                     .WithMany(fo => fo.SubFolders)
                     .HasForeignKey(fo => fo.ParentFolderId)
                     .OnDelete(DeleteBehavior.Cascade);
             });

             modelBuilder.Entity<SyncLogs>(entity =>
             {
                 entity.Property(s => s.SyncTime)
                     .HasDefaultValueSql("now()");
                 
                 entity.HasOne(s => s.File)
                       .WithMany(s => s.SyncLogs)
                       .HasForeignKey(fs => fs.FileId)
                       .OnDelete(DeleteBehavior.Cascade);
                 entity.HasOne(s => s.User)
                     .WithMany(s => s.SyncLogs)
                     .HasForeignKey(fs => fs.UserId)
                     .OnDelete(DeleteBehavior.Cascade);
             });

             modelBuilder.Entity<TrashBin>(entity =>
             {
                 entity.Property(t => t.DeletedAt)
                     .HasDefaultValueSql("now()");
                 entity.Property(t => t.Restored)
                     .HasDefaultValue(false);
                 entity.HasOne(t => t.File)
                     .WithMany(t => t.TrashBins)
                     .HasForeignKey(t => t.FileId)
                     .OnDelete(DeleteBehavior.Cascade);
                 entity.HasOne(t => t.Deleted)
                     .WithMany(t => t.TrashBins)
                     .HasForeignKey(t => t.DeletedBy)
                     .OnDelete(DeleteBehavior.Cascade);
             });

         }


     }
}