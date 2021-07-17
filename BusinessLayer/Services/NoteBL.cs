using BusinessLayer.Interfaces;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        // Add Notes
        public UserNoteResponseData CreateNote(int UserID, AddNote userNotes)
        {
            UserNoteResponseData userNoteResponseData = noteRL.CreateNote(UserID, userNotes);
            return userNoteResponseData;
        }


        // Get Notes using UserId

        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                return this.noteRL.GetAllNotes(UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        // Delete Note Using NotesId
        public bool DeleteNote(int NotesId)
        {
            try
            {
                bool result = noteRL.DeleteNote(NotesId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update Note
        public UserNoteResponseData UpdateNote(UpdateNoteRequest updateNoteRequest)
        {
            UserNoteResponseData userNoteResponseData = noteRL.UpdateNote( updateNoteRequest);
            return userNoteResponseData;
        }

        // // Update Title
        public bool UpdateTitle(int userId, int noteID, AddTitle title)
        {
            bool responseData = noteRL.UpdateTitle(userId, noteID, title);
            return responseData;
        }

        // Update Body
        public bool UpdateBody(int userId, int noteId, AddBody addBody)
        {
            bool responseData = noteRL.UpdateBody(userId, noteId, addBody);
            return responseData;
        }

        // Update Reminder
        public bool UpdateReminder(int userID, int noteID, ReminderRequest reminder)
        {
            bool responseData = noteRL.UpdateReminder(userID, noteID, reminder);
            return responseData;
        }


        // Update Color
        public bool UpdateColor(int userID, int noteID, ColorRequest color)
        {
            bool responseData = noteRL.UpdateColor(userID, noteID, color);
            return responseData;
        }

        // Update Image
        public bool UpdateImage(int userID, int noteID, ImageRequest image)
        {
            bool data = noteRL.UpdateImage(userID, noteID, image);
            return data;
        }
        // Update Archived
        public void UpdateArchived(AddArchived UpdateArchived)
        {
            try
            {
                this.noteRL.UpdateArchived(UpdateArchived);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Trash
        public void UpdateTrash(int noteId, bool Trash)
        {
            try
            {
                this.noteRL.UpdateTrash(noteId, Trash);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        // Update Pin
        public void UpdatePin(int noteId, bool UpdatePin)
        {
            try
            {
                this.noteRL.UpdatePin(noteId, UpdatePin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Get Trash Note
        public List<UserNoteResponseData> GetTrashedNotes(int userID)
        {
            List<UserNoteResponseData> userTrashedData = noteRL.GetTrashedNotes(userID);
            return userTrashedData;
        }

        // Get Archieve Notes
        public List<UserNoteResponseData> GetArchievedNotes(int userID)
        {
            List<UserNoteResponseData> userArchievedData = noteRL.GetArchievedNotes(userID);
            return userArchievedData;
        }

        // Get Pin Notes
        public List<UserNoteResponseData> GetPinnedNotes(int userID)
        {
            List<UserNoteResponseData> userNoteResponseData = noteRL.GetPinnedNotes(userID);
            return userNoteResponseData;
        }
    }
}
