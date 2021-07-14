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
        UserNoteResponseData CreateNote(int UserID, AddNote userNotes);

        // Get Notes using UserId
        List<Note> GetAllNotes(int UserId);

        // Delete Notes using NotesId
        bool DeleteNote(DeleteNote notesId);

        // Update Note
        public UserNoteResponseData UpdateNote(UpdateNoteRequest updateNoteRequest);


        // Update Title
        public bool UpdateTitle(int userId, int noteID, AddTitle title);


        // Update Body
        public bool UpdateBody(int userId, int noteId, AddBody addBody);
        // Update Reminder
        public bool UpdateReminder(int userID, int noteID, ReminderRequest reminder);

        // Update Color
        public bool UpdateColor(int userID, int noteID, ColorRequest color);

        // Update Image
        public bool UpdateImage(int userID, int noteID, ImageRequest image);


        // Update Archived
        public void UpdateArchived(int noteId, bool Archived);

        // Update Trash
        public void UpdateTrash(int noteId, bool Trash);

        // Update Pin
        public void UpdatePin(int noteId, bool UpdatePin);

        // Get trash Note
        List<UserNoteResponseData> GetTrashedNotes(int userID);

        //Get Archive Notes
        List<UserNoteResponseData> GetArchievedNotes(int userID);

        // Get Pin Notes
        public List<UserNoteResponseData> GetPinnedNotes(int userID);
    }
}
