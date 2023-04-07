using System;
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
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<DetailItem> DetailItems { get; set; } = null!;
        public virtual DbSet<DetailItemRequest> DetailItemRequests { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Reply> Replies { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestDetail> RequestDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<UserAddress> UserAddresses { get; set; } = null!;
        public virtual DbSet<ViewBlog> ViewBlogs { get; set; } = null!;
        public virtual DbSet<ViewReport> ViewReports { get; set; } = null!;
        public virtual DbSet<ViewReportBlog> ViewReportBlogs { get; set; } = null!;
        public virtual DbSet<ViewReportComment> ViewReportComments { get; set; } = null!;
        public virtual DbSet<ViewReportItem> ViewReportItems { get; set; } = null!;
        public virtual DbSet<ViewReportOrder> ViewReportOrders { get; set; } = null!;
        public virtual DbSet<ViewReportReply> ViewReportReplies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.BannerId).HasColumnName("BannerID");

                entity.Property(e => e.BannerLink).HasColumnName("Banner_Link");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateUpdate).HasColumnType("smalldatetime");
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

                entity.Property(e => e.ReasonDeny).HasColumnName("Reason_Deny");

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

                entity.Property(e => e.ItemDateCreated)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Created");

                entity.Property(e => e.ItemDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Update");

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

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Carts_UserAccounts");
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.Property(e => e.CartDetailId).HasColumnName("CartDetailID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetails_Carts");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CartDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartDetails_Items");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryImage).HasColumnName("Category_Image");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");

                entity.Property(e => e.CategoryStatus).HasColumnName("Category_Status");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Comments_Blogs");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_Comments_Items");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_UserAccounts");
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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DatePackage).HasColumnType("smalldatetime");

                entity.Property(e => e.DateReceived).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_UserAccounts1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Items");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.Property(e => e.ReplyId).HasColumnName("ReplyID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Replies_Comments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Replies_UserAccounts");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ReplyId).HasColumnName("ReplyID");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Reports_Blogs");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_Reports_Comments");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_Reports_Items");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Reports_Orders");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReplyId)
                    .HasConstraintName("FK_Reports_Replies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reports_UserAccounts");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.DateChangeStatus).HasColumnType("smalldatetime");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_UserAccounts");
            });

            modelBuilder.Entity<RequestDetail>(entity =>
            {
                entity.Property(e => e.RequestDetailId).HasColumnName("RequestDetailID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestDetails_Items");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDetails)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestDetails_Requests");
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

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.ToTable("UserAddress");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_UserAccounts");
            });

            modelBuilder.Entity<ViewBlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewBlog");

                entity.Property(e => e.BlogCategoryId).HasColumnName("Blog_CategoryID");

                entity.Property(e => e.BlogCategoryName).HasColumnName("Blog_Category_Name");

                entity.Property(e => e.BlogContent).HasColumnName("Blog_Content");

                entity.Property(e => e.BlogDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Create");

                entity.Property(e => e.BlogDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Update");

                entity.Property(e => e.BlogDescription).HasColumnName("Blog_Description");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogStatus).HasColumnName("Blog_Status");

                entity.Property(e => e.BlogTitle).HasColumnName("Blog_Title");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.Entity<ViewReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReport");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ReplyId).HasColumnName("ReplyID");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserName).HasColumnName("User_Name");
            });

            modelBuilder.Entity<ViewReportBlog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReportBlog");

                entity.Property(e => e.BlogDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Create");

                entity.Property(e => e.BlogDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Blog_Date_Update");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogStatus).HasColumnName("Blog_Status");

                entity.Property(e => e.BlogTitle).HasColumnName("Blog_Title");

                entity.Property(e => e.Expr7).HasColumnType("smalldatetime");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");
            });

            modelBuilder.Entity<ViewReportComment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReportComment");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogTitle).HasColumnName("Blog_Title");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateUpdate).HasColumnType("smalldatetime");

                entity.Property(e => e.Expr7).HasColumnType("smalldatetime");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");
            });

            modelBuilder.Entity<ViewReportItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReportItem");

                entity.Property(e => e.ItemCode).HasColumnName("Item_Code");

                entity.Property(e => e.ItemDateCreated)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Created");

                entity.Property(e => e.ItemDateUpdate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Item_Date_Update");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemStatus).HasColumnName("Item_Status");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserCodeReport).HasColumnName("User_CodeReport");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserDateCreateReport)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_DateCreateReport");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserGmailReport).HasColumnName("User_GmailReport");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserIdreport).HasColumnName("UserIDReport");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserImageReport).HasColumnName("User_ImageReport");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserNameReport).HasColumnName("User_NameReport");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");

                entity.Property(e => e.UserStausReport).HasColumnName("User_StausReport");
            });

            modelBuilder.Entity<ViewReportOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReportOrder");

                entity.Property(e => e.CodeUreport).HasColumnName("CodeUReport");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateCreateUreport)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DateCreateUReport");

                entity.Property(e => e.DatePackage).HasColumnType("smalldatetime");

                entity.Property(e => e.DateReceived).HasColumnType("smalldatetime");

                entity.Property(e => e.GmailUreport).HasColumnName("GmailUReport");

                entity.Property(e => e.Idureport).HasColumnName("IDUReport");

                entity.Property(e => e.ImageUreport).HasColumnName("ImageUReport");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.NameUreport).HasColumnName("NameUReport");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.ReputationUreport).HasColumnName("ReputationUReport");

                entity.Property(e => e.StatusUreport).HasColumnName("StatusUReport");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");
            });

            modelBuilder.Entity<ViewReportReply>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewReportReply");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogTitle).HasColumnName("Blog_Title");

                entity.Property(e => e.CodeUreport).HasColumnName("CodeUReport");

                entity.Property(e => e.DateCreate).HasColumnType("smalldatetime");

                entity.Property(e => e.DateCreateUreport)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DateCreateUReport");

                entity.Property(e => e.GmailUreport).HasColumnName("GmailUReport");

                entity.Property(e => e.Idureport).HasColumnName("IDUReport");

                entity.Property(e => e.ImageUreport).HasColumnName("ImageUReport");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemTitle).HasColumnName("Item_Title");

                entity.Property(e => e.NameUreport).HasColumnName("NameUReport");

                entity.Property(e => e.ReplyId).HasColumnName("ReplyID");

                entity.Property(e => e.ReportContent).HasColumnName("Report_Content");

                entity.Property(e => e.ReportDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Create");

                entity.Property(e => e.ReportDateResolve)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Report_Date_Resolve");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportStatus).HasColumnName("Report_Status");

                entity.Property(e => e.ReputationUreport).HasColumnName("ReputationUReport");

                entity.Property(e => e.StatusUreport).HasColumnName("StatusUReport");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserCode).HasColumnName("User_Code");

                entity.Property(e => e.UserDateCreate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("User_Date_Create");

                entity.Property(e => e.UserGmail).HasColumnName("User_Gmail");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserImage).HasColumnName("User_Image");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.Property(e => e.UserStatus).HasColumnName("User_Status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
