﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoneWall.Data;

#nullable disable

namespace StoneWall.Migrations
{
    [DbContext(typeof(StoneWallDbContext))]
    partial class StoneWallDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("StoneWall.Entities.Item", b =>
                {
                    b.Property<int?>("TmdbId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TmdbId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemStreaming", b =>
                {
                    b.Property<int?>("TmdbId")
                        .HasColumnType("int");

                    b.Property<string>("StreamingId")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TmdbId", "StreamingId");

                    b.HasIndex("StreamingId");

                    b.ToTable("Item_Streaming");
                });

            modelBuilder.Entity("StoneWall.Entities.StreamingService", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("HomePage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Streaming_Service");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemStreaming", b =>
                {
                    b.HasOne("StoneWall.Entities.StreamingService", "Streaming")
                        .WithMany("Items")
                        .HasForeignKey("StreamingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoneWall.Entities.Item", "Item")
                        .WithMany("Streamings")
                        .HasForeignKey("TmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Streaming");
                });

            modelBuilder.Entity("StoneWall.Entities.Item", b =>
                {
                    b.Navigation("Streamings");
                });

            modelBuilder.Entity("StoneWall.Entities.StreamingService", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
