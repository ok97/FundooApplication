﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        bool SampleApi(Users newUser);

        Users AddUser(Users user);
    }
}