using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silent.Practices.DDD;
using Silent.Practices.EventStore;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Gameplay.Api.Controllers;
using Silent.Tetris.Gameplay.Api.Infrastructure;
using System;
using System.Collections.Generic;

namespace Silent.Tetris.Gameplay.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IComparer<Event<Guid>> eventComparer = Comparer<Event<Guid>>.Create((x, y) => x.Timestamp.CompareTo(y.Timestamp));
            services.AddSingleton<IEventStore<Guid, Event<Guid>>>(new MemoryEventStore<Guid, Event<Guid>>(eventComparer));
            services.AddTransient<IRepository<GameField, Guid>, MemoryGameFieldRepository>();
            services.AddTransient<IActiveGamesRegistry, ActiveGameRegistry>();
            services.AddTransient<IOnlineGameService, OnlineGameService>();
            services.AddTransient<IReplayGameService, ReplayGameService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
