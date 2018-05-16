using MySql.Data.MySqlClient;
using System.Data;

namespace Zegeltjes_DAL
{
    public class BagCommand
    {
        private MySqlConnection _MySQLconn = new MySqlConnection("Server= localhost;Database= bag;Uid=root;Pwd= ;");
        private DataTable dtResult = new DataTable();

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

        private void CloseConn()
        {
            _MySQLconn.Close();
        }

        public string HaalStraatNaamOp(string postcode, string huisnummer)
        {
            try
            {
                OpenConn();
                string query = $"SELECT `Aanduiding` FROM nl_bag_adresdata WHERE `Postcode` = '{postcode}' && `Huisnummer` = '{huisnummer}'";
                MySqlCommand msCommand = new MySqlCommand(query, _MySQLconn);
                MySqlDataReader mscReader = msCommand.ExecuteReader();
                dtResult.Load(mscReader);
                CloseConn();
                if (dtResult.Rows.Count != 0)
                {
                    return dtResult.Rows[0][0].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public string HaalPlaatsNaamOpPostcode(string postcode)
        {
            OpenConn();
            string query = $"SELECT DISTINCT `Woonplaats` FROM nl_bag_adresdata WHERE `Postcode` = '{postcode}'";
            MySqlCommand msCommand = new MySqlCommand(query, _MySQLconn);
            MySqlDataReader mscReader = msCommand.ExecuteReader();
            dtResult.Load(mscReader);
            CloseConn();
            if (dtResult.Rows.Count != 0)
            {
                return dtResult.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }
        public string HaalPlaatsNaamOpText(string text)
        {
            OpenConn();
            string query = $"SELECT DISTINCT `Woonplaats` FROM nl_bag_adresdata WHERE `Woonplaats` = '{text}'";
            MySqlCommand msCommand = new MySqlCommand(query, _MySQLconn);
            MySqlDataReader mscReader = msCommand.ExecuteReader();
            dtResult.Load(mscReader);
            CloseConn();
            if (dtResult.Rows.Count != 0)
            {
                return dtResult.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
        }

    }
}
