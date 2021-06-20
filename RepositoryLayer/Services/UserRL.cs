using Amazon.CognitoIdentityProvider.Model;
using CommonLayer;
using CommonLayer.RequestModels;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        //Add User
        private readonly UserContext _userDBContext;
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
        
        //get data
        public List<Users> GetUsersData()
        {
            try
            {
                var usersList = new List<Users>();
                Users responseData = null;
                var users = _userDBContext.Users;
                foreach(Users user in users)
                {
                    responseData = new Users()
                    {
                        UserId = user.UserId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password
                    };
                    usersList.Add(responseData);
                }
                return usersList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // User Login

        public string Login(string email, string password)
        {
            var result = _userDBContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (result == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("Hello This Token Is Genereted ");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }


}
