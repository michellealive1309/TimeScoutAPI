﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TimeScout.API.DataAccess;

#nullable disable

namespace TimeScout.API.DataAccess.Migrations
{
    [DbContext(typeof(TimeScoutDbContext))]
    partial class TimeScoutDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventGroupUser", b =>
                {
                    b.Property<int>("EventGroupsId")
                        .HasColumnType("integer")
                        .HasColumnName("event_groups_id");

                    b.Property<int>("MembersId")
                        .HasColumnType("integer")
                        .HasColumnName("members_id");

                    b.HasKey("EventGroupsId", "MembersId")
                        .HasName("pk_event_group_user");

                    b.HasIndex("MembersId")
                        .HasDatabaseName("ix_event_group_user_members_id");

                    b.ToTable("event_group_user", (string)null);
                });

            modelBuilder.Entity("TimeScout.API.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Detail")
                        .HasColumnType("text")
                        .HasColumnName("detail");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("end_time");

                    b.Property<int>("EventGroupId")
                        .HasColumnType("integer")
                        .HasColumnName("event_group_id");

                    b.Property<bool>("IsShared")
                        .HasColumnType("boolean")
                        .HasColumnName("is_shared");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("start_time");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("EventGroupId")
                        .HasDatabaseName("ix_events_event_group_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_events_user_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("TimeScout.API.Models.EventGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_event_groups");

                    b.ToTable("event_groups", (string)null);
                });

            modelBuilder.Entity("TimeScout.API.Models.EventGroupMember", b =>
                {
                    b.Property<int>("EventGroupId")
                        .HasColumnType("integer")
                        .HasColumnName("event_group_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("EventGroupId", "UserId")
                        .HasName("pk_event_group_members");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_event_group_members_user_id");

                    b.ToTable("event_group_members", (string)null);
                });

            modelBuilder.Entity("TimeScout.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("EventGroupUser", b =>
                {
                    b.HasOne("TimeScout.API.Models.EventGroup", null)
                        .WithMany()
                        .HasForeignKey("EventGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_group_user_groups_event_groups_id");

                    b.HasOne("TimeScout.API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_group_user_users_members_id");
                });

            modelBuilder.Entity("TimeScout.API.Models.Event", b =>
                {
                    b.HasOne("TimeScout.API.Models.EventGroup", "EventGroup")
                        .WithMany("Events")
                        .HasForeignKey("EventGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_events_event_groups_event_group_id");

                    b.HasOne("TimeScout.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_events_users_user_id");

                    b.Navigation("EventGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeScout.API.Models.EventGroupMember", b =>
                {
                    b.HasOne("TimeScout.API.Models.EventGroup", "EventGroup")
                        .WithMany()
                        .HasForeignKey("EventGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_group_members_event_groups_event_group_id");

                    b.HasOne("TimeScout.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_group_members_users_user_id");

                    b.Navigation("EventGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeScout.API.Models.EventGroup", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
