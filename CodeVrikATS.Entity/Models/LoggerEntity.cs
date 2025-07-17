using System.Globalization;

namespace CodeVrikATS.Entity.Models
{
    public class LoggerEntity
    {
        public string? UserId { get; set; }
        public int UserCode { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; } 
        public int? IsActive { get; set; }
        public string? UserToken { get; set; }        
        public string? BirthdayEmployee { get; set; }
        public string? RoleRights { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }

    public class RoleRightCheck
    {
        public string MenuName { get; set; }
        public int CanView { get; set; }
        public int CanAdd { get; set; }
        public int CanEdit { get; set; }
        public int CanDelete { get; set; }
    }
}

