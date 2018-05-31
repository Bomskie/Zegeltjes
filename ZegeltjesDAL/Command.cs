using MySql.Data.MySqlClient;
using System.Data;

namespace Zegeltjes_DAL
{
    public abstract class Command<T>
    {
        protected MySqlConnection _MySQLconn = new MySqlConnection("Server= localhost;Database= zegeltjes;Uid=root;Pwd= ;");
        protected DataTable dtResult = new DataTable();

        private void OpenConn()
        {
            try
            {
                _MySQLconn.Open();
            }
            catch (MySqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public abstract T Execute();

        protected void DraaiQuery(string query)
        {
            OpenConn();
            MySqlCommand msSelect = new MySqlCommand(query, _MySQLconn);
            MySqlDataReader mscReader = msSelect.ExecuteReader();
            dtResult.Load(mscReader);
            CloseConn();
        }

        private void CloseConn()
        {
            _MySQLconn.Close();
        }
    }
}
