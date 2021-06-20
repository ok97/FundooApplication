using Amazon.CognitoIdentityProvider.Model;
using CommonLayer;
using CommonLayer.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        bool SampleApi(Users newUser);

        Users AddUser(Users user);

        // get data
         List<Users> GetUsersData();


        // User login
        string Login(string email, string password);
        

        // Forgot Password
        bool ForgotPassword(string email);

        


    }
}
