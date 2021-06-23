using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class UserNoteResponseData
    {
        public int NoteId { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }
        public string Body { get; set; }

        public string Color { set; get; }

        public string Image { set; get; }

      
        public bool Pin { set; get; }

      
        public bool Archived { set; get; }

       
        public bool Trash { set; get; }
        public DateTime ModifiedDate { get; set; }

        public DateTime? Reminder { set; get; }
    }
}
