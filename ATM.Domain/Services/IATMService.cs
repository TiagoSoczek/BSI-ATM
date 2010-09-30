namespace ATM.Domain.Services
{
	using System.ServiceModel;

	[ServiceContract]
	public interface IATMService
	{
		[OperationContract]
		void Sacar(string login, string cifra);

		[OperationContract]
		void Depositar(string login, string cifra);

		[OperationContract]
		string VisualizarSaldo(string login);
	}
}