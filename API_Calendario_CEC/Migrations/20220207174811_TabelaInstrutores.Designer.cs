﻿// <auto-generated />
using API_Calendario_CEC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_Calendario_CEC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220207174811_TabelaInstrutores")]
    partial class TabelaInstrutores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("API_Calendario_CEC.Models.Instrutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abreviacao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Disponibilidade")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Instrutores");
                });
#pragma warning restore 612, 618
        }
    }
}
