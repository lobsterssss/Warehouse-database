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

    }
}
