﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(Context.Context))]
    [Migration("20220708144359_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Model.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CommentLikeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PostID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CommentLikeCount = 0,
                            InsertedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(141),
                            PostID = 1,
                            Text = "the first comment",
                            UpdatedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(141)
                        });
                });

            modelBuilder.Entity("Model.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("desciption")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            InsertedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(117),
                            IsArchive = false,
                            LikeCount = 0,
                            Title = "first Post",
                            UpdatedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(117),
                            UserID = 1,
                            desciption = "it is the first post"
                        });
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            HashedPassword = "1574ad62d48a37f847699d7d2157105a5a5fd6ed323a3497fa41c7731229bf23",
                            InsertedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(56),
                            UpdatedDateTime = new DateTime(2022, 7, 8, 7, 43, 59, 59, DateTimeKind.Local).AddTicks(56),
                            UserName = "Amir"
                        });
                });

            modelBuilder.Entity("Model.Comment", b =>
                {
                    b.HasOne("Model.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Model.Post", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany("Post")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Post", b =>
                {
                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("Post");
                });
#pragma warning restore 612, 618
        }
    }
}
