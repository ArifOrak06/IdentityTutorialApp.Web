namespace IdentityAppTutorial.Core.Models.AdminModels.RoleModels
{
    public class AssignRoleToAppUserViewModel
    {
        public string Id { get; set; } = null!; // Role Id
        public string Name { get; set; } = null!; // Role Adı 
        public bool Exist { get; set; } // Kullanıcının herhangi bir rolü var mı yok mu ? 

    }
}
 