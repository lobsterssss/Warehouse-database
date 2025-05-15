using MySqlConnector;
using System.Data.Common;

namespace DTO_Warehouse
{
    public class DTOUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Role_ID { get; set; }
        public string Passcode { get; set; }


        public DTOUser(MySqlDataReader reader) 
        {
            ID = reader.GetInt32("ID");
            Name = reader.GetString("Name");
            Role_ID = reader.IsDBNull(reader.GetOrdinal("Role_ID")) ? (int?)null : reader.GetInt32("Role_ID");
            Passcode = reader.GetString("Passcode");
        }
    }
}
