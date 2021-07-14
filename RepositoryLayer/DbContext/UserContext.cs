using CommonLayer;
using CommonLayer.DatabaseModel;
using CommonLayer.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
   public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many To Many Note Label
            modelBuilder.Entity<NoteLabel>()
              .HasOne(n => n.Note)
              .WithMany(l => l.NoteLabels)
              .HasForeignKey(nl => nl.NoteId);

            modelBuilder.Entity<NoteLabel>()
                .HasOne(n => n.Label)
                .WithMany(l => l.NoteLabels)
                .HasForeignKey(nl => nl.LabelId);

            // Many To Many User Label
            modelBuilder.Entity<UserLabel>()
                .HasOne(n => n.User)
                .WithMany(l => l.UserLabels)
                .HasForeignKey(nl => nl.UserId);

            modelBuilder.Entity<UserLabel>()
               .HasOne(n => n.Label)
               .WithMany(l => l.UserLabels)
               .HasForeignKey(nl => nl.LabelId);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<NoteLabel> NoteLabels { get; set; }
        public DbSet<UserLabel> UserLabels { get; set; }

    }
}


        


    