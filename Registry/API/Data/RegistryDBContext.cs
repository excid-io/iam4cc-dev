﻿using Microsoft.EntityFrameworkCore;

using Excid.Registry.API.Models;

namespace Excid.Registry.API.Data
{

    public class RegistryDBContext: DbContext
    {
        public RegistryDBContext(DbContextOptions<RegistryDBContext> options)
                : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning);
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelationshipObject> RelationshipObjects { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
    }
}
