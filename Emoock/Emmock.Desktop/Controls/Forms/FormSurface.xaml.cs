using Emmock.Application.Interfaces;
using Emmock.Application.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Emmock.Desktop.Controls
{
	/// <summary>
	/// Interaction logic for FormSurface.xaml
	/// </summary>
	public partial class FormSurface : UserControl
	{
		private IFormSurfaceViewModel m_formSurfaceViewModel;

		public FormSurface()
		{
			InitializeComponent();
		}

		private void Border_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(IToolboxItem)))
			{
				IToolboxItem toolBoxItem = (IToolboxItem)e.Data.GetData(typeof(IToolboxItem));
				m_formSurfaceViewModel.CreateFormItemCommand.Execute(toolBoxItem);
			}
		}

		private void FormSurface_Loaded(object sender, RoutedEventArgs e)
		{
			m_formSurfaceViewModel = DataContext as IFormSurfaceViewModel;
		}
	}
}
