﻿// <auto-generated />
using System;
using ARKServerManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ARKServerManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211222081920_ServerAddColumn")]
    partial class ServerAddColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ARKServerManager.Models.PlayerStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastGame")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlayerId")
                        .HasColumnType("longtext");

                    b.Property<string>("PlayerName")
                        .HasColumnType("longtext");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeGame")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("ARKServerManager.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GamePort")
                        .HasColumnType("int");

                    b.Property<string>("LocalIP")
                        .HasColumnType("longtext");

                    b.Property<int>("LocalPort")
                        .HasColumnType("int");

                    b.Property<string>("Map")
                        .HasColumnType("longtext");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PublicName")
                        .HasColumnType("longtext");

                    b.Property<string>("RconPass")
                        .HasColumnType("longtext");

                    b.Property<ushort>("RconPort")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("RemoteIP")
                        .HasColumnType("longtext");

                    b.Property<int>("RemotePort")
                        .HasColumnType("int");

                    b.Property<string>("ServerPath")
                        .HasColumnType("longtext");

                    b.Property<string>("Version")
                        .HasColumnType("longtext");

                    b.Property<int>("Visible")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Server");
                });

            modelBuilder.Entity("ARKServerManager.Models.ServerTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateJob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("Repeating")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<int>("ServerId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("ARKServerManager.Models.SteamCMD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("OperationSystem")
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SteamCMD");
                });
#pragma warning restore 612, 618
        }
    }
}
