namespace ATM.Pages
{
	using System;
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for GerenciarUsuario.xaml
	/// </summary>
	public partial class GerenciarUsuario : Page
	{
		public GerenciarUsuario()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			App.ObterUsuarioService().CriarUsuario(textBox1.Text, textBox2.Password, Convert.ToDouble(textBox3.Text));

			NavigationService.GoBack();
			NavigationService.RemoveBackEntry();
		}
	}
}