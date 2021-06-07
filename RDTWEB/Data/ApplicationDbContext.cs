using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RDTWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<IdentityUser> AspNetUsers { get; set; }
        public DbSet<IdentityUserRole<string>> AspNetUserRoles { get; set; }
        public DbSet<IdentityRole> AspNetRoles { get; set; }
        public DbSet<QuestionSet> QuestionSets { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var splitStringConverter = new ValueConverter<List<string>, string>(
                v => string.Join(";", v),
                v => new List<string>(v.Split(new[] { ';' }))
            );

            builder.Entity<Question>()
                .Property(nameof(Question.Choices))
                .HasConversion(splitStringConverter);

            builder.Entity<Answer>()
                .HasKey(e => new { e.UserId, e.QuestionId });
        }
    }
}
