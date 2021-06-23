using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.EntityFrameworkCore;
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

        // Delete Note Using Note ID
        public bool DeleteNote(int noteId)
        {
            try
            {
                if (_userDBContext.Notes.Any(n => n.NotesId == noteId))
                {
                    var note = _userDBContext.Notes.Find(noteId);
                    if (note.Trash)
                    {
                        _userDBContext.Entry(note).State = EntityState.Deleted;
                    }
                    else
                    {
                        note.Trash = true;
                        note.Pin = false;
                      
                    }
                    _userDBContext.SaveChanges();
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Note
        public UserNoteResponseData UpdateNote(int userID, int noteID, UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                UserNoteResponseData userNoteResponseData = null;
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                userData.Title = updateNoteRequest.Title;
                userData.Description = updateNoteRequest.Description;
               

                userNoteResponseData = new UserNoteResponseData()
                {
                    NoteId = userData.NotesId,
                    Title = userData.Title,
                    Description = userData.Description,
                    Body = userData.Body,
                    Color = userData.Color,
                    Image = userData.Image,
                    Pin = userData.Pin,
                    Archived = userData.Archived,
                    Trash = userData.Trash,
                    ModifiedDate = userData.ModifiedDate,
                  // Reminder = userData.Reminder

                };
                _userDBContext.SaveChanges();
                return userNoteResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
