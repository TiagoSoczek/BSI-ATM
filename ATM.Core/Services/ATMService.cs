namespace ATM.Core.Services
{
	using System;
	using System.Linq;
	using Domain.Services;
	using Model;
	using Security.Services;

	public class ATMService : IATMService
	{
		#region IATMService Members

		public void Sacar(string login, string cifra)
		{
			var container = new ModelContainer();

			double quantia = Math.Abs(Convert.ToDouble(SegurancaService.Decriptografar(cifra)));

			Usuario usuarioAtual = container.Usuario.First(u => u.Login == login);

			if (usuarioAtual.Saldo < quantia)
			{
				throw new ApplicationException("Saldo insuficiente para efetuar o saque");
			}

			usuarioAtual.Saldo -= quantia;

			container.SaveChanges();
		}

		public void Depositar(string login, string cifra)
		{
			var container = new ModelContainer();

			double quantia = Math.Abs(Convert.ToDouble(SegurancaService.Decriptografar(cifra)));

			Usuario usuarioAtual = container.Usuario.First(u => u.Login == login);

			usuarioAtual.Saldo += quantia;

			container.SaveChanges();
		}

		public string VisualizarSaldo(string login)
		{
			var container = new ModelContainer();

			Usuario usuarioAtual = container.Usuario.First(u => u.Login == login);

			return SegurancaService.Criptografar(usuarioAtual.Saldo);
		}

		#endregion
	}
}