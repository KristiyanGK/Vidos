namespace Vidos.Web.Configurations.PasswordOptions
{
    public class DevelopmentPasswordOptions : Microsoft.AspNetCore.Identity.PasswordOptions
    {
        public DevelopmentPasswordOptions()
        {
            this.RequireDigit = false;
            this.RequiredLength = 3;
            this.RequireLowercase = false;
            this.RequireNonAlphanumeric = false;
            this.RequireUppercase = false;
        }
    }
}
