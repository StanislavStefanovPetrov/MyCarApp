﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCarApp.Data;

namespace MyCarApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180815223039_VehicleAddPictureCollection")]
    partial class VehicleAddPictureCollection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyCarApp.Models.Advertisements.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsExpired");

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("SellerId");

                    b.Property<string>("SellerId1");

                    b.Property<string>("Title");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("SellerId1");

                    b.HasIndex("VehicleId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("MyCarApp.Models.Advertisements.AdvertisementUser", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("AdvertisementId");

                    b.HasKey("UserId", "AdvertisementId");

                    b.HasIndex("AdvertisementId");

                    b.ToTable("AdvertisementUsers");
                });

            modelBuilder.Entity("MyCarApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.Colour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Colours");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.Engine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CubicCapacity");

                    b.HasKey("Id");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.EngineFuelType", b =>
                {
                    b.Property<int>("EngineId");

                    b.Property<int>("FuelTypeId");

                    b.HasKey("EngineId", "FuelTypeId");

                    b.HasIndex("FuelTypeId");

                    b.ToTable("EngineFuelTypes");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Fuel");

                    b.HasKey("Id");

                    b.HasIndex("Fuel")
                        .IsUnique()
                        .HasFilter("[Fuel] IS NOT NULL");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.PicturePath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("Path")
                        .IsUnique()
                        .HasFilter("[Path] IS NOT NULL");

                    b.HasIndex("VehicleId");

                    b.ToTable("PicturePaths");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.Transmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Transmissions");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ConditionId");

                    b.Property<int?>("EngineId");

                    b.Property<DateTime>("FirstRegistration");

                    b.Property<int>("Kilometre");

                    b.Property<int?>("MakeId");

                    b.Property<int?>("ModelId");

                    b.Property<int>("Power");

                    b.Property<decimal>("Price");

                    b.Property<int?>("VehicleExteriorColourId");

                    b.Property<int?>("VehicleTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ConditionId");

                    b.HasIndex("EngineId");

                    b.HasIndex("MakeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("VehicleExteriorColourId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Condition");

                    b.HasKey("Id");

                    b.HasIndex("Condition")
                        .IsUnique()
                        .HasFilter("[Condition] IS NOT NULL");

                    b.ToTable("VehicleConditions");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleMake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("VehicleMakes");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MakeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.HasIndex("Name");

                    b.ToTable("VehicleModels");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleTransmission", b =>
                {
                    b.Property<int>("VehicleId");

                    b.Property<int>("TransmissionId");

                    b.HasKey("VehicleId", "TransmissionId");

                    b.HasIndex("TransmissionId");

                    b.ToTable("VehicleTransmissions");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique()
                        .HasFilter("[Type] IS NOT NULL");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyCarApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyCarApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCarApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyCarApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Advertisements.Advertisement", b =>
                {
                    b.HasOne("MyCarApp.Models.ApplicationUser", "Seller")
                        .WithMany("Advertisements")
                        .HasForeignKey("SellerId1");

                    b.HasOne("MyCarApp.Models.Vehicles.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Advertisements.AdvertisementUser", b =>
                {
                    b.HasOne("MyCarApp.Models.Advertisements.Advertisement", "Advertisement")
                        .WithMany("Watchers")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCarApp.Models.ApplicationUser", "User")
                        .WithMany("TrackAdvertisements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.EngineFuelType", b =>
                {
                    b.HasOne("MyCarApp.Models.Vehicles.Engine", "Engine")
                        .WithMany("FuelTypes")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCarApp.Models.Vehicles.FuelType", "FuelType")
                        .WithMany("Engines")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.PicturePath", b =>
                {
                    b.HasOne("MyCarApp.Models.Vehicles.Vehicle", "Vehicle")
                        .WithMany("PicturePaths")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.Vehicle", b =>
                {
                    b.HasOne("MyCarApp.Models.Vehicles.VehicleCondition", "Condition")
                        .WithMany("Vehicles")
                        .HasForeignKey("ConditionId");

                    b.HasOne("MyCarApp.Models.Vehicles.Engine", "Engine")
                        .WithMany("Vehicles")
                        .HasForeignKey("EngineId");

                    b.HasOne("MyCarApp.Models.Vehicles.VehicleMake", "Make")
                        .WithMany("Vehicles")
                        .HasForeignKey("MakeId");

                    b.HasOne("MyCarApp.Models.Vehicles.VehicleModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId");

                    b.HasOne("MyCarApp.Models.Vehicles.Colour", "VehicleExteriorColour")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleExteriorColourId");

                    b.HasOne("MyCarApp.Models.Vehicles.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeId");
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleModel", b =>
                {
                    b.HasOne("MyCarApp.Models.Vehicles.VehicleMake", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyCarApp.Models.Vehicles.VehicleTransmission", b =>
                {
                    b.HasOne("MyCarApp.Models.Vehicles.Transmission", "Transmission")
                        .WithMany("Vehicles")
                        .HasForeignKey("TransmissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyCarApp.Models.Vehicles.Vehicle", "Vehicle")
                        .WithMany("Transmissions")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
