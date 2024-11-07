﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travelers.Infrastructure.DataAccess;

#nullable disable

namespace Travelers.Infrastructure.Migrations
{
    [DbContext(typeof(TravelersDbContext))]
    [Migration("20241107011926_Let_CreatedAt_Initialized_And_UpdatedAt_Nullable")]
    partial class Let_CreatedAt_Initialized_And_UpdatedAt_Nullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Travelers.Domain.Entities.User", b =>
                {
                    b.Property<long>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("IdUser"));

                    b.Property<Guid>("CoUserIdentifier")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TxAvatarUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TxEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TxName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TxPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TxRole")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
