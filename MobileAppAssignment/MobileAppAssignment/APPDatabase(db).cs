using MobileAppAssignment.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MobileAppAssignment
{
    class APPDatabase_db_
    {
        private static string dbApp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "sqldb.db3");

        public static void CreateTable()
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                db.CreateTable<Student>();
                db.CreateTable<NoteModal>();
            }
        }
        public static void RemoveTable()
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                db.DropTable<Student>();
                db.DropTable<NoteModal>();
            }
        }
       
        public static bool ComfirmEmail(string email)
        {

            using (var db = new SQLiteConnection(dbApp))
            {
                //db.Find<Student>(email);
                var result = (from s in db.Table<Student>()
                              where s.email == email
                              select s).SingleOrDefault();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static void AddStudent(Student student)
        {
            using (var db = new SQLiteConnection(dbApp))
            {

                db.Insert(student);
            }
        }
        public static void ReadImage()
        {

        }
        public static Student LoginChecking(string email, string password)
        {
            Student result;
            using (var db = new SQLiteConnection(dbApp))
            {

                result = (from s in db.Table<Student>()
                          where s.email == email
                          select s).SingleOrDefault();
                if(result!=null)
                {
                    bool checkpass = Register.scryptEncoder.Compare(password, result.password);
                    if (checkpass == false)
                    {
                        result = null;
                    }
                }
               

            }
            return result;
        }
        public static Student GetCode(string code, Student student)
        {
            Student result;
            using (var db = new SQLiteConnection(dbApp))
            {
                //LinQ
                 result = (from s in db.Table<Student>()
                                where s.ActivateCode == code
                                select s).SingleOrDefault();
                if (result != null)
                {
                    student.isActivate = true;
                    db.Update(student);
                }
            }
            return result;
        }
        public static Student CheckEmail(string email)
        {
            Student result;
            using (var db = new SQLiteConnection(dbApp))
            {
                result = (from s in db.Table<Student>()
                          where s.email == email
                          select s).SingleOrDefault();
               
            }
            return result;
        }
        public static Student UpdateDetail(Student student)
        {
            Student result;
            using (var db=new SQLiteConnection(dbApp))
            {
                result = (from s in db.Table<Student>()
                          where s.studentId.Equals(student.studentId)
                         select s).SingleOrDefault();
                if(result!=null)
                {
                    db.Update(student);
                    return result;
                }
            }
            return result;
        }
        public static Student UpdateNote(NoteModal noteModal)
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                var result = (from s in db.Table<Student>()
                          where s.studentId.Equals(noteModal.StudentId)
                          select s).SingleOrDefault();
                if (result != null)
                {
                    db.Update(noteModal);
                    return result;
                }
            }
            return null;
        }
        public static Student SaveNote(NoteModal noteModal)
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                var result = (from s in db.Table<Student>()
                              where s.studentId == noteModal.StudentId
                              select s).SingleOrDefault();
                if (result != null)
                {
                    db.Insert(noteModal);
                    return result;
                }
            }
            return null;
        }
        public static Student getStudentData(NoteModal noteModal)
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                var _noteModal = (from s in db.Table<Student>()
                               where s.studentId==noteModal.StudentId
                                  select s).SingleOrDefault();
                if(_noteModal!=null)
                {
                    return _noteModal;
                }
            }
            return null;
        }
       
        
        public static ObservableCollection<NoteModal> readNote(Student student)
        {
            ObservableCollection<NoteModal> noteModal;
            using (var db=new SQLiteConnection(dbApp))
            {
                var _noteModal=(from s in db.Table<NoteModal>()
                                where s.StudentId==student.studentId
                           select s).ToList();
                noteModal = new ObservableCollection<NoteModal>(_noteModal);
            }
            return noteModal;
        }
        public static void DeleteStudent(NoteModal noteModal)
        {
            using (var db = new SQLiteConnection(dbApp))
            {
                db.Delete(noteModal);
            }
        }
    }
}
