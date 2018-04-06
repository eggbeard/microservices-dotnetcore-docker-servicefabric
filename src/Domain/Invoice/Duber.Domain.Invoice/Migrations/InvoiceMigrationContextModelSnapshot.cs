﻿// <auto-generated />
using Duber.Domain.Invoice.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Duber.Domain.Invoice.Migrations
{
    [DbContext(typeof(InvoiceMigrationContext))]
    partial class InvoiceMigrationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Duber.Domain.Invoice.Model.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<double>("Distance");

                    b.Property<TimeSpan>("Duration");

                    b.Property<decimal>("Fee");

                    b.Property<int>("PaymentMethodId");

                    b.Property<decimal>("Total");

                    b.HasKey("InvoiceId");

                    b.ToTable("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
