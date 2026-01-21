using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CHIETAMIS.People
{
    [Table("tbl_Person")]
    public class Person : Entity<int>
    {
        public string Title { get; set; }
        public Int16 Designation { get; set; }
        public string Firstname { get; set; }
        public string Middlenames { get; set; }
        public string Lastname { get; set; }
        public Int16 Idtype { get; set; }
        public string Saidnumber { get; set; }
        public string Otheriddetails { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Equity { get; set; }
        public string Language { get; set; }
        public string Citizenship { get; set; }
        public DateTime Datecreated { get; set; }
        public int Userid {get; set;}
    }
}
