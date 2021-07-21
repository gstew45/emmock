using Emmock.Application;
using Emmock.Application.ViewModels;
using Emmock.Application.ViewModels.Authentication;
using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Persistance;
using Emmock.Persistance.Serializers;
using Emmock.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Abstractions;
using System.Windows;

namespace Emmock.Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application
	{
		private readonly IServiceProvider m_serviceProvider;

		public App(IServiceCollection services = null)
		{
			services = services ?? new ServiceCollection();
			ConfigureServices(services);
			m_serviceProvider = services.BuildServiceProvider();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			//Disable shutdown when the dialog closes
			Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

			var loginWindow = m_serviceProvider.GetService<LoginWindow>();
			if (loginWindow.ShowDialog() is true)
			{
				var mainWindow = m_serviceProvider.GetService<MainWindow>();

				//Re-enable normal shutdown mode.
				Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
				Current.MainWindow = mainWindow;
				mainWindow.Show();
			}
		}

		private void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IFileSystem, FileSystem>();
			services.AddTransient<ISerializer, EmmockJsonSerializer>();

			services.AddSingleton<IEquipmentRepository, FileEquipmentStore>();
			services.AddSingleton<IRigTemplateRepository, FileRigTemplateStore>();
			services.AddSingleton<IRigRepository, FileRigStore>();

			services.AddTransient<IRigGenerator, RigGenerator>();
			services.AddTransient<IRigService, FileRigService>();
			services.AddTransient<IEquipmentService, FileEquipmentService>();

			services.AddApplicationServices();

			services.AddSingleton<IPageService, WpfPageService>();

			services.AddTransient<LoginViewModel>();

			services.AddTransient<LoginWindow>();
			services.AddTransient<MainWindow>();
		}
	}
}
