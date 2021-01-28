using Canducci.Pagination;
using Dapper;
using DesafioLuby.Controllers;
using RestSharp;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Models
{
    public class ServiceHorasLancamento : ServiceBase
    {
        public async Task<PaginatedRest<HorasLancamentoModel>> SelectAsync(int? IdDesenvolvedor, int? page)
        {

            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var list = con.Query<HorasLancamentoModel>("SELECT * FROM HorasLancamento" + (IdDesenvolvedor.HasValue && IdDesenvolvedor.Value > 0 ? " WHERE IdHorasLancamento = " + IdDesenvolvedor.Value : ""));

                    var result = await list.OrderBy(c => c.IdDesenvolvedor).ToPaginatedRestAsync(page.Value, 10);

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return null;

            }

        }

        public async Task<bool> InsertAsync(HorasLancamentoModel horas)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {

                    con.Open();
                    var query = "SET DATEFORMAT DMY INSERT INTO HorasLancamento Values (@dataInicio, @dataFim, @IdDesenvolvedor, @IdProjeto, @dataCriacao)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@dataInicio", horas.DataInicio);
                        command.Parameters.AddWithValue("@dataFim", horas.DataFim);
                        command.Parameters.AddWithValue("@IdDesenvolvedor", horas.IdDesenvolvedor);
                        command.Parameters.AddWithValue("@IdProjeto", horas.IdProjeto);
                        command.Parameters.AddWithValue("@dataCriacao", DateTime.Now);

                        int result = await command.ExecuteNonQueryAsync();
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    con.Close();
                }

                return true;
            }
        }


    }
}
