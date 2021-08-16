using Emmock.Application.Factories;
using Emmock.Application.Interfaces;
using Emmock.Application.Messaging;
using Emmock.Core.Supporting;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Emmock.Application.ViewModels.Controls
{
	public class FormSurfaceViewModel : ObservableObject, IFormSurfaceViewModel
	{
		private readonly IMessengerHub m_messengerHub;
		private readonly IFormItemFactory m_formItemFactory;

		public FormSurfaceViewModel(IMessengerHub messengerHub, IFormItemFactory formItemFactory)
		{
			m_messengerHub = messengerHub;
			m_formItemFactory = formItemFactory;

			CreateFormItemCommand = new DelegateCommand<IToolboxItem>(CreateFormItem);
			SelectFormItemCommand = new DelegateCommand<IFormItem>(SelectFormItem);
		}

		public ICommand CreateFormItemCommand { get; }
		public ICommand SelectFormItemCommand { get; }

		public ObservableCollection<IFormItem> FormItems { get; } = new ObservableCollection<IFormItem>();

		public IFormItem SelectedFormItem => FormItems.FirstOrDefault(t => t.IsSelected);

		private void CreateFormItem(IToolboxItem toolboxItem)
		{
			IFormItem formItem = m_formItemFactory.GetFormItem(toolboxItem);
			FormItems.Add(formItem);

			SelectFormItem(formItem);
		}

		private void SelectFormItem(IFormItem formItemToSelect)
		{
			if (formItemToSelect is null)
				return;

			foreach (IFormItem formItem in FormItems)
			{
				if (formItem == formItemToSelect)
					formItemToSelect.IsSelected = !formItemToSelect.IsSelected;
				else
					formItem.IsSelected = false;
			}

			OnPropertyChanged(nameof(SelectedFormItem));

			m_messengerHub.Send(new FormSurfaceItemSelectionMessage(formItemToSelect));
		}
	}
}
