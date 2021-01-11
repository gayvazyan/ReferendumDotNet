﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Referendum.core.Entities;

namespace Referendum.core.Migrations
{
    [DbContext(typeof(ReferendumContext))]
    [Migration("20210111100842_NewMigration")]
    partial class NewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Referendum.core.Entities.CitizenDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Opaque");

                    b.Property<int?>("ReferendumId");

                    b.Property<string>("Ssn");

                    b.Property<string>("Time");

                    b.HasKey("Id");

                    b.ToTable("CitizenDb");
                });

            modelBuilder.Entity("Referendum.core.Entities.CommunitiesDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommunityCode");

                    b.Property<string>("CommunityName");

                    b.HasKey("Id");

                    b.ToTable("CommunitiesDb");
                });

            modelBuilder.Entity("Referendum.core.Entities.ReferendumDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommunityId");

                    b.Property<int?>("ConnectionCount");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired();

                    b.Property<bool?>("IsActive");

                    b.Property<string>("Question")
                        .IsRequired();

                    b.Property<DateTime?>("StartDate")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ReferendumDb");
                });

            modelBuilder.Entity("Referendum.core.Entities.UserDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("UserDb");

                    b.HasData(
                        new { Id = 1, FirstName = "Garegin", LastName = "Ayvazyan", Login = "Garegin", Password = "Sa123456!" },
                        new { Id = 2, FirstName = "Yelena", LastName = "Ayvazyan", Login = "Yelena", Password = "Sa123456" },
                        new { Id = 3, FirstName = "Meline", LastName = "Davtyan", Login = "Meline", Password = "Sa123456" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
