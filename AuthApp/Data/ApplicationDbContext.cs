﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AuthApp.Models;
using JikanDotNet;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AuthApp.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnimeLabel> AnimeLabels { get; set; }
        public virtual DbSet<UserAnimeLabel> UserAnimeLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);
            //modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => x.UserId);

            modelBuilder.Entity<AnimeLabel>().HasKey(x => x.Id);
            modelBuilder.Entity<UserAnimeLabel>().HasKey(x => new { x.UserId, x.AnimeId, x.LabelId });

            modelBuilder.Entity<FriendRelation>()
                .HasKey(e => new { e.UserId, e.FriendId });

            modelBuilder.Entity<FriendRelation>()
                .HasOne(e => e.User)
                .WithMany(e => e.IAmFriendsWith)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FriendRelation>()
                .HasOne(e => e.Friend)
                .WithMany(e => e.AreFriendsWithMe)
                .HasForeignKey(e => e.FriendId)
                .OnDelete(DeleteBehavior.NoAction);

            var userEmail = "test@test.com";
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = "f2bc0937-52d6-4012-bc65-91535266799d",
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpper(),
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEL6eBpwetmHd6NziWJg86HtmxOyznY2CUyRaYAlGxEdU423LR18jK5rXYu8pCTWpGw==",
                    SecurityStamp = "E4IMYKVLO574KFHY5KLX4YHR46TFGK4O",
                    ConcurrencyStamp = "271801b3-57ba-44f4-a84b-c8d149e691bc",
                    AccessFailedCount = 0,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true
                }
            );

            modelBuilder.Entity<AnimeLabel>().HasData(
                new AnimeLabel() { Id = 1, Name = "Liked" },
                new AnimeLabel() { Id = 2, Name = "Disliked" },
                new AnimeLabel() { Id = 3, Name = "Watched" },
                new AnimeLabel() { Id = 4, Name = "Dropped" },
                new AnimeLabel() { Id = 5, Name = "Won't watch" }
            );

            Jikan jikan = new Jikan();
            var searchAnime = jikan.SearchAnime("Sekaiichi Hatsukoi").GetAwaiter().GetResult();
            var yaoiId = searchAnime.Results.First().MalId;

            modelBuilder.Entity<UserAnimeLabel>().HasData(
                new UserAnimeLabel() { UserId = "f2bc0937-52d6-4012-bc65-91535266799d", AnimeId = yaoiId, LabelId = 1 },
                new UserAnimeLabel() { UserId = "f2bc0937-52d6-4012-bc65-91535266799d", AnimeId = yaoiId, LabelId = 3 }
                );
        }

    }
}
