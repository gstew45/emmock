using Emmock.Application.Interfaces;
using Emmock.Application.ViewModels.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Emmock.Desktop.Controls
{
	/// <summary>
	/// Interaction logic for Toolbox.xaml
	/// </summary>
	public partial class Toolbox : UserControl
	{
		private readonly IToolboxViewModel m_toolboxViewModel;

		public Toolbox()
		{
			InitializeComponent();

			m_toolboxViewModel = DataContext as IToolboxViewModel;
		}

		private void ToolboxItem_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (sender is FrameworkElement element && element.DataContext is IToolboxItem toolboxItem)
				{
					DragDrop.DoDragDrop(element, new DataObject(typeof(IToolboxItem), toolboxItem), DragDropEffects.Copy);
				}
			}
		}
	}
}
