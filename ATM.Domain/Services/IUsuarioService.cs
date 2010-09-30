namespace ATM.Domain.Services
{
	using System.Collections.Generic;
	using System.ServiceModel;
	using DTO;

	[ServiceContract]
	public interface IUsuarioService
	{
		[OperationContract]
		List<UsuarioDto> ObterTodosUsuarios();

		[OperationContract]
		void CriarUsuario(string login, string senha, double saldo);

		[OperationContract]
		bool Login(string login, string senha);

		[OperationContract]
		void RemoverTodos();
	}
}