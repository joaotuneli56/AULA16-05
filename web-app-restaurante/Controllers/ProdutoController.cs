using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data;
using web_app_restaurante.Entidades;

namespace web_app_restaurante.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly string? _connectionString;
        //ctor - atalho para criar o construtor
        public ProdutoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection OpenConnection()
        {
            IDbConnection dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            using IDbConnection dbConnection = OpenConnection();
            string sql = "select id, nome, descricao, ImageUrl from Produto;";
            var result = await dbConnection.QueryAsync<Produto>(sql);
            return Ok(result);
        }



    }
}
