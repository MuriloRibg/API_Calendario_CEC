﻿// <auto-generated />
using System;
using API_Calendario_CEC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_Calendario_CEC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("API_Calendario_CEC.Models.Aula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Id_Disciplina")
                        .HasColumnType("int");

                    b.Property<int>("Id_Instrutor")
                        .HasColumnType("int");

                    b.Property<int>("Id_Reserva")
                        .HasColumnType("int");

                    b.Property<int>("Id_Turma")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Disciplina");

                    b.HasIndex("Id_Instrutor");

                    b.HasIndex("Id_Reserva");

                    b.HasIndex("Id_Turma");

                    b.ToTable("Aulas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Disciplina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pilar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Id_Instrutor")
                        .HasColumnType("int");

                    b.Property<int>("Id_Reserva")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Instrutor");

                    b.HasIndex("Id_Reserva");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Instrutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abreviacao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Disponibilidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Pilar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PilarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PilarId");

                    b.ToTable("Instrutores");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Local", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Sistemas")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Locais");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Pilar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime");

                    b.Property<string>("NomePilar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pilares");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFim")
                        .HasMaxLength(20)
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataInicio")
                        .HasMaxLength(20)
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HoraFim")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime");

                    b.Property<int>("Id_Local")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Local");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime");

                    b.Property<int>("Id_Pilar")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quant_alunos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Pilar");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Aula", b =>
                {
                    b.HasOne("API_Calendario_CEC.Models.Disciplina", "Disciplina")
                        .WithMany("Aulas")
                        .HasForeignKey("Id_Disciplina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Calendario_CEC.Models.Instrutor", "Instrutor")
                        .WithMany("Aulas")
                        .HasForeignKey("Id_Instrutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Calendario_CEC.Models.Reserva", "Reserva")
                        .WithMany("Aulas")
                        .HasForeignKey("Id_Reserva")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Calendario_CEC.Models.Turma", "Turma")
                        .WithMany("Aulas")
                        .HasForeignKey("Id_Turma")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disciplina");

                    b.Navigation("Instrutor");

                    b.Navigation("Reserva");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Evento", b =>
                {
                    b.HasOne("API_Calendario_CEC.Models.Instrutor", "Instrutor")
                        .WithMany("Eventos")
                        .HasForeignKey("Id_Instrutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_Calendario_CEC.Models.Reserva", "Reserva")
                        .WithMany("Eventos")
                        .HasForeignKey("Id_Reserva")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrutor");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Instrutor", b =>
                {
                    b.HasOne("API_Calendario_CEC.Models.Pilar", null)
                        .WithMany("Instrutor")
                        .HasForeignKey("PilarId");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Reserva", b =>
                {
                    b.HasOne("API_Calendario_CEC.Models.Local", "Local")
                        .WithMany("Reservas")
                        .HasForeignKey("Id_Local")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Local");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Turma", b =>
                {
                    b.HasOne("API_Calendario_CEC.Models.Pilar", "Pilar")
                        .WithMany("Turmas")
                        .HasForeignKey("Id_Pilar")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilar");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Disciplina", b =>
                {
                    b.Navigation("Aulas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Instrutor", b =>
                {
                    b.Navigation("Aulas");

                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Local", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Pilar", b =>
                {
                    b.Navigation("Instrutor");

                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Reserva", b =>
                {
                    b.Navigation("Aulas");

                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("API_Calendario_CEC.Models.Turma", b =>
                {
                    b.Navigation("Aulas");
                });
#pragma warning restore 612, 618
        }
    }
}
