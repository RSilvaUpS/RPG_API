// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TBRPGF_API.Data.Context;

#nullable disable

namespace TBRPGFAPI.Migrations
{
    [DbContext(typeof(TBRPGDBContext))]
    partial class TBRPGDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TBRPGF_API.Heroes.Armor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageReducitonMaximum")
                        .HasColumnType("int");

                    b.Property<int>("DamageReducitonMinimum")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Armors");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccuracyRate")
                        .HasColumnType("int");

                    b.Property<int?>("ArmorId")
                        .HasColumnType("int");

                    b.Property<int>("AttackMaximum")
                        .HasColumnType("int");

                    b.Property<int>("AttackMinimum")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HPMax")
                        .HasColumnType("int");

                    b.Property<int>("HPMin")
                        .HasColumnType("int");

                    b.Property<int>("HeroClassId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPlayable")
                        .HasColumnType("bit");

                    b.Property<int>("ManaMax")
                        .HasColumnType("int");

                    b.Property<int>("ManaMin")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PortraitLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<float>("SpellModifier")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ArmorId");

                    b.HasIndex("HeroClassId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.HeroClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HeroClasses");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.HeroSpellList", b =>
                {
                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.Property<int>("SpellId")
                        .HasColumnType("int");

                    b.HasKey("HeroId", "SpellId");

                    b.HasIndex("SpellId");

                    b.ToTable("HeroSpellList");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.Spell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageMax")
                        .HasColumnType("int");

                    b.Property<int>("DamageMin")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManaCost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpellType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.Hero", b =>
                {
                    b.HasOne("TBRPGF_API.Heroes.Armor", "Armor")
                        .WithMany()
                        .HasForeignKey("ArmorId");

                    b.HasOne("TBRPGF_API.Heroes.HeroClass", "HeroClass")
                        .WithMany()
                        .HasForeignKey("HeroClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Armor");

                    b.Navigation("HeroClass");
                });

            modelBuilder.Entity("TBRPGF_API.Heroes.HeroSpellList", b =>
                {
                    b.HasOne("TBRPGF_API.Heroes.Hero", "Hero")
                        .WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TBRPGF_API.Heroes.Spell", "Spell")
                        .WithMany()
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("Spell");
                });
#pragma warning restore 612, 618
        }
    }
}
