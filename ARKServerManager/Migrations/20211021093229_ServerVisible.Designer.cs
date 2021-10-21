﻿// <auto-generated />
using System;
using ARKServerManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ARKServerManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211021093229_ServerVisible")]
    partial class ServerVisible
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ARKServerManager.Models.PlayerStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastGame")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ServerId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("TimeGame")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("ARKServerManager.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocalIP")
                        .HasColumnType("TEXT");

                    b.Property<int>("LocalPort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Map")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("RconPass")
                        .HasColumnType("TEXT");

                    b.Property<ushort>("RconPort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RemoteIP")
                        .HasColumnType("TEXT");

                    b.Property<int>("RemotePort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServerPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .HasColumnType("TEXT");

                    b.Property<int>("Visible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Server");
                });
#pragma warning restore 612, 618
        }
    }
}