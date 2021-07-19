using System.ComponentModel;

namespace Emmock.Application.Supporting
{
	/// <summary>
	///		Base class for ViewModels with INotifyPropertyChanged
	/// </summary>
	public class ObservableObject : INotifyPropertyChanged
	{
		/// <summary> PropertyChanged event </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///		Handler for PropertyChanged Event
		/// </summary>
		/// <param name="property"></param>
		public void OnPropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
