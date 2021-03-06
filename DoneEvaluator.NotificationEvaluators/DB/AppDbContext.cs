﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneEvaluator.NotificationEvaluators.DB
{
    [DbConfigurationType(typeof(DbConfiguration))]
    public class AppDbContext : DbContext
    {
        public string ConnectionString { get; set; }
        public AppDbContext(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<Observation> Observations { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Project> Projects { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    Database.SetInitializer<AppDbContext>(new AppDbContextInitializer());
        //}
    }
}
