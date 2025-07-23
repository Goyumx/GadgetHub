namespace GadgetHub.API.Models
{
    public class Distributor
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., TechWorld, ElectroCom, Gadget Central
        public string Username { get; set; }
        public string Password { get; set; } // Later, consider hashing
    }
}
