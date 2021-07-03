using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
   public interface ILabelBL
    {
        // Add Label
        LabelResponseData CreateLabel(int userID, LabelRequest label);

    }
}
