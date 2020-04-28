using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace SMAPI.Migrations
{
    public partial class InitialMigration : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Comment",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                Text = c.String(),
                CommentPostId = c.Int(nullable: false),
                AuthorId = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.ApplicationUser", t => t.AuthorId)
            .ForeignKey("dbo.Post", t => t.CommentPostId)
            .Index(t => t.CommentPostId)
            .Index(t => t.AuthorId);

        CreateTable(
            "dbo.ApplicationUser",
            c => new
            {
                Id = c.String(nullable: false, maxLength: 128),
                Email = c.String(),
                EmailConfirmed = c.Boolean(nullable: false),
                PasswordHash = c.String(),
                SecurityStamp = c.String(),
                PhoneNumber = c.String(),
                PhoneNumberConfirmed = c.Boolean(nullable: false),
                TwoFactorEnabled = c.Boolean(nullable: false),
                LockoutEndDateUtc = c.DateTime(),
                LockoutEnabled = c.Boolean(nullable: false),
                AccessFailedCount = c.Int(nullable: false),
                UserName = c.String(),
            })
            .PrimaryKey(t => t.Id);

        CreateTable(
            "dbo.IdentityUserClaim",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                UserId = c.String(),
                ClaimType = c.String(),
                ClaimValue = c.String(),
                ApplicationUser_Id = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
            .Index(t => t.ApplicationUser_Id);

        CreateTable(
            "dbo.Like",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                PostId = c.Int(nullable: false),
                LikerId = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.Post", t => t.PostId)
            .ForeignKey("dbo.ApplicationUser", t => t.LikerId)
            .Index(t => t.PostId)
            .Index(t => t.LikerId);

        CreateTable(
            "dbo.Post",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                Title = c.String(),
                Text = c.String(),
                CreatedDate = c.DateTime(nullable: false),
                AuthorId = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.ApplicationUser", t => t.AuthorId)
            .Index(t => t.AuthorId);

        CreateTable(
            "dbo.Reply",
            c => new
            {
                Id = c.Int(nullable: false, identity: true),
                CommentId = c.Int(nullable: false),
                Text = c.String(),
                CommentPostId = c.Int(nullable: false),
                AuthorId = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.ApplicationUser", t => t.AuthorId)
            .ForeignKey("dbo.Post", t => t.CommentPostId)
            .ForeignKey("dbo.Comment", t => t.CommentId)
            .Index(t => t.CommentId)
            .Index(t => t.CommentPostId)
            .Index(t => t.AuthorId);

        CreateTable(
            "dbo.IdentityUserLogin",
            c => new
            {
                UserId = c.String(nullable: false, maxLength: 128),
                LoginProvider = c.String(),
                ProviderKey = c.String(),
                ApplicationUser_Id = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.UserId)
            .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
            .Index(t => t.ApplicationUser_Id);

        CreateTable(
            "dbo.IdentityUserRole",
            c => new
            {
                UserId = c.String(nullable: false, maxLength: 128),
                RoleId = c.String(),
                ApplicationUser_Id = c.String(maxLength: 128),
                IdentityRole_Id = c.String(maxLength: 128),
            })
            .PrimaryKey(t => t.UserId)
            .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
            .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
            .Index(t => t.ApplicationUser_Id)
            .Index(t => t.IdentityRole_Id);

        CreateTable(
            "dbo.IdentityRole",
            c => new
            {
                Id = c.String(nullable: false, maxLength: 128),
                Name = c.String(),
            })
            .PrimaryKey(t => t.Id);

    }

    public override void Down()
    {
        DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
        DropForeignKey("dbo.Comment", "CommentPostId", "dbo.Post");
        DropForeignKey("dbo.Comment", "AuthorId", "dbo.ApplicationUser");
        DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
        DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
        DropForeignKey("dbo.Like", "LikerId", "dbo.ApplicationUser");
        DropForeignKey("dbo.Like", "PostId", "dbo.Post");
        DropForeignKey("dbo.Reply", "CommentId", "dbo.Comment");
        DropForeignKey("dbo.Reply", "CommentPostId", "dbo.Post");
        DropForeignKey("dbo.Reply", "AuthorId", "dbo.ApplicationUser");
        DropForeignKey("dbo.Post", "AuthorId", "dbo.ApplicationUser");
        DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
        DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
        DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
        DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
        DropIndex("dbo.Reply", new[] { "AuthorId" });
        DropIndex("dbo.Reply", new[] { "CommentPostId" });
        DropIndex("dbo.Reply", new[] { "CommentId" });
        DropIndex("dbo.Post", new[] { "AuthorId" });
        DropIndex("dbo.Like", new[] { "LikerId" });
        DropIndex("dbo.Like", new[] { "PostId" });
        DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
        DropIndex("dbo.Comment", new[] { "AuthorId" });
        DropIndex("dbo.Comment", new[] { "CommentPostId" });
        DropTable("dbo.IdentityRole");
        DropTable("dbo.IdentityUserRole");
        DropTable("dbo.IdentityUserLogin");
        DropTable("dbo.Reply");
        DropTable("dbo.Post");
        DropTable("dbo.Like");
        DropTable("dbo.IdentityUserClaim");
        DropTable("dbo.ApplicationUser");
        DropTable("dbo.Comment");
    }
}