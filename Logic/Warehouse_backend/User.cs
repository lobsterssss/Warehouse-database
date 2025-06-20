using System.Text;
using static WarehouseBLL.Role;

namespace WarehouseBLL
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public Roles? Role { get; set; }

    }
}
