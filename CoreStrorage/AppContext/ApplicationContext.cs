using System.Reflection;
using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Comments;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.CoreEntity;
using CoreBussiness.UnitOfWork;
using CoreStrorage.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoreStrorage.AppContext;

public class ApplicationContext:DbContext,IUnitOfWork
{
    public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
    {
        
    }
    public DbSet<User>Users { get; set; }
    public DbSet<Comment>Comments { get; set; }
    public DbSet<AppService>AppServices { get; set; }
    public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));
            
            var entities = modelBuilder
                .Model
                .GetEntityTypes()
                .Select(x => x.ClrType)
                .Where(x => x.BaseType == typeof(Core))
                .ToList();

            foreach (var type in entities)
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] {modelBuilder});
            }
        }

        public static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationContext)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : Core
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }


        private void changeEntitiesStates()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Core && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((Core) entityEntry.Entity).CreationTime = DateTime.Now;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((Core) entityEntry.Entity).ModificationTime = DateTime.Now;
                }
            }
        }
        
        public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.UseSqlServer("Server=.;Database=EasyService;Trusted_Connection=True; TrustServerCertificate=True;");

                return new ApplicationContext(optionsBuilder.Options);
            }
        }
}