using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ILabelRL
    {

        LabelResponseData CreateLabel(int userID, LabelRequest label);
    }
}
