﻿// <auto-generated />
using System;
using CS.DAL.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CS.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240301161134_TicketAndMessageModelsImprovemrnt")]
    partial class TicketAndMessageModelsImprovemrnt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CS.DAL.Models.Dialog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TicketId")
                        .IsUnique();

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("CS.DAL.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DialogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("WhenSend")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CS.DAL.Models.MessageAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("MessageAttachments");
                });

            modelBuilder.Entity("CS.DAL.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSolved")
                        .HasColumnType("bit");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DetailsId")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("CS.DAL.Models.TicketAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketAttachments");
                });

            modelBuilder.Entity("CS.DAL.Models.TicketDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("WhenCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TicketDetails");
                });

            modelBuilder.Entity("CS.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CS.DAL.Models.Dialog", b =>
                {
                    b.HasOne("CS.DAL.Models.Ticket", "Ticket")
                        .WithOne("Dialog")
                        .HasForeignKey("CS.DAL.Models.Dialog", "TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("CS.DAL.Models.Message", b =>
                {
                    b.HasOne("CS.DAL.Models.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CS.DAL.Models.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Dialog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CS.DAL.Models.MessageAttachment", b =>
                {
                    b.HasOne("CS.DAL.Models.Message", "Message")
                        .WithMany("Attachments")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("CS.DAL.Models.Ticket", b =>
                {
                    b.HasOne("CS.DAL.Models.User", "Admin")
                        .WithMany("AssignedTickets")
                        .HasForeignKey("AdminId");

                    b.HasOne("CS.DAL.Models.User", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CS.DAL.Models.TicketDetails", "Details")
                        .WithOne("Ticket")
                        .HasForeignKey("CS.DAL.Models.Ticket", "DetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Customer");

                    b.Navigation("Details");
                });

            modelBuilder.Entity("CS.DAL.Models.TicketAttachment", b =>
                {
                    b.HasOne("CS.DAL.Models.Ticket", "Ticket")
                        .WithMany("Attachments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("CS.DAL.Models.Dialog", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("CS.DAL.Models.Message", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("CS.DAL.Models.Ticket", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Dialog")
                        .IsRequired();
                });

            modelBuilder.Entity("CS.DAL.Models.TicketDetails", b =>
                {
                    b.Navigation("Ticket")
                        .IsRequired();
                });

            modelBuilder.Entity("CS.DAL.Models.User", b =>
                {
                    b.Navigation("AssignedTickets");

                    b.Navigation("Messages");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
