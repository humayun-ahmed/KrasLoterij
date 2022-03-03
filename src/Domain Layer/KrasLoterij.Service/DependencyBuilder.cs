using System;
using AutoMapper;
using Infrastructure.Repository;
using Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NederlandseLoterij.KrasLoterij.Repository;
using NederlandseLoterij.KrasLoterij.Service.Contracts;
using NederlandseLoterij.KrasLoterij.Service.Mapper;

namespace NederlandseLoterij.KrasLoterij.Service
{
    public static class DependencyBuilder
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region DB

            services.AddDbContext<KrasLoterijContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(KrasLoterijContext))));

            #endregion

            #region Services

            services.AddScoped<ILotteryService, LotteryService>();
            services.AddScoped<ILotteryQueryService, LotteryQueryService>();

            var profile = new MapperProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapperConfiguration.CompileMappings();
            services.AddSingleton<IMapper>(x => new AutoMapper.Mapper(mapperConfiguration));
            #endregion

            #region Repositories

            services.AddScoped<BaseContext, KrasLoterijContext>();
            services.AddScoped<IRepository, Infrastructure.Repository.Repository>();
            services.AddScoped<IReadOnlyRepository, Infrastructure.Repository.Repository>();

            #endregion
        }
    }
}