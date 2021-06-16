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
        IList<User> users = new List<User>();
        public bool SampleApi(User newUser)
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
       
   }

   
}
