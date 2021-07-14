using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class UpdateNoteRequest
    {
        public int NotesId { get; set; }
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

