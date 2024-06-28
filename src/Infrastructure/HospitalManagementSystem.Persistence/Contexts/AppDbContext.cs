﻿using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Entities.Common;
using HospitalManagementSystem.Domain.Entities.Identity;
using HospitalManagementSystem.Persistence.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HospitalManagementSystem.Persistence.Contexts
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _accessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor accessor):base(options)
        {
            _accessor = accessor;
        }

        DbSet<Department> Departments { get; set; }
        DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyFilters();
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            var currentUser = _accessor.HttpContext?.User?.Identity?.Name ?? "System";
            foreach (var data in entities)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedAt = DateTime.UtcNow;
                        data.Entity.CreatedBy = currentUser;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedAt = DateTime.UtcNow;
                        data.Entity.UpdatedBy = currentUser;
                        break;
                    default: break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
