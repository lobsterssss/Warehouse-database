using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class UserCollection
    {
        private readonly IUserRepository UserDal;

        public UserCollection(IUserRepository userDal)
        {
            UserDal = userDal;
        }

        public async IAsyncEnumerable<User> GetAllUsers()
        {
            IAsyncEnumerable<UserDTO> users = UserDal.GetAll();

            await foreach (UserDTO user in users)
            {
                yield return new User()
                {
                    ID = user.ID,
                    Name = user.Name,
                    //Role = user.Role_ID

                };
            }
        }
    }
}
