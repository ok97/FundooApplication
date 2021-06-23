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
        public void AddNote(AddNote note)
        {
            try
            {
                this.noteRL.AddNote(note);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

       
    }
}
