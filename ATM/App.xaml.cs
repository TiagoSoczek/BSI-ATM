namespace ATM
{
	using System.Configuration;
	using System.ServiceModel;
	using System.Windows;
	using Domain.Services;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IATMService ObterATMService()
		{
			return CriarCanal<IATMService>(ConfigurationManager.AppSettings["ATMServiceAddress"]);
		}

		public static IUsuarioService ObterUsuarioService()
		{
			return CriarCanal<IUsuarioService>(ConfigurationManager.AppSettings["UsuarioServiceAddress"]);
		}

		private static T CriarCanal<T>(string address)
		{
			return ChannelFactory<T>.CreateChannel(new NetTcpBinding("default"), new EndpointAddress(address));
		}
	}
}