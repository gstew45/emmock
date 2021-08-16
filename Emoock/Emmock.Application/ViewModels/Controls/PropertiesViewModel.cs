using Emmock.Application.Interfaces;
using Emmock.Application.Messaging;
using Emmock.Core.Models;
using Emmock.Core.Supporting;
using Prism.Commands;
using System.Windows.Input;

namespace Emmock.Application.ViewModels.Controls
{
	public class PropertiesViewModel : ObservableObject, IPropertiesViewModel
	{
		private readonly IMessengerHub m_messengerHub;

		private IFormItem m_selectedFormItem;

		public PropertiesViewModel(IMessengerHub messengerHub)
		{
			m_messengerHub = messengerHub;

			LinkEquipmentCommand = new DelegateCommand<Equipment>(LinkEquipment);
		}

		public ICommand LinkEquipmentCommand { get; }

		public IFormItem SelectedFormItem
		{
			get => m_selectedFormItem;
			set
			{
				m_selectedFormItem = value;
				OnPropertyChanged(nameof(SelectedFormItem));
			}
		}

		public void Initialize()
		{
			m_messengerHub.Register<FormSurfaceItemSelectionMessage>(this, HandleFormSurfaceItemSelected);
		}

		private void HandleFormSurfaceItemSelected(FormSurfaceItemSelectionMessage message)
		{
			SelectedFormItem = message.SelectedFormItem;
		}

		private void LinkEquipment(Equipment equipmentToLink)
		{
			SelectedFormItem.LinkedEquipment.Add(equipmentToLink);
		}
	}
}
