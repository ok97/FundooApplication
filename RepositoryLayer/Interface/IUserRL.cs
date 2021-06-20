using Amazon.CognitoIdentityProvider.Model;
using CommonLayer;
using CommonLayer.RequestModels;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        bool SampleApi(Users newUser);
        Users AddUser(Users user);

        // get data
         List<Users> GetUsersData();

        // User login
        string Login(string email, string password);


    }
}
