﻿// <auto-generated />
using System;
using AccountServer.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240208080929_oauth3")]
    partial class oauth3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AccountServer.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastLoginAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("AccountServer.Entities.Oauth", b =>
                {
                    b.Property<int>("OauthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("OauthToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OauthType")
                        .HasColumnType("int");

                    b.HasKey("OauthId");

                    b.HasIndex("AccountId");

                    b.HasIndex("OauthId")
                        .IsUnique();

                    b.ToTable("Oauth");
                });

            modelBuilder.Entity("AccountServer.Entities.Oauth", b =>
                {
                    b.HasOne("AccountServer.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
