namespace ATM
{
	using System;
	using System.Windows;
	using ATWService;
	using ATWService2;
	using Pages;

	/// <summary>
	/// Interaction logic for Principal.xaml
	/// </summary>
	public partial class Principal : Window
	{
		public Principal()
		{
			InitializeComponent();

			Loaded += Login_Click;
		}

		private void VerUsuarios_Click(object sender, RoutedEventArgs e)
		{
			frame1.Navigate(new Uri("pages/Usuarios.xaml", UriKind.RelativeOrAbsolute));
		}

		private void CriarUsuario_OnClick(object sender, RoutedEventArgs e)
		{
			frame1.Navigate(new GerenciarUsuario());
		}

		private void RemoverUsuarios_OnClick(object sender, RoutedEventArgs e)
		{
			App.ObterUsuarioService().RemoverTodos();

			Login_Click(sender, e);
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			ATWService2.ILicensingModuleservice service = new ILicensingModuleservice();

			try
			{
				string requestLicenseInfo = service.RequestLicenseInfo(new [] {new TLicenca {Id = 123, Observacao = "teste"}});
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}

			frame1.Navigate(new Uri("pages/Login.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Sair_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}