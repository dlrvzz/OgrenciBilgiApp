using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OkulBilgiApp
{
    internal class OkulDbContext : DbContext
    {
        public DbSet<Ogrenci> Ogrenciler { get; set; } //databasedeki öğrenciler tablosu
        public DbSet<Ders> TblDersler { get; set; }
        public DbSet<OgrenciDers> TblOgrenciDers { get; set; }
        public DbSet<Sinif> TblSiniflar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //entity framework core da veritabanı ile konfügrasyon yapılması için yazmış olduğum bi kod. 
        {
            base.OnConfiguring(optionsBuilder); //database'e bağlanmak için yazdığım kod
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=OkulBilgiDb;Integrated Security=true;TrustServerCertificate=true");

        }

  // Veritabanı tabloları ve ilişkiler için model oluşturma işlemleri.
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) //konfigrasyonların yapılandırıldığı bölüm 
                                                                           //model ile tablo nesne sütunlar ve anahtar kelime özellikleri belirlenebilir.
        {
            base.OnModelCreating(modelBuilder);

 // Öğrenci ve Sınıf arasındaki ilişkiyi tanımlar: Bir öğrenci bir sınıfa aittir, bir sınıf birden fazla öğrenciye sahip olabilir.
            modelBuilder.Entity<Ogrenci>()
                .HasOne(o => o.Sinif)
                .WithMany(s => s.Ogrenciler)
                .HasForeignKey(o => o.SinifId);

 // ÖğrenciDers ve Öğrenci arasındaki ilişki: 
            modelBuilder.Entity<OgrenciDers>()
                .HasOne(od => od.Ogrenci)
                .WithMany(o => o.OgrenciDersler)
                .HasForeignKey(od => od.OgrenciId);
                
// ÖğrenciDers ve Ders arasındaki ilişki: 
    
            modelBuilder.Entity<OgrenciDers>()
                .HasOne(od => od.Ders)
                .WithMany(d => d.OgrenciDersler)
                .HasForeignKey(od => od.DersId);
//öğrenci tablosu için sütun kuralları
            modelBuilder.Entity<Ogrenci>().Property(o => o.Ad).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyad).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Numara).HasColumnType("varchar").HasMaxLength(10).IsRequired();
 // Ders tablosu için sütun kuralları.
            modelBuilder.Entity<Ders>().Property(o => o.DersKod).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ders>().Property(o => o.DersAd).HasColumnType("varchar").HasMaxLength(30).IsRequired();
          
 // Sınıf tablosu için sütun kuralları.
            modelBuilder.Entity<Sinif>().Property(o => o.SinifAd).HasColumnType("varchar").HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Sinif>().Property(o => o.Kontenjan).HasColumnType("varchar").HasMaxLength(10).IsRequired();
        }
    }
}
