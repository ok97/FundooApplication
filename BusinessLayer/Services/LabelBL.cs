using BusinessLayer.Interfaces;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {

        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        // Add Label
        public LabelResponseData CreateLabel(int userID, LabelRequest labelRequest)
        {
            LabelResponseData responseData = labelRL.CreateLabel(userID, labelRequest);
            return responseData;
        }
    }
}
