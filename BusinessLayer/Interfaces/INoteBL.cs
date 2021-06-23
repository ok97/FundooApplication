using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        // Add Notes
        void AddNote(AddNote note);

        // Get Notes using UserId
        List<Note> GetAllNotes(int UserId);

        // Delete Notes using NotesId
        bool DeleteNote(int NotesId);

        // Update Note
        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);


        // Add Reminder
        public bool AddReminder(int userID, int noteID, ReminderRequest reminder);

        // Add Color
        public bool AddColor(int userID, int noteID, ColorRequest color);

        // Add Image
        public bool AddImage(int userID, int noteID, ImageRequest image);
    }
}
