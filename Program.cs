using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;
using MOBY_API_Core6.Repository.IRepository;
using System.Text.Json.Serialization;
using Azure.Identity;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
        builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
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
            connectionString = Environment.GetEnvironmentVariable("")!;
        }
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<MOBYContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ICartDetailRepository, CartDetailRepository>();
        builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
        builder.Services.AddScoped<IBlogRepository, BlogRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IReplyRepository, ReplyRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
        builder.Services.AddScoped<IBannerRepository, BannerRepository>();
        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        builder.Services.AddScoped<IImageVerifyRepository, ImageVerifyRepository>();
        builder.Services.AddScoped<ITransationRepository, TransationRepository>();
        builder.Services.AddScoped<IRecordPenaltyRepository, RecordPenaltyRepository>();
        builder.Services.AddScoped<IBabyRepository, BabyRepository>();
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