﻿using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        // Add Notes
        UserNoteResponseData CreateNote(int UserID, AddNote userNotes);
        // Get Notes using UserId
        List<Note> GetAllNotes(int UserId);

        // Delete Notes using NotesId
        bool DeleteNote(int NotesId);

        // Update Note

        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);


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

        // Put Note in Trash
        bool TrashNote(int userId, int noteID);

        //Put Archive Note
        public bool ArchievedUnarchievedNote(int userID, int noteID);
       
        //Put Pin Note
        public bool PinUnpinNote(int userID, int noteID);
    }
}
