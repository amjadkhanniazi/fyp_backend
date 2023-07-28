using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SP3.Model;

public partial class SocialWelfareContext : DbContext
{
    public SocialWelfareContext()
    {
    }

    public SocialWelfareContext(DbContextOptions<SocialWelfareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addre> Addres { get; set; }

    public virtual DbSet<NeedyBankDetail> NeedyBankDetails { get; set; }

    public virtual DbSet<PersonalDetail> PersonalDetails { get; set; }

    public virtual DbSet<UserDocument> UserDocuments { get; set; }

    public virtual DbSet<UserRegistry> UserRegistries { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-ADDB7P8;Initial Catalog=Social_Welfare;Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Addre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__addres__3214EC27D048D696");

            entity.ToTable("addres");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Cnic).HasColumnName("cnic");
            entity.Property(e => e.CurrentAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("current_address");
            entity.Property(e => e.NearbyFamousPlace)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nearby_famous_place");
            entity.Property(e => e.NearbyFamousPlace2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nearby_famous_place2");
            entity.Property(e => e.PermanantAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("permanant_address");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Town)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("town");

            entity.HasOne(d => d.CnicNavigation).WithMany(p => p.Addres)
                .HasForeignKey(d => d.Cnic)
                .HasConstraintName("FK__addres__cnic__4316F928");
        });

        modelBuilder.Entity<NeedyBankDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__needy_ba__3214EC27AB4C4C0F");

            entity.ToTable("needy_bank_details");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_title");
            entity.Property(e => e.BankName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("bank_name");
            entity.Property(e => e.Cnic).HasColumnName("cnic");
            entity.Property(e => e.Iban)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("iban");

            entity.HasOne(d => d.CnicNavigation).WithMany(p => p.NeedyBankDetails)
                .HasForeignKey(d => d.Cnic)
                .HasConstraintName("FK__needy_bank__cnic__534D60F1");
        });

        modelBuilder.Entity<PersonalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personal__3214EC27FA1065D2");

            entity.ToTable("personal_details");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AvgMonthElectricBill).HasColumnName("avg_month_electric_bill");
            entity.Property(e => e.AvgMonthGasBill).HasColumnName("avg_month_gas_bill");
            entity.Property(e => e.AvgMonthlyExpense).HasColumnName("avg_monthly_expense");
            entity.Property(e => e.Cnic).HasColumnName("cnic");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.GrantCategory)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("grant_category");
            entity.Property(e => e.House)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("house");
            entity.Property(e => e.HouseAreaMarla).HasColumnName("house_area_marla");
            entity.Property(e => e.JobType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("job_type");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MarriageStatus).HasColumnName("marriage_status");
            entity.Property(e => e.MonthalyIncome).HasColumnName("monthaly_income");
            entity.Property(e => e.NoOfChildren).HasColumnName("no_of_children");

            entity.HasOne(d => d.CnicNavigation).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.Cnic)
                .HasConstraintName("FK__personal_d__cnic__403A8C7D");
        });

        modelBuilder.Entity<UserDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_doc__3214EC27C473B98E");

            entity.ToTable("user_documents");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cnic).HasColumnName("cnic");
            entity.Property(e => e.CnicBack).HasColumnName("cnic_back");
            entity.Property(e => e.CnicFront).HasColumnName("cnic_front");
            entity.Property(e => e.ElectricBill).HasColumnName("electric_bill");
            entity.Property(e => e.GasBill).HasColumnName("gas_bill");

            entity.HasOne(d => d.CnicNavigation).WithMany(p => p.UserDocuments)
                .HasForeignKey(d => d.Cnic)
                .HasConstraintName("FK__user_docum__cnic__5070F446");
        });

        modelBuilder.Entity<UserRegistry>(entity =>
        {
            entity.HasKey(e => e.Cnic).HasName("cnic");

            entity.ToTable("user_registry");

            entity.Property(e => e.Cnic)
                .ValueGeneratedNever()
                .HasColumnName("cnic");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
