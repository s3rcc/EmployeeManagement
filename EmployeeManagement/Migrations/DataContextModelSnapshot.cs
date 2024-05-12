﻿// <auto-generated />
using System;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement.Models.Claim", b =>
                {
                    b.Property<int>("ClaimID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClaimID"));

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("ClaimValue")
                        .HasColumnType("bit");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ClaimID");

                    b.HasIndex("RoleID");

                    b.ToTable("Claim");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Form", b =>
                {
                    b.Property<int>("FormID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormID"));

                    b.Property<byte[]>("Attachments")
                        .IsRequired()
                        .HasColumnType("varbinary(MAX)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("FormDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FormName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FormID");

                    b.HasIndex("TypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("EmployeeManagement.Models.FormType", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TypeID");

                    b.ToTable("Form_Type");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Salary", b =>
                {
                    b.Property<int>("SalaryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryID"));

                    b.Property<decimal>("BaseSalary")
                        .HasColumnType("money");

                    b.Property<decimal>("DailyRate")
                        .HasColumnType("money");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("SalaryID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("EmployeeManagement.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("SalaryID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EmployeeManagement.Models.UserClaim", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ClaimID")
                        .HasColumnType("int");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ClaimID");

                    b.HasIndex("ClaimID");

                    b.ToTable("User_Claim");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Claim", b =>
                {
                    b.HasOne("EmployeeManagement.Models.Role", "Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Form", b =>
                {
                    b.HasOne("EmployeeManagement.Models.FormType", "FormType")
                        .WithMany("Forms")
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.User", "User")
                        .WithMany("Forms")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FormType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Salary", b =>
                {
                    b.HasOne("EmployeeManagement.Models.User", "User")
                        .WithOne("Salary")
                        .HasForeignKey("EmployeeManagement.Models.Salary", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeManagement.Models.User", b =>
                {
                    b.HasOne("EmployeeManagement.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EmployeeManagement.Models.UserClaim", b =>
                {
                    b.HasOne("EmployeeManagement.Models.Claim", "Claim")
                        .WithMany("UserClaims")
                        .HasForeignKey("ClaimID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.Models.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Claim", b =>
                {
                    b.Navigation("UserClaims");
                });

            modelBuilder.Entity("EmployeeManagement.Models.FormType", b =>
                {
                    b.Navigation("Forms");
                });

            modelBuilder.Entity("EmployeeManagement.Models.Role", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("EmployeeManagement.Models.User", b =>
                {
                    b.Navigation("Forms");

                    b.Navigation("Salary")
                        .IsRequired();

                    b.Navigation("UserClaims");
                });
#pragma warning restore 612, 618
        }
    }
}
