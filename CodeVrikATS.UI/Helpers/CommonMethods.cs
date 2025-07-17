using CodeVrikATS.Entity.Models;

namespace CodeVrikATS.UI.Helpers
{
    public static class CommonMethods
    {
        public static bool HasRight(List<RoleRightEntity> rights, string menuName, string permissionType)
        {
            var item = rights.FirstOrDefault(r => r.MenuName == menuName);

            if (item == null) return false;

            return permissionType switch
            {
                "CanView" => item.CanView == 1,
                "CanAdd" => item.CanAdd == 1,
                "CanEdit" => item.CanEdit == 1,
                "CanDelete" => item.CanDelete == 1,
                _ => false
            };
        }
    }
}
