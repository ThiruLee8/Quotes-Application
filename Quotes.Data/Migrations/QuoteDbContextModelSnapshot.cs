﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quotes.Data.DatabaseContext;

#nullable disable

namespace Quotes.Data.Migrations
{
    [DbContext(typeof(QuoteDbContext))]
    partial class QuoteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("QuoteTag", b =>
                {
                    b.Property<int>("QuotesQuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuotesQuoteId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("QuoteTag");
                });

            modelBuilder.Entity("Quotes.Data.EntityModals.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InspirationalQuote")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuoteStageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuoteId");

                    b.HasIndex("QuoteStageId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Quotes.Data.EntityModals.QuoteStage", b =>
                {
                    b.Property<int>("QuoteStageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuoteStageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("QuoteStageId");

                    b.ToTable("QuoteStages");

                    b.HasData(
                        new
                        {
                            QuoteStageId = 1,
                            IsActive = true,
                            QuoteStageName = "Created"
                        },
                        new
                        {
                            QuoteStageId = 2,
                            IsActive = true,
                            QuoteStageName = "Approved Validation"
                        },
                        new
                        {
                            QuoteStageId = 3,
                            IsActive = true,
                            QuoteStageName = "Rejected Validation"
                        },
                        new
                        {
                            QuoteStageId = 4,
                            IsActive = true,
                            QuoteStageName = "Approved"
                        },
                        new
                        {
                            QuoteStageId = 5,
                            IsActive = true,
                            QuoteStageName = "Rejected"
                        });
                });

            modelBuilder.Entity("Quotes.Data.EntityModals.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("QuoteTag", b =>
                {
                    b.HasOne("Quotes.Data.EntityModals.Quote", null)
                        .WithMany()
                        .HasForeignKey("QuotesQuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quotes.Data.EntityModals.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quotes.Data.EntityModals.Quote", b =>
                {
                    b.HasOne("Quotes.Data.EntityModals.QuoteStage", "Stage")
                        .WithMany()
                        .HasForeignKey("QuoteStageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stage");
                });
#pragma warning restore 612, 618
        }
    }
}
