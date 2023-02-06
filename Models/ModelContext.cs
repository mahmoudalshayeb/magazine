using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace magazine.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Contactusedit> Contactusedits { get; set; }
       
        public virtual DbSet<Element> Elements { get; set; }
   
      
        public virtual DbSet<Orders1> Orders1s { get; set; }
     
        public virtual DbSet<Product1> Product1s { get; set; }
    
        public virtual DbSet<Roles1> Roles1s { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
       
        public virtual DbSet<Users1> Users1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=JOR17_User13;Password=Alshayeb30;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER13")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Imgpath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMGPATH");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });

            modelBuilder.Entity<Contactusedit>(entity =>
            {
                entity.ToTable("CONTACTUSEDIT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.P)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Theemail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("THEEMAIL");

                entity.Property(e => e.Thelocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("THELOCATION");

                entity.Property(e => e.Thenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("THENUMBER");
            });

           

            modelBuilder.Entity<Element>(entity =>
            {
                entity.ToTable("ELEMENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Aboutustest)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ABOUTUSTEST");

                entity.Property(e => e.Backgroudcolor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BACKGROUDCOLOR");

                entity.Property(e => e.Footertext1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FOOTERTEXT1");

                entity.Property(e => e.Footertext2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FOOTERTEXT2");

                entity.Property(e => e.Footertext3)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FOOTERTEXT3");

                entity.Property(e => e.Logoimg)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LOGOIMG");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Sliderpath1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SLIDERPATH1");

                entity.Property(e => e.Sliderpath2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SLIDERPATH2");

                entity.Property(e => e.Sliderpath3)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SLIDERPATH3");
            });

            


            modelBuilder.Entity<Orders1>(entity =>
            {
                entity.ToTable("ORDERS1");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEFROM");

                entity.Property(e => e.Dateto)
                    .HasColumnType("DATE")
                    .HasColumnName("DATETO");

                entity.Property(e => e.Proid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Stats)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STATS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Pro)
                    .WithMany(p => p.Orders1s)
                    .HasForeignKey(d => d.Proid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PRODUCT1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders1s)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USERS1");
            });

            

            modelBuilder.Entity<Product1>(entity =>
            {
                entity.ToTable("PRODUCT1");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATID");

                entity.Property(e => e.Imgpath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMGPATH");

                entity.Property(e => e.Ispublished)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ISPUBLISHED");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Rate)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATE");

                entity.Property(e => e.Sale)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALE");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Product1s)
                    .HasForeignKey(d => d.Catid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CATEGORY");
            });

           

            modelBuilder.Entity<Roles1>(entity =>
            {
                entity.ToTable("ROLES1");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.State)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STATE");

                entity.Property(e => e.Text)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TESTUSER");
            });

            

            modelBuilder.Entity<Users1>(entity =>
            {
                entity.ToTable("USERS1");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Review)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REVIEW");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users1s)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ROLEID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
