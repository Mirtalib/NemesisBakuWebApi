﻿using Application.IRepositories;
using Application.IRepositories.IAdminRepository;
using Application.IRepositories.ICategoryRepository;
using Application.IRepositories.IClientFavoriShoesRepository;
using Application.IRepositories.IClientRepository;
using Application.IRepositories.IClientShoeShoppingListRepository;
using Application.IRepositories.ICourierRepository;
using Application.IRepositories.IOrderCommentRepository;
using Application.IRepositories.IOrderRepository;
using Application.IRepositories.IShoeCountSizeRepository;
using Application.IRepositories.IShoesCommentRepository;
using Application.IRepositories.IShoesRepository;
using Application.IRepositories.IStoreRepository;
using Application.Models.Configuration;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IAuthServices;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;
using FluentValidation;
using Infrastructure.Services.AuthServices;
using Infrastructure.Services.HelperServices;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Repositories;
using Persistence.Repositories.AdminRepository;
using Persistence.Repositories.CategoryRepository;
using Persistence.Repositories.ClientFavoriShoesRepository;
using Persistence.Repositories.ClientRepository;
using Persistence.Repositories.ClientShoeShoppingListRepository;
using Persistence.Repositories.CourierRepository;
using Persistence.Repositories.OrderCommentRepository;
using Persistence.Repositories.OrderRepository;
using Persistence.Repositories.ShoeCountSizeRepository;
using Persistence.Repositories.ShoesCommentRepository;
using Persistence.Repositories.ShoesRepository;
using Persistence.Repositories.StoreRepository;
using System.Text;
using WebApi.Validation;

namespace WebApi.Extensions
{
    public static class Extension
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "My Api - V1",
                        Version = "v1",
                    }
                );

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Jwt Authorization header using the Bearer scheme/ \r\r\r\n Enter 'Bearer' [space] and then token in the text input below. \r\n\r\n Example : \"Bearer f3c04qc08mh3n878\""
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJWTService, JWTService>();

            var jwtConfig = new JWTConfiguration();
            configuration.GetSection("JWT").Bind(jwtConfig);

            services.AddSingleton(jwtConfig);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, setup =>
            {
                setup.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICourierService, CourierService>();
            services.AddScoped<IAdminService, AdminService>();
            //services.AddScoped<IPassHashService, PassHashService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IStoreService, StoreService>();


            services.AddTransient<IValidator<AddShoeDto>, AddShoeDtoValidator>();
            
            //var smtpConfig = new SMTPConfiguration();
            //configuration.GetSection("SMTP").Bind(smtpConfig);
            //services.AddSingleton(smtpConfig);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReadCourierRepository, ReadCourierRepository>();
            services.AddScoped<IWriteCourierRepository, WriteCourierRepository>();

            services.AddScoped<IWriteClientRepository, WriteClientRepository>();
            services.AddScoped<IReadClientRepository, ReadClientRepository>();

            services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
            services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();

            services.AddScoped<IReadStoreRepository, ReadStoreRepository>();
            services.AddScoped<IWriteStoreRepository, WriteStoreRepository>();
            
            services.AddScoped<IReadCategoryRepository, ReadCategoryRepository>();
            services.AddScoped<IWriteCategoryRepository, WriteCategoryRepository>();

            services.AddScoped<IReadShoesCommentRepository, ReadShoesCommentRepository>();
            services.AddScoped<IWriteShoesCommentRepository, WriteShoesCommentRepository>();

            services.AddScoped<IReadAdminRepository, ReadAdminRepository>();
            services.AddScoped<IWriteAdminRepository, WriteAdminRepository>();

            services.AddScoped<IReadShoesRepository, ReadShoesRepository>();
            services.AddScoped<IWriteShoesRepository, WriteShoesRepository>();
            
            services.AddScoped<IReadOrderCommentRepository, ReadOrderCommentRepository>();
            services.AddScoped<IWriteOrderCommentRepository, WriteOrderCommentRepository>();


            services.AddScoped<IReadShoeCountSizeRepository, ReadShoeCountSizeRepository>();
            services.AddScoped<IWriteShoeCountSizeRepository, WriteShoeCountSizeRepository>();

            services.AddScoped<IReadClientFavoriShoesRepository, ReadClientFavoriShoesRepository>();
            services.AddScoped<IWriteClientFavoriShoesRepository, WriteClientFavoriShoesRepository>();

            services.AddScoped<IReadClientShoeShoppingListRepository, ReadClientShoeShoppingListRepository>();
            services.AddScoped<IWriteClientShoeShoppingListRepository, WriteClientShoeShoppingListRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
