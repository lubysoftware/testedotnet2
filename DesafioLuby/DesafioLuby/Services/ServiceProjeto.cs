using Canducci.Pagination;
using Dapper;
using DesafioLuby.Controllers;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Models
{
    public class ServiceProjeto : ServiceBase
    {
        public async Task<PaginatedRest<ProjetoModel>> SelectAsync(int? IdProjeto, int? page)
        {

            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var list = con.Query<ProjetoModel>("SELECT * FROM Projeto" + (IdProjeto.HasValue && IdProjeto.Value > 0 ? " WHERE IdProjeto = " + IdProjeto.Value : ""));

                    var result = await list.OrderBy(c => c.IdProjeto).ToPaginatedRestAsync(page.Value, 10);

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

        public async Task<bool> InsertAsync(ProjetoModel Projeto)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "SET DATEFORMAT DMY INSERT INTO Projeto Values (@Nome, @DescricaoProjeto, @Date)";


                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Nome", Projeto.NomeProjeto);
                        command.Parameters.AddWithValue("@DescricaoProjeto", Projeto.DescricaoProjeto);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);

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


        public async Task<bool> UpdateAsync(ProjetoModel Projeto)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "Update Projeto Set NomeProjeto = @Nome, DescricaoProjeto = @DescricaoProjeto WHERE IdProjeto = @IdProjeto";


                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Nome", Projeto.NomeProjeto);
                        command.Parameters.AddWithValue("@DescricaoProjeto", Projeto.DescricaoProjeto);
                        command.Parameters.AddWithValue("@IdProjeto", Projeto.IdProjeto);

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
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int IdProjeto)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Projeto WHERE IdProjeto = @IdProjeto ";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@IdProjeto", IdProjeto);
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
