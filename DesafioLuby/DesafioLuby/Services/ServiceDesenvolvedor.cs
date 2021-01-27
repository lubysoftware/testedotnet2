using Canducci.Pagination;
using Dapper;
using DesafioLuby.Controllers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioLuby.Models
{
    public class ServiceDesenvolvedor : ServiceBase
    {
        public async Task<PaginatedRest<DesenvolvedorModel>> SelectAsync(string cpf, int? page)
        {

            return await SelectDev(cpf).OrderBy(c => c.IdDesenvolvedor).ToPaginatedRestAsync(page.Value, 10);

        }

        public List<DesenvolvedorModel> SelectDev(string cpf) {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var list = con.Query<DesenvolvedorModel>("SELECT * FROM Desenvolvedor" + (String.IsNullOrEmpty(cpf) ? " WHERE Cpf = " + cpf : ""));

                    return list.ToList();
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


        public bool IsCpfValid(DesenvolvedorModel desenvolvedor) {

            var client = new RestClient($@"https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9?cpf={desenvolvedor.Cpf}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var valido = response.Content;

            return true;
        }

        public async Task<bool> InsertAsync(DesenvolvedorModel desenvolvedor)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {

                    if (IsCpfValid(desenvolvedor)) {  
                  

                    con.Open();
                    var query = "SET DATEFORMAT DMY INSERT INTO Desenvolvedor Values (@Nome, @Cpf, @Date)";


                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Nome", desenvolvedor.NomeDesenvolvedor);
                        command.Parameters.AddWithValue("@Cpf", desenvolvedor.Cpf);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);

                        int result = await command.ExecuteNonQueryAsync();
                    }
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


        public async Task<bool> UpdateAsync(DesenvolvedorModel desenvolvedor)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "Update Desenvolvedor Set NomeDesenvolvedor = @Nome, Cpf = @Cpf WHERE IdDesenvolvedor = @IdDesenvolvedor";


                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Nome", desenvolvedor.NomeDesenvolvedor);
                        command.Parameters.AddWithValue("@Cpf", desenvolvedor.Cpf);
                        command.Parameters.AddWithValue("@IdDesenvolvedor", desenvolvedor.IdDesenvolvedor);

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

        public async Task<bool> DeleteAsync(int IdDesenvolvedor)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Desenvolvedor WHERE IdDesenvolvedor = @IdDesenvolvedor ";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@IdDesenvolvedor", IdDesenvolvedor);
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
