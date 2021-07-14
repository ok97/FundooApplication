using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    class NoteResponse
    {
        public int NotesId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archived { get; set; }
        public bool Trash { get; set; }
        public bool Pin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        
    }
}
