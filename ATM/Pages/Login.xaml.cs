namespace ATM.Pages
{
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		public Login()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			bool resultado = App.ObterUsuarioService().Login(textBox1.Text, textBox2.Password);

			if (!resultado)
			{
				MessageBox.Show("Login ou senha incorreta");

				textBox2.Focus();
			}
			else
			{
				NavigationService.Navigate(new ATM(textBox1.Text));

				textBox1.Text = string.Empty;
				textBox2.Password = string.Empty;
			}
		}
	}
}