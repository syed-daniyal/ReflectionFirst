using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationDemo
{
    class Student
    {
        public int id {get;set;}
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string country { get; set; }

        public Student() {
            id = 1;
            username = "Daniyal";
            password = "fds";
            email = "fsdf";
            country = "Pakistan";
        }
    }

 
}
