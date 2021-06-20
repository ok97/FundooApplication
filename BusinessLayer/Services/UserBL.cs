using Amazon.CognitoIdentityProvider.Model;
using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.RequestModels;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
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
        public bool SampleApi(Users newUser)
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

        public Users AddUser(Users user)
        {
            this.userRL.AddUser(user);
            return user;
        }

        //get data
       
        public List<Users> GetUsersData()
        {
            List<Users> usersList = userRL.GetUsersData();
            return usersList;
        }

        //Users login
        public string Login(string email, string password)
        {
            return this.userRL.Login(email, password);
        }

    }
}
