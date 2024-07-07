﻿// <auto-generated />
using System;
using Chatix.Libs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chatix.Libs.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RepositoryChatixDbContext))]
    [Migration("20240707111954_CreateChatixDB")]
    partial class CreateChatixDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ToRoomId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("ToRoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.RoomUser", b =>
                {
                    b.Property<Guid>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("RoomId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomUsers");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.Message", b =>
                {
                    b.HasOne("Chatix.Libs.Core.Models.Entities.Chatix.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chatix.Libs.Core.Models.Entities.Chatix.Room", "ToRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ToRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");

                    b.Navigation("ToRoom");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.Room", b =>
                {
                    b.HasOne("Chatix.Libs.Core.Models.Entities.Chatix.User", "Admin")
                        .WithMany("CreatedRooms")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.RoomUser", b =>
                {
                    b.HasOne("Chatix.Libs.Core.Models.Entities.Chatix.Room", "Room")
                        .WithMany("UserRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chatix.Libs.Core.Models.Entities.Chatix.User", "User")
                        .WithMany("UserRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.Room", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserRooms");
                });

            modelBuilder.Entity("Chatix.Libs.Core.Models.Entities.Chatix.User", b =>
                {
                    b.Navigation("CreatedRooms");

                    b.Navigation("Messages");

                    b.Navigation("UserRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
