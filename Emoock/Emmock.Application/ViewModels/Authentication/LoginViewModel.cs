using Emmock.Core.Supporting;

namespace Emmock.Application.ViewModels.Authentication
{
	public class LoginViewModel : ObservableObject
	{
		private string m_username = string.Empty;
		private string m_password = string.Empty;

		public LoginViewModel()
		{
		}

		public string Username 
		{
			get => m_username;
			set
			{
				m_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		public string Password
		{
			get => m_password;
			set
			{
				m_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
	}
}
