using Microsoft.Extensions.Configuration;

namespace ToDoMvc.Models
{
    public class ExternalAuthentication
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public static ExternalAuthentication GetAuthentication(IConfiguration Configuration, string external)
        {
            return new ExternalAuthentication
            {
                AppId = Configuration[$"Authentication:{external}:AppId"],
                AppSecret = Configuration[$"Authentication:{external}:AppSecret"]
            };
        }
    }
}
