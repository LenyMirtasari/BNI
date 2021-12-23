﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SewaAPI.Context;

namespace SewaAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20211221151255_satu")]
    partial class satu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SewaAPI.Models.Account", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Tb_T_Account");
                });

            modelBuilder.Entity("SewaAPI.Models.LogPenyewa", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AkhirSewa")
                        .HasColumnType("datetime2");

                    b.Property<int>("MobilId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MulaiSewa")
                        .HasColumnType("datetime2");

                    b.Property<int>("PenyewaId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TglKembali")
                        .HasColumnType("datetime2");

                    b.HasKey("LogId");

                    b.HasIndex("MobilId");

                    b.HasIndex("PenyewaId");

                    b.ToTable("Tb_T_LogPenyewa");
                });

            modelBuilder.Entity("SewaAPI.Models.Mobil", b =>
                {
                    b.Property<int>("MobilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StatusMobil")
                        .HasColumnType("int");

                    b.Property<string>("Tipe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("plat")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MobilId");

                    b.ToTable("Tb_T_Mobil");
                });

            modelBuilder.Entity("SewaAPI.Models.Penyewa", b =>
                {
                    b.Property<int>("PenyewaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alamat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoTelp")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PenyewaId");

                    b.HasIndex("NoTelp")
                        .IsUnique()
                        .HasFilter("[NoTelp] IS NOT NULL");

                    b.ToTable("Tb_T_Penyewa");
                });

            modelBuilder.Entity("SewaAPI.Models.LogPenyewa", b =>
                {
                    b.HasOne("SewaAPI.Models.Mobil", "Mobil")
                        .WithMany("LogPenyewa")
                        .HasForeignKey("MobilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SewaAPI.Models.Penyewa", "Penyewa")
                        .WithMany("LogPenyewa")
                        .HasForeignKey("PenyewaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mobil");

                    b.Navigation("Penyewa");
                });

            modelBuilder.Entity("SewaAPI.Models.Mobil", b =>
                {
                    b.Navigation("LogPenyewa");
                });

            modelBuilder.Entity("SewaAPI.Models.Penyewa", b =>
                {
                    b.Navigation("LogPenyewa");
                });
#pragma warning restore 612, 618
        }
    }
}
