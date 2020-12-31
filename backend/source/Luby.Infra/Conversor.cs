namespace Luby.Infra
{
    public static class Conversor
    {
        public static  Luby.Domain.Models.Desenvolvedor ConverterParaDominio(Luby.Infra.Context.Desenvolvedor infra)
        {
            try
            {
                var dominio = new Luby.Domain.Models.Desenvolvedor(
                    infra.Nome,
                    infra.Cpf,
                    infra.Cargo,
                    infra.Email,
                    infra.Login,
                    infra.Senha);
                return dominio;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static  Luby.Infra.Context.Desenvolvedor ConverterParaModelo(Luby.Domain.Models.Desenvolvedor modelo)
        {
            try
            {
                var infra = new Luby.Infra.Context.Desenvolvedor();
                infra.Nome = modelo.Nome;
                infra.Cpf = modelo.Cpf;
                infra.Cargo = modelo.Cargo;
                infra.Email = modelo.Email;
                infra.Login = modelo.Login;
                infra.Senha = modelo.Senha;
                return infra;
            }
            catch (System.Exception)
            {
                return new Luby.Infra.Context.Desenvolvedor();
            }
        }
    }
}