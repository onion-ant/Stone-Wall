﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoneWall.Data;

#nullable disable

namespace StoneWall.Migrations
{
    [DbContext(typeof(StoneWallDbContext))]
    [Migration("20240523172249_Fix_Addon")]
    partial class Fix_Addon
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("GenreItemCatalog", b =>
                {
                    b.Property<string>("GenresId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ItemsTmdbId")
                        .HasColumnType("int");

                    b.HasKey("GenresId", "ItemsTmdbId");

                    b.HasIndex("ItemsTmdbId");

                    b.ToTable("GenreItemCatalog");
                });

            modelBuilder.Entity("StoneWall.Entities.Addon", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HomePage")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StreamingId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("StreamingId");

                    b.ToTable("Addons");
                });

            modelBuilder.Entity("StoneWall.Entities.Genre", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemCatalog", b =>
                {
                    b.Property<int?>("TmdbId")
                        .HasColumnType("int");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Popularity")
                        .HasColumnType("double");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("TmdbId");

                    b.ToTable("ItemsCatalog");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemCatalogStreaming", b =>
                {
                    b.Property<int?>("TmdbId")
                        .HasColumnType("int");

                    b.Property<string>("StreamingId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TmdbId", "StreamingId");

                    b.HasIndex("StreamingId");

                    b.ToTable("ItemsCatalog_Streamings");
                });

            modelBuilder.Entity("StoneWall.Entities.Streaming", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HomePage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Streamings");
                });

            modelBuilder.Entity("StoneWall.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoneWall.Entities.UserStreaming", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StreamingId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Plan")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("longtext");

                    b.HasKey("UserEmail", "StreamingId");

                    b.ToTable("User_Streaming");
                });

            modelBuilder.Entity("GenreItemCatalog", b =>
                {
                    b.HasOne("StoneWall.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoneWall.Entities.ItemCatalog", null)
                        .WithMany()
                        .HasForeignKey("ItemsTmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StoneWall.Entities.Addon", b =>
                {
                    b.HasOne("StoneWall.Entities.Streaming", null)
                        .WithMany("Addons")
                        .HasForeignKey("StreamingId");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemCatalogStreaming", b =>
                {
                    b.HasOne("StoneWall.Entities.Streaming", "Streaming")
                        .WithMany("Items")
                        .HasForeignKey("StreamingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoneWall.Entities.ItemCatalog", "Item")
                        .WithMany("Streamings")
                        .HasForeignKey("TmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Streaming");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemCatalog", b =>
                {
                    b.Navigation("Streamings");
                });

            modelBuilder.Entity("StoneWall.Entities.Streaming", b =>
                {
                    b.Navigation("Addons");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
