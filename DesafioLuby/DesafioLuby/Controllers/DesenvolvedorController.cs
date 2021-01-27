using Dapper;
using DesafioLuby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Canducci.Pagination;
namespace DesafioLuby.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesenvolvedorController : DevControllerBase
    {
        [HttpGet("page/{page?}")]
        public async Task<PaginatedRest<DesenvolvedorModel>> GetAsync(int? IdDesenvolvedor, int? page)
        {
            page ??= 1;
            if (page <= 0) page = 1;

            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var list = con.Query<DesenvolvedorModel>("SELECT * FROM Desenvolvedor" + (IdDesenvolvedor.HasValue && IdDesenvolvedor.Value > 0 ? " WHERE IdDesenvolvedor = " + IdDesenvolvedor.Value : ""));

                    var result = await list
              .OrderBy(c => c.IdDesenvolvedor)
              .ToPaginatedRestAsync(page.Value, 10);

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


        [HttpPut]
        public string Put(DesenvolvedorModel desenvolvedor)
        {

            using (var con = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    con.Open();
                    var query = "SET DATEFORMAT DMY INSERT INTO Desenvolvedor Values (@Nome, @Idade, @Date)";


                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Nome", desenvolvedor.NomeDesenvolvedor);
                        command.Parameters.AddWithValue("@Idade", desenvolvedor.Idade);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);

                        int result = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return "Registro salvo com sucesso!";
            }
        }


        [HttpPost]
        public string Post(DesenvolvedorModel desenvolvedor)
        {
            if (desenvolvedor.IdDesenvolvedor > 0)
            {

                using (var con = new SqlConnection(this.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        var query = "Update Desenvolvedor Set NomeDesenvolvedor = @Nome, Idade = @Idade WHERE IdDesenvolvedor = @IdDesenvolvedor";


                        using (SqlCommand command = new SqlCommand(query, con))
                        {

                            command.Parameters.AddWithValue("@Nome", desenvolvedor.NomeDesenvolvedor);
                            command.Parameters.AddWithValue("@Idade", desenvolvedor.Idade);
                            command.Parameters.AddWithValue("@IdDesenvolvedor", desenvolvedor.IdDesenvolvedor);

                            int result = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                return "Registro alterado com sucesso!";

            }
            else 
            return "O Id tem que ser maior que 0";

        }

        [HttpDelete]
        public string Delete(int IdDesenvolvedor)
        {
            if (IdDesenvolvedor > 0)
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
                            int result = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return "Registro deletado com sucesso";
                }

            }
            else
                return "O Id tem que ser maior que 0";



        }
    }

}

