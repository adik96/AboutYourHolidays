using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AboutYourHolidays.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        : base("DefaultConnection")
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Potrzebne dla klas Identity
            base.OnModelCreating(modelBuilder);
            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel
            // w bazie danych
            // Zamiast Kategorie zostałaby utworzona tabela o nazwie Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention> ();
            // Wyłącza konwencję CascadeDelete
            // CascadeDelete zostanie włączone za pomocą Fluent API
            modelBuilder.Conventions.Remove <OneToManyCascadeDeleteConvention> ();
            // Używa się Fluent API, aby ustalić powiązanie pomiędzy tabelami
            // i włączyć CascadeDelete dla tego powiązania
            modelBuilder.Entity<Post>().HasRequired(x => x.User).WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>().HasRequired(x => x.User).WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);
        }
    }
    }