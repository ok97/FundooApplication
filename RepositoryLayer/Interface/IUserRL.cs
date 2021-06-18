using CommonLayer;
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
    }
}
