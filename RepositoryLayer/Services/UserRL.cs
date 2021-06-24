using CommonLayer;
using CommonLayer.RequestModel;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RepositoryLayer.Interface;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
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
        public void RegisterUser(Users user)
        {
            try
            {
                string encryptedPassword = StringCipher.Encrypt(user.Password);
                user.Password = encryptedPassword;
                _userDBContext.Users.Add(user);
                _userDBContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
            try
            {
                string encryptedPassword = StringCipher.Encrypt(password);
                var result = _userDBContext.Users.FirstOrDefault(u => u.Email == email && u.Password == encryptedPassword);
                if (result == null)
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("Hello This Token Is Genereted");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email",email),
                    new Claim("UserId",result.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Forgot Password
        public bool ForgotPassword(string email)
        {
            try
            {
                var result = _userDBContext.Users.FirstOrDefault(u => u.Email == email);
                if (result == null)
                {
                    return false;
                }
                MessageQueue queue;

                // Message Queue 
                if (MessageQueue.Exists(@".\Private$\FundooApplicationQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooApplicationQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooApplicationQueue");
                }

                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = email;
                MyMessage.Label = "Forget Password Email Fundoo Application";
                queue.Send(MyMessage);
                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(msg.Body.ToString(), GenerateToken(msg.Body.ToString()));
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                queue.BeginReceive();
                queue.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
           
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
           

        }

        // Generate Token
        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email",email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Change Password

        public void ChangePassword(string email, string newPassword)
        {
            try
            {
                var result = _userDBContext.Users.FirstOrDefault(u => u.Email == email);
                if (result != null)
                {
                    string encryptedPassword = StringCipher.Encrypt(newPassword);
                    result.Password = encryptedPassword;
                    _userDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

    }


}
