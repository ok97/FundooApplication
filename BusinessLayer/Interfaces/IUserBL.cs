using Amazon.CognitoIdentityProvider.Model;
using CommonLayer;
using CommonLayer.RequestModel;
using CommonLayer.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        //bool SampleApi(Users newUser);

        void RegisterUser(Users user);

        // get data
        List<Users> GetUsersData();


        // User login
        string Login(string email, string password);


        // Forgot Password
        bool ForgotPassword(string email);

        // Change Password
        void ChangePassword(string email, string newPassword);
      
    }
}
