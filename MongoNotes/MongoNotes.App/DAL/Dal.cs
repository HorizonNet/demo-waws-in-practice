using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;
using MongoNotes.App.Models;

namespace MongoNotes.App.DAL
{
    public class Dal
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_MONGOLAB_URI");

        private const string DbName = "mvpmentor-demo";

        private const string CollectionName = "Notes";

        public List<Note> GetAllNotes()
        {
            try
            {
                MongoCollection<Note> collection = GetNotesCollection();

                return collection.FindAll().ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<Note>();
            }
        }

        public void CreateNote(Note note)
        {
            MongoCollection<Note> collection = GetNotesCollection();

            try
            {
                collection.Insert(note, WriteConcern.Acknowledged);
            }
            catch (MongoCommandException)
            {
            }
        }

        private MongoCollection<Note> GetNotesCollection()
        {
            var client = new MongoClient(_connectionString);
            MongoDatabase database = client.GetServer().GetDatabase(DbName);
            MongoCollection<Note> notesCollection = database.GetCollection<Note>(CollectionName);

            return notesCollection;
        }
    }
}