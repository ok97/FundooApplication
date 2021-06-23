using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        // Add Notes
        void AddNote(AddNote note);

        // 
        List<Note> GetAllNotes(int UserId);

    }
}
