﻿// <auto-generated />
using System;
using Hospital_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hospital_App.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231104084944_Add2")]
    partial class Add2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hospital_App.Data.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Hospital_App.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AdminID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Hospital_App.Models.Business", b =>
                {
                    b.Property<int>("OrganizationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConatactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NPONumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("OperatingHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.HasKey("OrganizationID");

                    b.HasIndex("SuburbID");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Hospital_App.Models.Chronic", b =>
                {
                    b.Property<int>("ChronicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChronicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ChronicID");

                    b.ToTable("Chronics");
                });

            modelBuilder.Entity("Hospital_App.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityAbbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CityID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Hospital_App.Models.Contract", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NurseID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.Property<string>("WoundDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ContractID");

                    b.HasIndex("NurseID");

                    b.HasIndex("PatientID");

                    b.HasIndex("SuburbID");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Hospital_App.Models.Nurse", b =>
                {
                    b.Property<int>("NurseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("NurseID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("Hospital_App.Models.OfficeManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("OfficeManagers");
                });

            modelBuilder.Entity("Hospital_App.Models.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmergencyContatctPerson")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmergencyNumber")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDNumber")
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PatientID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital_App.Models.PatientChronic", b =>
                {
                    b.Property<int>("PatientChronicID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChronicID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("PatientChronicID");

                    b.HasIndex("ChronicID");

                    b.HasIndex("PatientID");

                    b.ToTable("PatientChronics");
                });

            modelBuilder.Entity("Hospital_App.Models.PreferedSuburb", b =>
                {
                    b.Property<int>("PreferedSuburbID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NurseID")
                        .HasColumnType("int");

                    b.Property<int>("SuburbID")
                        .HasColumnType("int");

                    b.HasKey("PreferedSuburbID");

                    b.HasIndex("NurseID");

                    b.HasIndex("SuburbID");

                    b.ToTable("PreferedSuburbs");
                });

            modelBuilder.Entity("Hospital_App.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("ApproxArriTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ArriveTime")
                        .HasColumnType("time");

                    b.Property<int>("ContractID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DepartTime")
                        .HasColumnType("time");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NurseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WoundCondition")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduleID");

                    b.HasIndex("ContractID");

                    b.HasIndex("NurseID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Hospital_App.Models.Suburb", b =>
                {
                    b.Property<int>("SuburbID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("SuburbName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SuburbID");

                    b.HasIndex("CityID");

                    b.ToTable("Suburbs");
                });

            modelBuilder.Entity("Hospital_App.Models.Visit", b =>
                {
                    b.Property<int>("VisitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("ApproxArriTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ArriveTime")
                        .HasColumnType("time");

                    b.Property<int>("ContractID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("DepartTime")
                        .HasColumnType("time");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NurseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("VistDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WoundCondition")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VisitID");

                    b.HasIndex("ContractID");

                    b.HasIndex("NurseID");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Hospital_App.Models.Admin", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", "ApplicationyUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationyUser");
                });

            modelBuilder.Entity("Hospital_App.Models.Business", b =>
                {
                    b.HasOne("Hospital_App.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suburb");
                });

            modelBuilder.Entity("Hospital_App.Models.Contract", b =>
                {
                    b.HasOne("Hospital_App.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID");

                    b.HasOne("Hospital_App.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nurse");

                    b.Navigation("Patient");

                    b.Navigation("Suburb");
                });

            modelBuilder.Entity("Hospital_App.Models.Nurse", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", "ApplicationyUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationyUser");
                });

            modelBuilder.Entity("Hospital_App.Models.OfficeManager", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", "ApplicationyUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationyUser");
                });

            modelBuilder.Entity("Hospital_App.Models.Patient", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Hospital_App.Models.PatientChronic", b =>
                {
                    b.HasOne("Hospital_App.Models.Chronic", "Chronic")
                        .WithMany()
                        .HasForeignKey("ChronicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Models.Patient", "Patient")
                        .WithMany("PatientChronics")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chronic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Hospital_App.Models.PreferedSuburb", b =>
                {
                    b.HasOne("Hospital_App.Models.Nurse", "Nurse")
                        .WithMany("PreferedSuburbs")
                        .HasForeignKey("NurseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Models.Suburb", "Suburb")
                        .WithMany()
                        .HasForeignKey("SuburbID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nurse");

                    b.Navigation("Suburb");
                });

            modelBuilder.Entity("Hospital_App.Models.Schedule", b =>
                {
                    b.HasOne("Hospital_App.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Nurse");
                });

            modelBuilder.Entity("Hospital_App.Models.Suburb", b =>
                {
                    b.HasOne("Hospital_App.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Hospital_App.Models.Visit", b =>
                {
                    b.HasOne("Hospital_App.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Nurse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital_App.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Hospital_App.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hospital_App.Models.Nurse", b =>
                {
                    b.Navigation("PreferedSuburbs");
                });

            modelBuilder.Entity("Hospital_App.Models.Patient", b =>
                {
                    b.Navigation("PatientChronics");
                });
#pragma warning restore 612, 618
        }
    }
}
