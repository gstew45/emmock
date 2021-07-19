using Emmock.Application.Factories;
using Emmock.Application.Services;
using Emmock.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Emmock.Application
{
	public static class DependencyInjection
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddTransient<ISideBarContextFactory, SideBarContextFactory>();
			services.AddTransient<IPageFactory, PageFactory>();
			services.AddSingleton<BreadcrumbNavigationViewModel>();

			services.AddTransient<HomeViewModel>()
				.AddTransient<IPageViewModel, HomeViewModel>(s => s.GetService<HomeViewModel>());

			services.AddTransient<RigsViewModel>()
				.AddTransient<IPageViewModel, RigsViewModel>(s => s.GetService<RigsViewModel>());

			services.AddTransient<RigDetailsViewModel>()
				.AddTransient<IPageViewModel, RigDetailsViewModel>(s => s.GetService<RigDetailsViewModel>());

			services.AddTransient<EquipmentDetailsViewModel>()
				.AddTransient<IPageViewModel, EquipmentDetailsViewModel>(s => s.GetService<EquipmentDetailsViewModel>());

			services.AddTransient<CreateNewRigViewModel>()
				.AddTransient<IPageViewModel, CreateNewRigViewModel>(s => s.GetService<CreateNewRigViewModel>());

			services.AddTransient<CreateSystemViewModel>()
				.AddTransient<IPageViewModel, CreateSystemViewModel>(s => s.GetService<CreateSystemViewModel>());

			services.AddTransient<CreateNewEquipmentViewModel>()
				.AddTransient<IPageViewModel, CreateNewEquipmentViewModel>(s => s.GetService<CreateNewEquipmentViewModel>());

			services.AddTransient<TestingViewModel>()
				.AddTransient<IPageViewModel, TestingViewModel>(s => s.GetService<TestingViewModel>());

			services.AddTransient<RecentTestsViewModel>()
				.AddTransient<IPageViewModel, RecentTestsViewModel>(s => s.GetService<RecentTestsViewModel>());

			services.AddTransient<MainViewModel>();
		}
	}
}
