using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOBY_API_Core6.Models;
using System.Text.Json.Serialization;
using Azure.Identity;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Service.IService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var projectId = "moby-177a8";
                options.Authority = $"https://securetoken.google.com/{projectId}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"https://securetoken.google.com/{projectId}",
                    ValidateAudience = true,
                    ValidAudience = projectId,
                    ValidateLifetime = true
                };
            });
        builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


        string connectionString = "";

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null)
        {
            connectionString = Environment.GetEnvironmentVariable("MobyDBLocallam")!;
        }
        else
        {
            connectionString = builder.Configuration.GetConnectionString("MobyDBAzure");
        }
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<MOBYContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<ICartDetailService, CartDetailService>();
        builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
        builder.Services.AddScoped<IBlogService, BlogService>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IReplyService, ReplyService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IUserAddressService, UserAddressService>();
        builder.Services.AddScoped<IBannerService, BannerService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IImageVerifyService, ImageVerifyService>();
        builder.Services.AddScoped<ITransationService, TransationService>();
        builder.Services.AddScoped<IRecordPenaltyService, RecordPenaltyService>();
        builder.Services.AddScoped<IBabyService, BabyService>();
        builder.Services.AddScoped<JsonToObj>();

        builder.Configuration.AddUserSecrets<Program>(true);
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Moby API" });
            swagger.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter Firebase access token",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey
                }
            );
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
    }
        });
        });

        var app = builder.Build();

        app.UseAuthentication();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("appsettings.json")
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

        }
        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}