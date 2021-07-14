using CommonLayer.DBModels;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
   public class LabelRL : ILabelRL
    {
        private readonly UserContext _userDBContext;

        public LabelRL(UserContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        // Add Label
        public LabelResponseData CreateLabel(int userID, LabelRequest labelRequest)
        {
            try
            {
                Label labelInfo = new Label()
                {
                    UserId = userID,                   
                    Name = labelRequest.Name
                   
                };
                _userDBContext.Labels.Add(labelInfo);
                _userDBContext.SaveChanges();

                LabelResponseData responseData = new LabelResponseData()
                {
                    LabelId = labelInfo.LabelId,
                    Name = labelInfo.Name
                };
                return responseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
