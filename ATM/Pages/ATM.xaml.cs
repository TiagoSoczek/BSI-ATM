namespace ATM.Pages
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using Security.Services;

	/// <summary>
	/// Interaction logic for ATM.xaml
	/// </summary>
	public partial class ATM : Page
	{
		private readonly string _login;
		private int _qtdeVisitas;

		public ATM(string login)
		{
			_login = login;
			InitializeComponent();

			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_qtdeVisitas++;

			if (_qtdeVisitas > 1)
			{
				MessageBox.Show("Sessão expirada");

				Sair_Click(sender, null);

				return;
			}

			lblUsuario.Content = _login;
		}

		private void Sacar(object sender, RoutedEventArgs e)
		{
			try
			{
				string cifra = SegurancaService.Criptografar(txtQuantiaSaque.Text);

				lblQuantiaSaqueCripto.Content = cifra;

				App.ObterATMService().Sacar(_login, cifra);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Não foi possível efetuar o saque devido ao seguinte motivo: \n" + ex.Message);
			}
		}

		private void Depositar(object sender, RoutedEventArgs e)
		{
			try
			{
				string cifra = SegurancaService.Criptografar(txtQuantiaDeposito.Text);

				lblQuantiaDepositoCripto.Content = cifra;

				App.ObterATMService().Depositar(_login, cifra);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Não foi possível efetuar o depósito devido ao seguinte motivo: \n" + ex.Message);
			}
		}

		private void VisualizarSaldo(object sender, RoutedEventArgs e)
		{
			try
			{
				string cifra = App.ObterATMService().VisualizarSaldo(_login);

				lblSaldoCripto.Content = cifra;

				lblSaldo.Content = FormatarMoeda(SegurancaService.Decriptografar(cifra));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Não foi possível visualizar o saldo devido ao seguinte motivo: \n" + ex.Message);
			}
		}

		private object FormatarMoeda(string valor)
		{
			return string.Format("{0:C}", Convert.ToDouble(valor));
		}

		private void Sair_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
			NavigationService.RemoveBackEntry();
		}
	}
}