﻿// <auto-generated />
using System;
using ARKServerManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ARKServerManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ARKServerManager.Models.Job", b =>
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

                    b.Property<string>("Name")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Server");
                });
#pragma warning restore 612, 618
        }
    }
}
