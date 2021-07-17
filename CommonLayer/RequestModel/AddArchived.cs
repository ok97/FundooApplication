using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
   public class AddArchived
    {
        public int NotesId { get; set; }
        public bool Archived { get; set; }
    }
}
