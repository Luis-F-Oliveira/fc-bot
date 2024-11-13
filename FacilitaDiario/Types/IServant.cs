namespace FacilitaDiario.Types
{
    public class IServant
    {
        public int Id { get; set; }
        public required string Enrollment { get; set; }
        public required string Contract { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public bool Active { get; set; }
    }
}
