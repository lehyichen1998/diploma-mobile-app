using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAssignment.Models
{
    public class NoteModal
    {
        [PrimaryKey,AutoIncrement]
        public int NoteModalId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        [ManyToOne]
        public Student student { get; set; }
        [ForeignKey(typeof(Student))]
        public int StudentId { get; set; }
    }
}
