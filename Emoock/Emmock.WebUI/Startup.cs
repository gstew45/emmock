using BlazorFluentUI;
using Emmock.Application.ViewModels;
using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Persistance;
using Emmock.Persistance.Serializers;
using Emmock.Services;
using Emmock.WebUI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace Emmock.WebUI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddBlazorFluentUI();

			services.AddSingleton<WeatherForecastService>();

			services.AddTransient<IFileSystem, FileSystem>();
			services.AddTransient<ISerializer, EmmockJsonSerializer>();

			services.AddSingleton<IEquipmentRepository, FileEquipmentStore>();
			services.AddSingleton<IRigTemplateRepository, FileRigTemplateStore>();
			services.AddSingleton<IRigRepository, FileRigStore>();

			services.AddTransient<IRigGenerator, RigGenerator>();
			services.AddTransient<IRigService, FileRigService>();
			services.AddTransient<IEquipmentService, FileEquipmentService>();

			services.AddTransient<RigsViewModel>();
			services.AddTransient<CreateNewRigViewModel>();
			services.AddTransient<RigDetailsViewModel>();
			services.AddTransient<EquipmentDetailsViewModel>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
