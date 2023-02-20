﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MOBY_API_Core6.Models
{
    public partial class MOBYContext : DbContext
    {
        public MOBYContext()
        {
        }

        public MOBYContext(DbContextOptions<MOBYContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<BlogCategory> BlogCategories { get; set; } = null!;
        public virtual DbSet<BriefItem> BriefItems { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartDetail> CartDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DetailItem> DetailItems { get; set; } = null!;
        public virtual DbSet<DetailItemRequest> DetailItemRequests { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.BannerId)
                    .ValueGeneratedNever()
                    .HasColumnName("BannerID");

                entity.Property(e => e.BannerLink).HasColumnName("Banner_Link");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogCategoryId).HasColumnName("Blog_CategoryID");

                entity.Property(e => e.BlogContent).HasColumnName("Blog_Content");

                entity.Property(e => e.BlogDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Create");

                entity.Property(e => e.BlogDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Update");

                entity.Property(e => e.BlogDescription).HasColumnName("Blog_Description");

                entity.Property(e => e.BlogStatus).HasColumnName("Blog_Status");

                entity.Property(e => e.BlogTitle).HasColumnName("Blog_Title");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.BlogCategory)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.BlogCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blogs_BlogCategories");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blogs_UserAccounts");
            });

            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.Property(e => e.BlogCategoryId).HasColumnName("Blog_CategoryID");

                entity.Property(e => e.BlogCategoryName).HasColumnName("Blog_Category_Name");
            });

            modelBuilder.Entity<BriefItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BriefItem");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");

                entity.Property(e => e.CategoryStatus).HasColumnName("Category_Status");

                entity.Property(e => e.ItemCode).HasColumnName("Item_Code");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemSalePrice).HasColumnName("Item_Sale_Price");

                entity.Property(e => e.ItemStatus).HasColumnName("Item_Status");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.SubCategoryId).HasColumnName("Sub_CategoryID");

                entity.Property(e => e.SubCategoryName).HasColumnName("Sub_Category_Name");

                entity.Property(e => e.SubCategoryStatus).HasColumnName("Sub_Category_Status");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CartDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Cart_Date_Create");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Carts_UserAccounts");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.Property(e => e.CartDetailId).HasColumnName("Cart_DetailID");

                entity.Property(e => e.CartDetailDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Cart_Detail_Date_Create");

                entity.Property(e => e.CartDetailDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Cart_Detail_Date_Update");

                entity.Property(e => e.CartDetailItemQuantity).HasColumnName("Cart_Detail_Item_Quantity");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CartStatus).HasColumnName("Cart_Status");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetailID_Carts");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetailID_Items");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryImage).HasColumnName("Category_Image");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");

                entity.Property(e => e.CategoryStatus).HasColumnName("Category_Status");
            });

            modelBuilder.Entity<DetailItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DetailItem");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");

                entity.Property(e => e.ItemCode).HasColumnName("Item_Code");

                entity.Property(e => e.ItemDateCreated)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Created");

                entity.Property(e => e.ItemDetailedDescription).HasColumnName("Item_Detailed_Description");

                entity.Property(e => e.ItemEstimateValue).HasColumnName("Item_Estimate_Value");

                entity.Property(e => e.ItemExpiredTime)
                    .HasColumnType("date")
                    .HasColumnName("Item_Expired_Time");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemMass).HasColumnName("Item_Mass");

                entity.Property(e => e.ItemQuanlity).HasColumnName("Item_Quanlity");

                entity.Property(e => e.ItemSalePrice).HasColumnName("Item_Sale_Price");

                entity.Property(e => e.ItemShareAmount).HasColumnName("Item_Share_Amount");

                entity.Property(e => e.ItemShippingAddress).HasColumnName("Item_Shipping_Address");

                entity.Property(e => e.ItemSize).HasColumnName("Item_Size");

                entity.Property(e => e.ItemSponsoredOrderShippingFee).HasColumnName("Item_Sponsored_Order_Shipping_Fee");

                entity.Property(e => e.ItemStatus).HasColumnName("Item_Status");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.SubCategoryId).HasColumnName("Sub_CategoryID");

                entity.Property(e => e.SubCategoryName).HasColumnName("Sub_Category_Name");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.Entity<DetailItemRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DetailItemRequest");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");

                entity.Property(e => e.ItemCode).HasColumnName("Item_Code");

                entity.Property(e => e.ItemDateCreated)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Created");

                entity.Property(e => e.ItemDetailedDescription).HasColumnName("Item_Detailed_Description");

                entity.Property(e => e.ItemExpiredTime)
                    .HasColumnType("date")
                    .HasColumnName("Item_Expired_Time");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemShareAmount).HasColumnName("Item_Share_Amount");

                entity.Property(e => e.ItemShippingAddress).HasColumnName("Item_Shipping_Address");

                entity.Property(e => e.ItemSponsoredOrderShippingFee).HasColumnName("Item_Sponsored_Order_Shipping_Fee");

                entity.Property(e => e.ItemStatus).HasColumnName("Item_Status");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.SubCategoryId).HasColumnName("Sub_CategoryID");

                entity.Property(e => e.SubCategoryName).HasColumnName("Sub_Category_Name");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemCode).HasColumnName("Item_Code");

                entity.Property(e => e.ItemDateCreated)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Created");

                entity.Property(e => e.ItemDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Update");

                entity.Property(e => e.ItemDetailedDescription).HasColumnName("Item_Detailed_Description");

                entity.Property(e => e.ItemEstimateValue).HasColumnName("Item_Estimate_Value");

                entity.Property(e => e.ItemExpiredTime)
                    .HasColumnType("date")
                    .HasColumnName("Item_Expired_Time");

                entity.Property(e => e.ItemMass).HasColumnName("Item_Mass");

                entity.Property(e => e.ItemQuanlity).HasColumnName("Item_Quanlity");

                entity.Property(e => e.ItemSalePrice).HasColumnName("Item_Sale_Price");

                entity.Property(e => e.ItemShareAmount).HasColumnName("Item_Share_Amount");

                entity.Property(e => e.ItemShippingAddress).HasColumnName("Item_Shipping_Address");

                entity.Property(e => e.ItemSize).HasColumnName("Item_Size");

                entity.Property(e => e.ItemSponsoredOrderShippingFee).HasColumnName("Item_Sponsored_Order_Shipping_Fee");

                entity.Property(e => e.ItemStatus).HasColumnName("Item_Status");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.SubCategoryId).HasColumnName("Sub_CategoryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_SubCategories1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_UserAccounts");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Update");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Items");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_UserAccounts");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId).HasColumnName("Sub_CategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName).HasColumnName("Sub_Category_Name");

                entity.Property(e => e.SubCategoryStatus).HasColumnName("Sub_Category_Status");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategories_Categories");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User_Accounts");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserAddress).HasColumnName("User_Address");

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserDateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("User_Date_Of_Birth");

                entity.Property(e => e.UserDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Update");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserMoreInformation).HasColumnName("User_More_Information");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(20)
                    .HasColumnName("User_Phone")
                    .IsFixedLength();

                entity.Property(e => e.UserSex).HasColumnName("User_Sex");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccounts_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
