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
    [Migration("20240426182254_change_data-annotations")]
    partial class change_dataannotations
    {
        /// <inheritdoc />
        /// 
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("StoneWall.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("StoneWall.Entities.Item", b =>
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

                    b.ToTable("Items");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemGenre", b =>
                {
                    b.Property<int?>("TmdbId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("TmdbId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("itemGenre");
                });

            modelBuilder.Entity("StoneWall.Entities.ItemStreaming", b =>
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

                    b.ToTable("Item_Streaming");
                });

            modelBuilder.Entity("StoneWall.Entities.StreamingService", b =>
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

                    b.ToTable("Streaming_Services");
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

            modelBuilder.Entity("StoneWall.Entities.ItemGenre", b =>
                {
                    b.HasOne("StoneWall.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoneWall.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("TmdbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Item");
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
