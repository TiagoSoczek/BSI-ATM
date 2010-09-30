namespace ATM.Core.Services
{
	using System.Collections.Generic;
	using System.Linq;
	using Domain.DTO;
	using Domain.Services;
	using Model;
	using Security.Services;

	public class UsuarioService : IUsuarioService
	{
		#region IUsuarioService Members

		public List<UsuarioDto> ObterTodosUsuarios()
		{
			var container = new ModelContainer();

			return ToDto(container.Usuario.ToList());
		}

		public void CriarUsuario(string login, string senha, double saldo)
		{
			var container = new ModelContainer();

			Usuario usuario = Usuario.CreateUsuario(login, senha, saldo);

			usuario.Senha = SegurancaService.GerarHash(usuario.Senha);

			container.Usuario.AddObject(usuario);

			container.SaveChanges();
		}

		public bool Login(string login, string senha)
		{
			var container = new ModelContainer();

			Usuario usuario = container.Usuario.Where(u => u.Login == login).FirstOrDefault();

			if (usuario == null)
			{
				return false;
			}

			return usuario.Senha == SegurancaService.GerarHash(senha);
		}

		public void RemoverTodos()
		{
			var container = new ModelContainer();

			foreach (Usuario usuario in container.Usuario)
			{
				container.DeleteObject(usuario);
			}

			container.SaveChanges();
		}

		#endregion

		private List<UsuarioDto> ToDto(List<Usuario> usuarios)
		{
			return usuarios.Select(u => new UsuarioDto {Login = u.Login, Saldo = u.Saldo}).ToList();
		}
	}
}