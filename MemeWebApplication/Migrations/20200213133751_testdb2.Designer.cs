﻿// <auto-generated />
using System;
using MemeWebApplication.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MemeWebApplication.Migrations
{
    [DbContext(typeof(MemeDbContext))]
    [Migration("20200213133751_testdb2")]
    partial class testdb2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MemeWebApplication.Models.Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MemeWebApplication.Models.ActorTemplates", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("TemplateID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ActorID");

                    b.HasIndex("TemplateID");

                    b.ToTable("ActorTemplates");
                });

            modelBuilder.Entity("MemeWebApplication.Models.Phrase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TemplateID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("TemplateID");

                    b.ToTable("Phrases");
                });

            modelBuilder.Entity("MemeWebApplication.Models.Template", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FilmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("MemeWebApplication.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MemeWebApplication.Models.UserReaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Download")
                        .HasColumnType("bit");

                    b.Property<bool>("Favourite")
                        .HasColumnType("bit");

                    b.Property<int>("TemplateID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TemplateID");

                    b.HasIndex("UserID");

                    b.ToTable("UserReactions");
                });

            modelBuilder.Entity("MemeWebApplication.Models.ActorTemplates", b =>
                {
                    b.HasOne("MemeWebApplication.Models.Actor", null)
                        .WithMany("ActorTemp")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemeWebApplication.Models.Template", null)
                        .WithMany("ActorTemp")
                        .HasForeignKey("TemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MemeWebApplication.Models.Phrase", b =>
                {
                    b.HasOne("MemeWebApplication.Models.Template", "Template")
                        .WithMany("TempPhrases")
                        .HasForeignKey("TemplateID");
                });

            modelBuilder.Entity("MemeWebApplication.Models.UserReaction", b =>
                {
                    b.HasOne("MemeWebApplication.Models.Template", "Template")
                        .WithMany("UserReactions")
                        .HasForeignKey("TemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MemeWebApplication.Models.User", "User")
                        .WithMany("UserReactions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
