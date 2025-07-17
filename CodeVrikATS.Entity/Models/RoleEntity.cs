using System.ComponentModel.DataAnnotations;

namespace CodeVrikATS.Entity.Models
{
    public class RoleEntity
    {
        [Key]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string RoleName { get; set; }

        public int? createdBy { get; set; }

    }

    public class RoleRightEntity
    {
        public int RoleRightId { get; set; }
        public int? MenuId { get; set; }
        public string? MenuName { get; set; }
        public int? RoleId { get; set; }
        public int CanView { get; set; }
        public int CanAdd { get; set; }
        public int CanEdit { get; set; }
        public int CanDelete { get; set; }
        public List<RoleEntity>? RolesList { get; set; }
        public int? SelectedRole { get; set; }
        public string? RoleRightsJson { get; set; }
        public int? loggedEmpCode { get; set; }
    }

    public class RoleRightUpdateDto
    {
        public int RoleId { get; set; }
        public int ManuId { get; set; }

        public bool CanView { get; set; }

        public bool CanAdd { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }
        public string? RoleRightsJson { get; set; }
    }

    public class AuthRoleRightEntity
    {
        public string MenuName { get; set; }
        public int CanView { get; set; }
        public int CanAdd { get; set; }
        public int CanEdit { get; set; }
        public int CanDelete { get; set; }
    }
}
