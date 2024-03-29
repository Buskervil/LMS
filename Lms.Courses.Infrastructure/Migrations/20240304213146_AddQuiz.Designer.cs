﻿// <auto-generated />
using System;
using Lms.Courses.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lms.Courses.Infrastructure.Migrations
{
    [DbContext(typeof(CoursesContext))]
    [Migration("20240304213146_AddQuiz")]
    partial class AddQuiz
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Published")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.CourseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseSectionId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PreviousItemId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseSectionId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.CourseSection", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PreviousSection")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseSection", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("AllowMultipleAnswers")
                        .HasColumnType("boolean");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Learnings.Learning", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssignedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("FinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Learning", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Learnings.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CommittedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CourseItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LearningId")
                        .HasColumnType("uuid");

                    b.Property<int>("ScoreInPercent")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LearningId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Article", b =>
                {
                    b.HasBaseType("Lms.Courses.Domain.Courses.CourseItem");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Article", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Quiz", b =>
                {
                    b.HasBaseType("Lms.Courses.Domain.Courses.CourseItem");

                    b.ToTable("Quiz", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Video", b =>
                {
                    b.HasBaseType("Lms.Courses.Domain.Courses.CourseItem");

                    b.Property<string>("ContentLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Video", (string)null);
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Answer", b =>
                {
                    b.HasOne("Lms.Courses.Domain.Courses.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.CourseItem", b =>
                {
                    b.HasOne("Lms.Courses.Domain.Courses.CourseSection", null)
                        .WithMany("CourseItems")
                        .HasForeignKey("CourseSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.CourseSection", b =>
                {
                    b.HasOne("Lms.Courses.Domain.Courses.Course", null)
                        .WithMany("CourseSections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Question", b =>
                {
                    b.HasOne("Lms.Courses.Domain.Courses.Quiz", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Learnings.Progress", b =>
                {
                    b.HasOne("Lms.Courses.Domain.Learnings.Learning", null)
                        .WithMany("Progresses")
                        .HasForeignKey("LearningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Course", b =>
                {
                    b.Navigation("CourseSections");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.CourseSection", b =>
                {
                    b.Navigation("CourseItems");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Learnings.Learning", b =>
                {
                    b.Navigation("Progresses");
                });

            modelBuilder.Entity("Lms.Courses.Domain.Courses.Quiz", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
