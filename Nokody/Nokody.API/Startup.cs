using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nokody.API.Data;
using Nokody.API.Data.Repositories.Implementation;
using Nokody.API.Domain;
using Nokody.API.Domain.Handlers;
using Nokody.API.Domain.Handlers.Accounts;
using Nokody.API.Domain.Handlers.Transactions;
using Nokody.API.Domain.Handlers.Users;
using Nokody.API.Domain.Model.Models;
using Nokody.API.Domain.Repositories;

namespace Nokody.API
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
      services.AddDbContext<NokodyModel>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));
      services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
      services.AddScoped<IQueryableUnitOfWork, SqlUnitOfWork>();
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddScoped(typeof(GetUsers), typeof(GetUsers));
      services.AddScoped(typeof(AuthenticateUser), typeof(AuthenticateUser));
      services.AddScoped(typeof(ValidateAccountBalance), typeof(ValidateAccountBalance));
      services.AddScoped(typeof(ExecuteTransaction), typeof(ExecuteTransaction));
      services.AddScoped(typeof(GetAccount), typeof(GetAccount));
      services.AddScoped(typeof(GetTransactions), typeof(GetTransactions));
      services.AddScoped(typeof(UpdateDeviceId), typeof(UpdateDeviceId));
      services.AddScoped(typeof(GetUser), typeof(GetUser));
      services.AddScoped(typeof(AddUser), typeof(AddUser));
      services.AddMvc();
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
