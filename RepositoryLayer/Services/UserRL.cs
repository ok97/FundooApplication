using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        // DB
        IList<Users> users = new List<Users>();
        public bool SampleApi(Users newUser)
        {
            try
            {
                users.Add(newUser);
                if (users.Contains(newUser) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        UserContext _userDBContext;
        public UserRL(UserContext userDBContext)
        {
            _userDBContext = userDBContext;
        }
        public Users AddUser(Users newuser)
        {
            _userDBContext.Users.Add(newuser);
            _userDBContext.SaveChanges();
            return newuser;

        }
    }


}
