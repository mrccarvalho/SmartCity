﻿// <auto-generated />
using System;
using SmartCity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SmartCity.Migrations
{
    [DbContext(typeof(SmartCityDbContext))]
    partial class ArduinoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SmartCity.Models.Estacionamento", b =>
                {
                    b.Property<int>("EstacionamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstacionamentoId"), 1L, 1);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstacionamentoId");

                    b.ToTable("Estacionamentos");
                });

            modelBuilder.Entity("SmartCity.Models.Medicao", b =>
                {
                    b.Property<int>("MedicaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicaoId"), 1L, 1);

                    b.Property<DateTime>("DataMedicao")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstacionamentoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorLido")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MedicaoId");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("Medicoes");
                });

            modelBuilder.Entity("SmartCity.Models.Medicao", b =>
                {
                    b.HasOne("SmartCity.Models.Estacionamento", "Estacionamento")
                        .WithMany("Medicoes")
                        .HasForeignKey("EstacionamentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Estacionamento");
                });

            modelBuilder.Entity("SmartCity.Models.Estacionamento", b =>
                {
                    b.Navigation("Medicoes");
                });
#pragma warning restore 612, 618
        }
    }
}
