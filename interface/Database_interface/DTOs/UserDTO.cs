using System.Data.Common;

namespace InterfacesDal.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Role_ID { get; set; }
        public string Passcode { get; set; }


    }
}
