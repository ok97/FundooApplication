using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
  public class NoteRL : INoteRL
    {
        private readonly UserContext _userDBContext;
        public NoteRL(UserContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        // Add Notes
        public void AddNote(AddNote note)
        {
            try
            {
                var user = _userDBContext.Users.FirstOrDefault(e => e.UserId == note.UserId);
                if (user != null)
                {
                    Note Addnote = new Note();
                    Addnote.UserId = note.UserId;
                    Addnote.Title = note.Title;
                    Addnote.Description = note.Description;
                    Addnote.Body = note.Body;
                    Addnote.Reminder = note.Reminder;
                    Addnote.Color = note.Color;
                    Addnote.Image = note.Image;
                    Addnote.Archived = note.Archived;
                    Addnote.Trash = note.Trash;
                    Addnote.Pin = note.Pin;
                    Addnote.CreatedDate = note.CreatedDate;
                    Addnote.ModifiedDate = note.ModifiedDate;
                    Addnote.User = user;
                    
                    _userDBContext.Notes.Add(Addnote);
                    _userDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("UserId Not Exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        // Get Notes Using ID

        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                var list = _userDBContext.Notes.Where(e => e.UserId == UserId).ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
