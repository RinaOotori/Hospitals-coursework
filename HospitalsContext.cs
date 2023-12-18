using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hospitalsCoursework;

public partial class HospitalsContext : DbContext
{
    public HospitalsContext()
    {
    }

    public HospitalsContext(DbContextOptions<HospitalsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<HospitalsDepartment> HospitalsDepartments { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientsDiagnosis> PatientsDiagnoses { get; set; }

    public virtual DbSet<PatientsDoctor> PatientsDoctors { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=hospitals;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.HasIndex(e => e.DepartmentCode, "departments_department_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('departments_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("department_code");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("hospital_code");
            entity.Property(e => e.Manager).HasColumnName("manager");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.HospitalCodeNavigation).WithMany(p => p.Departments)
                .HasPrincipalKey(p => p.HospitalCode)
                .HasForeignKey(d => d.HospitalCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("departments_hospital_code_fkey");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diagnoses_pkey");

            entity.ToTable("diagnoses");

            entity.HasIndex(e => e.DiagnosisCode, "diagnoses_diagnosis_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiagnosisCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("diagnosis_code");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.TreatmentMethod).HasColumnName("treatment_method");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctors_pkey");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.Tin, "doctors_tin_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('doctors_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("department_code");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("hospital_code");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PostCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("post_code");
            entity.Property(e => e.Tin)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("tin");

            entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Doctors)
                .HasPrincipalKey(p => p.DepartmentCode)
                .HasForeignKey(d => d.DepartmentCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_department_code_fkey");

            entity.HasOne(d => d.HospitalCodeNavigation).WithMany(p => p.Doctors)
                .HasPrincipalKey(p => p.HospitalCode)
                .HasForeignKey(d => d.HospitalCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_hospital_code_fkey");

            entity.HasOne(d => d.PostCodeNavigation).WithMany(p => p.Doctors)
                .HasPrincipalKey(p => p.PostCode)
                .HasForeignKey(d => d.PostCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctors_post_code_fkey");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hospitals_pkey");

            entity.ToTable("hospitals");

            entity.HasIndex(e => e.HospitalCode, "hospitals_hospital_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('hospitals_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("hospital_code");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Tin)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tin");
            entity.Property(e => e.AgePatients).HasColumnName("age_patients");
        });

        modelBuilder.Entity<HospitalsDepartment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("hospitals_departments");

            entity.Property(e => e.IdDepartment).HasColumnName("id_department");
            entity.Property(e => e.IdHospital).HasColumnName("id_hospital");

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany()
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("hospitals_departments_id_department_fkey");

            entity.HasOne(d => d.IdHospitalNavigation).WithMany()
                .HasForeignKey(d => d.IdHospital)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("hospitals_departments_id_hospital_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.HasIndex(e => e.Tin, "patients_tin_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConditionDischarge).HasColumnName("condition_discharge");
            entity.Property(e => e.DiagnosisCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("diagnosis_code");
            entity.Property(e => e.DoctorTin)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("doctor_tin");
            entity.Property(e => e.ExtractDate).HasColumnName("extract_date");
            entity.Property(e => e.HospitalCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("hospital_code");
            entity.Property(e => e.HospitalizationDate).HasColumnName("hospitalization_date");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Tin)
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("tin");

            entity.HasOne(d => d.DiagnosisCodeNavigation).WithMany(p => p.Patients)
                .HasPrincipalKey(p => p.DiagnosisCode)
                .HasForeignKey(d => d.DiagnosisCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_diagnosis_code_fkey");

            entity.HasOne(d => d.DoctorTinNavigation).WithMany(p => p.Patients)
                .HasPrincipalKey(p => p.Tin)
                .HasForeignKey(d => d.DoctorTin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_doctor_tin_fkey");

            entity.HasOne(d => d.HospitalCodeNavigation).WithMany(p => p.Patients)
                .HasPrincipalKey(p => p.HospitalCode)
                .HasForeignKey(d => d.HospitalCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_hospital_code_fkey");
        });

        modelBuilder.Entity<PatientsDiagnosis>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("patients_diagnoses");

            entity.Property(e => e.IdDiagnosis).HasColumnName("id_diagnosis");
            entity.Property(e => e.IdPatient).HasColumnName("id_patient");

            entity.HasOne(d => d.IdDiagnosisNavigation).WithMany()
                .HasForeignKey(d => d.IdDiagnosis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_diagnoses_id_diagnosis_fkey");

            entity.HasOne(d => d.IdPatientNavigation).WithMany()
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_diagnoses_id_patient_fkey");
        });

        modelBuilder.Entity<PatientsDoctor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("patients_doctors");

            entity.Property(e => e.IdDoctor).HasColumnName("id_doctor");
            entity.Property(e => e.IdPatient).HasColumnName("id_patient");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany()
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_doctors_id_doctor_fkey");

            entity.HasOne(d => d.IdPatientNavigation).WithMany()
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patients_doctors_id_patient_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.HasIndex(e => e.PostCode, "posts_post_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Branch).HasColumnName("branch");
            entity.Property(e => e.PostCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("post_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
