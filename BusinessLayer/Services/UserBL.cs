using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        //post request Add
        public bool SampleApi(User newUser)
        {
            try
            {
                newUser.FirstName = newUser.FirstName + "Update From BL";
                return this.userRL.SampleApi(newUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
