using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace MyApi.Models.DataAccess
{
    public class BaseDao
    {
        protected SqlConnection _con { get; set; }

        protected IConfiguration _configuration;

        public BaseDao(IConfiguration configuration)
        {
            _configuration = configuration;
            _con = new SqlConnection(_configuration.GetValue<string>("ConnectionStrings:Default"));
        }
    }
}
