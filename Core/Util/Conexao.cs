using Npgsql;

namespace Core.Util
{
    public static class Conexao
    {
        private static NpgsqlConnection _Connection;

        public static NpgsqlConnection Connection
        {
            get
            {
                if (_Connection == null)
                    //_Connection = new NpgsqlConnection("Host=localhost;Database=EComLivro_LES_2020;Username=postgres;Password=145964");
                    _Connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=145964;Database=EComLivro_LES_2020;");
                return _Connection;
            }
            set { _Connection = value; }
        }

    }
}
