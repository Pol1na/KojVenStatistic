//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KojVenStatistic.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Appeal = new HashSet<Appeal>();
            this.ReportOfTheDay = new HashSet<ReportOfTheDay>();
            this.ReportOfTheMonth = new HashSet<ReportOfTheMonth>();
            this.ReportOfTheWeek = new HashSet<ReportOfTheWeek>();
            this.ReportOfTheYear = new HashSet<ReportOfTheYear>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PostId { get; set; }
        public int AccessLevelId { get; set; }
        public string Experience { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string NumberPassport { get; set; }
        public int CabinetId { get; set; }
    
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual ICollection<Appeal> Appeal { get; set; }
        public virtual Cabinet Cabinet { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<ReportOfTheDay> ReportOfTheDay { get; set; }
        public virtual ICollection<ReportOfTheMonth> ReportOfTheMonth { get; set; }
        public virtual ICollection<ReportOfTheWeek> ReportOfTheWeek { get; set; }
        public virtual ICollection<ReportOfTheYear> ReportOfTheYear { get; set; }
    }
}
