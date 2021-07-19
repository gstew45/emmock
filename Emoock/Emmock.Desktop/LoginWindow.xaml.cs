using Emmock.Application.ViewModels.Authentication;
using System.Windows;
using System.Windows.Input;

namespace Emmock.Desktop
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow(LoginViewModel loginViewModel)
		{
			InitializeComponent();
			LoginViewModel = loginViewModel;
		}

		public LoginViewModel LoginViewModel { get; }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}
	}
}
