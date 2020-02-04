using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamProject1.Models;

namespace TeamProject1.Models
{
    public class TeamProject1Context : DbContext
    {
        public TeamProject1Context (DbContextOptions<TeamProject1Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamProject1.Models.MyRecipe_Ingredient>()
                .HasIndex(p => new { p.R_id, p.I_id }).IsUnique();
        }

        public DbSet<TeamProject1.Models.Ingredient> Ingredient { get; set; }

        public DbSet<TeamProject1.Models.Meat> Meat { get; set; }

        public DbSet<TeamProject1.Models.MyRecipe_Ingredient> MyRecipe_Ingredient { get; set; }

        public DbSet<TeamProject1.Models.MyRecipe> MyRecipe { get; set; }

        public DbSet<TeamProject1.Models.Vegetable> Vegetable { get; set; }

        public DbSet<TeamProject1.Models.Grain> Grain { get; set; }

        public DbSet<TeamProject1.Models.Herb> Herb { get; set; }

        public DbSet<TeamProject1.Models.Spice> Spice { get; set; }

        public DbSet<TeamProject1.Models.Seasoning> Seasoning { get; set; }

        public DbSet<TeamProject1.Models.MyRecipe_Seasoning> MyRecipe_Seasoning { get; set; }
    }
}
