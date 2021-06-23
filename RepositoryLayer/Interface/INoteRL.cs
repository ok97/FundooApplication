using CommonLayer.DatabaseModel;
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
        void AddNote(AddNote note);

        // Get Notes using UserId
        List<Note> GetAllNotes(int UserId);

        // Delete Notes using NotesId
        bool DeleteNote(int NotesId);

        // Update Note

        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest);

    }
}
