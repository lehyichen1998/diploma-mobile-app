using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAssignment.Models
{
    public class Student
    {
        [PrimaryKey,AutoIncrement]
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string phoneNo { get; set; }
        public string Programme { get; set; }
        public DateTime DateEnroll { get; set; }
        public bool isActivate { get; set; }
        public string ActivateCode { get; set; }
        public string photo { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<NoteModal> NoteM { get; set; }
    }
}
