using Domain.Common;
using Domain.Features.Auth.Entities;
using Domain.Features.Users.Entities;
using Domain.Features.Finances.Entities;

using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {

        ////////////////////////////////////////////////////////////
        /// users
        ///////////////////////////////////////////////////////////////
        public DbSet<User> Users => Set<User>();

        ////////////////////////////////////////////////////////////
        /// Auths
        ///////////////////////////////////////////////////////////////
        public DbSet<TwoFactorCode> TwoFactorCodes => Set<TwoFactorCode>();
        public DbSet<TwoFactorStatus> TwoFactorStatus => Set<TwoFactorStatus>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();



        ////////////////////////////////////////////////////////////
        /// finances
        ///////////////////////////////////////////////////////////////
        public DbSet<Nature> Natures => Set<Nature>();
        public DbSet<Bank> Banks => Set<Bank>();
        public DbSet<Currency> Currecies => Set<Currency>();
        public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<AccountType> AccountTypes => Set<AccountType>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// modela las entidades y relaciones, as como configura las tablas y restricciones
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>().ToTable("Users", "users");
            modelBuilder.Entity<TwoFactorCode>().ToTable("TwoFactorCodes", "auth");
            modelBuilder.Entity<TwoFactorStatus>().ToTable("TwoFactorStatus", "auth");
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens", "auth");

            modelBuilder.Entity<Bank>().ToTable("Banks", "finances");
            modelBuilder.Entity<Currency>().ToTable("Currencies", "finances");
            modelBuilder.Entity<Nature>().ToTable("Natures", "finances");
            modelBuilder.Entity<AccountType>().ToTable("AccountTypes", "finances");
            modelBuilder.Entity<Account>().ToTable("Accounts", "finances");
            modelBuilder.Entity<Category>().ToTable("Categories", "finances");
            modelBuilder.Entity<TransactionType>().ToTable("TransactionTypes", "finances");
            modelBuilder.Entity<Transaction>().ToTable("Transactions", "finances");




            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("UserID");
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
            });



            modelBuilder.Entity<Nature>(entity =>
            {
                entity.HasKey(x => x.NatureID);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Abbre).IsRequired().HasMaxLength(5);
            });

            //anadrir datos semilla para los para naturalezas
            modelBuilder.Entity<Nature>().HasData(
                new Nature(1, "Income", "I"),
                new Nature(2, "Expenses", "E")
            );

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("BankID");
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Abbre).IsRequired().HasMaxLength(10);
                entity.Property(x => x.TransferFee).IsRequired();
            });

            //Anadir datos semilla para los bancos
            modelBuilder.Entity<Bank>().HasData(
                new Bank("BANCO DE AMERICA", "BAC", 2),
                new Bank("BANCO DE LA PRODUCCION", "BANPRO", 2)
            );

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("CurrecyID");
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Code).IsRequired().HasMaxLength(3);
                entity.Property(x => x.Symbol).IsRequired().HasMaxLength(3);
            });


            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("TransactionTypeID");
                entity.HasIndex(x => x.Code).IsUnique();
                entity.Property(x => x.Name).IsRequired();
            });


            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType("Income", "INC"),
                 new TransactionType("Expenses", "EXP"),
                new TransactionType("Transfers", "TRF")
            );



            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("AccountTypeID");
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Abbre).IsRequired().HasMaxLength(5);
            });




            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("CategoryID");
                entity.HasIndex(x => x.UserID);
                entity.HasIndex(x => x.ParentID);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(150);
                entity.HasOne(x => x.User)
                      .WithMany() // un usuario puede tener muchas categorías
                      .HasForeignKey(x => x.UserID);
                entity.HasOne(x => x.Nature)
                      .WithMany() // una naturaleza puede tener muchas categorías
                      .HasForeignKey(x => x.NatureID);
                entity.HasOne(x => x.Parent) // relación consigo misma para categorías anidadas
                      .WithMany() // una categoría padre puede tener muchas categorías hijas
                      .HasForeignKey(x => x.ParentID)
                      .OnDelete(DeleteBehavior.Restrict); // evitar eliminación en cascada para no borrar toda la jerarquía
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("AccountID");
                entity.HasOne(x => x.User)
                      .WithMany() // un usuario puede tener muchas cuentas
                      .HasForeignKey(x => x.UserID);
                entity.HasOne(x => x.AccountType)
                      .WithMany() // un tipo de cuenta puede tener muchas cuentas
                      .HasForeignKey(x => x.AccountTypeID);
                entity.HasOne(x => x.Currecy)
                      .WithMany() // una moneda puede tener muchas cuentas
                      .HasForeignKey(x => x.CurrecyID);
                entity.HasOne(x => x.Bank)
                      .WithMany() // un banco puede tener muchas cuentas
                      .HasForeignKey(x => x.BankID);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(150);
                entity.Property(x => x.Balance).IsRequired();
            });


            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("TransactionID");
                entity.HasIndex(x => new { x.UserID, x.TransactionDate });
                entity.HasIndex(x => x.Reference);
                entity.HasOne(x => x.User)
                      .WithMany() // un usuario puede tener muchas cuentas
                      .HasForeignKey(x => x.UserID);
                entity.HasOne(x => x.TransactionType)
                      .WithMany() // un tipo de cuenta puede tener muchas cuentas
                      .HasForeignKey(x => x.TransactionTypeID);
                entity.HasOne(x => x.Category)
                        .WithMany() // una categoría
                        .HasForeignKey(x => x.CategoryID);
                entity.HasOne(x => x.Account)
                        .WithMany() // una cuenta puede tener muchas transacciones
                        .HasForeignKey(x => x.AccountID);
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Amount).IsRequired();
                entity.Property(x => x.TransactionDate).IsRequired();
                entity.Property(x => x.TransferGroupID);
            });








            //categorías de estado para los códigos 2FA
            modelBuilder.Entity<TwoFactorStatus>(entity =>
            {
                entity.HasKey(x => x.TwoFactorStatusID);
                entity.Property(x => x.Status)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            //anadrir datos semilla para los estados de 2FA
            modelBuilder.Entity<TwoFactorStatus>().HasData(
                new TwoFactorStatus(1, "Active"),
                new TwoFactorStatus(2, "Used"),
                new TwoFactorStatus(3, "Expired"),
                new TwoFactorStatus(4, "Replaced")
            );


            modelBuilder.Entity<TwoFactorCode>(entity =>
            {
                entity.Property(x => x.Id).HasColumnName("TwoFactorCodeID");
                entity.HasIndex(x => new { x.UserID, x.Code });
                entity.HasOne(x => x.Status)
                      .WithMany()
                      .HasForeignKey(x => x.TwoFactorStatusID);
                entity.HasOne<User>()       // relación con User
                .WithMany()                  // un usuario puede tener muchos códigos 2FA
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("RefreshTokenID");
                entity.HasIndex(x => x.Token).IsUnique();
                entity.HasOne<User>()              // relación con User
                      .WithMany()                  // un usuario puede tener muchos refresh tokens
                      .HasForeignKey(x => x.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        /// <summary>
        /// permite actualizar automáticamente la propiedad UpdatedAt de las entidades que heredan de AuditableEntity cada vez que se guardan cambios en la base de datos. 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.GetType()
                        .GetProperty("UpdatedAt")?
                        .SetValue(entry.Entity, DateTime.UtcNow);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
