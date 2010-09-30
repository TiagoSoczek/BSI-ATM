namespace ATM.Pages
{
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for Usuarios.xaml
	/// </summary>
	public partial class Usuarios : Page
	{
		public Usuarios()
		{
			InitializeComponent();

			Loaded += Load;
		}

		private void Load(object sender, RoutedEventArgs e)
		{
			dataGrid1.ItemsSource = App.ObterUsuarioService().ObterTodosUsuarios();
		}
	}
}