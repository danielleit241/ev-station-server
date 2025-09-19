using EV_Station.Application.IdentityCards.QueryHandlers;
using EV_Station.Infrastructure.Services.GeminiAi;

namespace EV_Station.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            AddSwagger(builder);
            AddDbContext(builder);
            AddApiVersioning(builder);
            AddCors(builder);
            AddAuthentication(builder);
            builder.Services.AddAuthorization();

            builder.Services.AddMediatR(typeof(RegisterUser).Assembly);
            builder.Services.AddMediatR(typeof(IdentityCardScanHandler).Assembly);

            builder.Services.AddAutoMapper(typeof(UserProfile));

            return builder;
        }

        private static void AddCors(IHostApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllClients", builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }

        private static void AddSwagger(IHostApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization: {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void AddDbContext(IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EVStationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        private static void AddApiVersioning(IHostApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Version")
                );
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        private static void AddAuthentication(IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var authHeader = context.Request.Headers["Authorization"].ToString();

                            if (!string.IsNullOrEmpty(authHeader))
                            {
                                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                                    context.Token = authHeader.Substring("Bearer ".Length).Trim();
                                else
                                    context.Token = authHeader.Trim();
                            }

                            return Task.CompletedTask;
                        }
                    };

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Jwt:Key"]!)
                        )
                    };
                });
        }

        public static void RegisterEndpointDefinitions(this IHostApplicationBuilder builder, Assembly assembly)
        {
            var types = assembly.ExportedTypes
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in types)
            {
                builder.Services.AddScoped(typeof(IEndpointDefinition), type);
            }
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ITesseractOcrService, TesseractOcrService>();
            builder.Services.AddScoped<IGeminiAiService, GeminiAiService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<DatabaseSeeder>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IProviderRepository, ProviderRepository>();


            return builder;
        }
    }
}