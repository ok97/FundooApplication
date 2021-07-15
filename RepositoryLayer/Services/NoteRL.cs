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
        public UserNoteResponseData CreateNote(int UserID, AddNote note)
        {
            try
            {
                Note userNote = new Note()
                {
                    UserId = UserID,
                    Title = note.Title,
                    Description = note.Description,
                    Body = note.Body,
                    Reminder = note.Reminder,
                    Color = note.Color,
                    Image = note.Image,
                    Pin = note.Pin,
                    Archived = note.Archived,
                    Trash = note.Trash,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                _userDBContext.Notes.Add(userNote);
                _userDBContext.SaveChanges();

                UserNoteResponseData noteResponseData = new UserNoteResponseData()
                {
                    NoteId = userNote.NotesId,
                    Title = userNote.Title,
                    Description = userNote.Description,
                    Body = userNote.Body,
                    Reminder = userNote.Reminder,
                    Color = userNote.Color,
                    Image = userNote.Image,
                    Pin = userNote.Pin,
                    Archived = userNote.Archived,
                    Trash = userNote.Trash,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                };
                return noteResponseData;
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
        public UserNoteResponseData UpdateNote( UpdateNoteRequest updateNoteRequest)
        {
            try
            {
                UserNoteResponseData userNoteResponseData = null;
                var userData = _userDBContext.Notes.FirstOrDefault(user =>  user.NotesId == updateNoteRequest. NotesId);
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

                };
                _userDBContext.SaveChanges();
                return userNoteResponseData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Update Reminder
        public bool UpdateTitle(int userId, int noteID, AddTitle title)
        {
            try
            {
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userId && user.NotesId == noteID);
                if (userData != null)
                {
                    userData.Title = title.Title;
                    _userDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // Update Body
        public bool UpdateBody(int userId, int noteId, AddBody addBody)
        {
            try
            {
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userId && user.NotesId == noteId);
                if (userData != null)
                {
                    userData.Body = addBody.Body;
                    _userDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Reminder
        public bool UpdateReminder(int userID, int noteID, ReminderRequest reminder)
        {
            try
            {
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                if (userData != null)
                {
                    userData.Reminder = reminder.Reminder;
                    _userDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Color
        public bool UpdateColor(int userID, int noteID, ColorRequest color)
        {
            try
            {
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                if (userData != null)
                {
                    userData.Color = color.Color;
                    _userDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Image
        public bool UpdateImage(int userID, int noteID, ImageRequest image)
        {
            try
            {
                var userData = _userDBContext.Notes.FirstOrDefault(user => user.UserId == userID && user.NotesId == noteID);
                userData.Image = image.Image;
                _userDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Update Archived
        public void UpdateArchived(int noteId, bool Archived)
        {
            try
            {
                var result = _userDBContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Archived = Archived;
                    _userDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update Trash
        public void UpdateTrash(int noteId, bool Trash)
        {
            try
            {
                var result = _userDBContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Trash = Trash;
                    _userDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        // Update Pin
        public void UpdatePin(int noteId, bool Pin)
        {
            try
            {
                var result = _userDBContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Pin = Pin;
                    _userDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Get Trash Notes
        public List<UserNoteResponseData> GetTrashedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _userDBContext.Notes.
                    Where(user => user.UserId == userID && user.Trash == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Description = user.Description,
                        Body = user.Body,
                        Reminder = user.Reminder,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                        
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get Archive Notes
        public List<UserNoteResponseData> GetArchievedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _userDBContext.Notes.
                    Where(user => user.UserId == userID && user.Archived == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Description = user.Description,
                        Body = user.Body,
                        Reminder = user.Reminder,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Get Pin Notes
        public List<UserNoteResponseData> GetPinnedNotes(int userID)
        {
            try
            {
                List<UserNoteResponseData> userNoteLists = _userDBContext.Notes.
                    Where(user => user.UserId == userID && user.Pin == true).
                    Select(user => new UserNoteResponseData
                    {
                        NoteId = user.NotesId,
                        Title = user.Title,
                        Description = user.Description,
                        Body = user.Body,
                        Reminder = user.Reminder,
                        Color = user.Color,
                        Image = user.Image,
                        Pin = user.Pin,
                        Archived = user.Archived,
                        Trash = user.Trash,
                        CreatedDate = user.CreatedDate,
                        ModifiedDate = user.ModifiedDate
                    }).
                    ToList();

                if (userNoteLists == null)
                {
                    return null;
                }
                return userNoteLists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
