using System.Data.Common;

namespace Interfaces.DTOs
{
    public class DTOUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Role_ID { get; set; }
        public string Passcode { get; set; }


    }
}
