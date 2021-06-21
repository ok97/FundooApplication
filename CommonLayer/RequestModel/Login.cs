using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModels
{
    public class Login
    {
     
        public string Email { get; set; }

        
        public string Password { get; set; }
    }
}