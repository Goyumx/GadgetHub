using System;

namespace GadgetHub.Web.Models
{
    [Serializable]
    public class AuthResponseDto
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
    }
}
