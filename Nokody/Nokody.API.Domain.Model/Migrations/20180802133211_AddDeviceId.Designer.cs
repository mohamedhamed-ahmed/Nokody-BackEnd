﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nokody.API.Domain.Model.Models;

namespace Nokody.API.Domain.Model.Migrations
{
    [DbContext(typeof(NokodyModel))]
    [Migration("20180802133211_AddDeviceId")]
    partial class AddDeviceId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("Balance");

                    b.Property<string>("BraceletNumber");

                    b.Property<bool?>("IsActive");

                    b.Property<DateTime?>("OpenedDate");

                    b.Property<string>("PassportNumber");

                    b.Property<int?>("PinCode");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("Amount");

                    b.Property<DateTime?>("Date");

                    b.Property<int?>("FromAccountId");

                    b.Property<bool?>("IsSuccessful");

                    b.Property<int?>("ToAccountId");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("UserIdentityId");

                    b.Property<int?>("UserTypeId");

                    b.HasKey("Id");

                    b.HasIndex("UserIdentityId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.UserIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("UserIdentity");
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.Account", b =>
                {
                    b.HasOne("Nokody.API.Domain.Model.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.Transaction", b =>
                {
                    b.HasOne("Nokody.API.Domain.Model.Models.Account", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Nokody.API.Domain.Model.Models.Account", "ToAccount")
                        .WithMany()
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Nokody.API.Domain.Model.Models.User", b =>
                {
                    b.HasOne("Nokody.API.Domain.Model.Models.UserIdentity", "UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserIdentityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Nokody.API.Domain.Model.Models.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
