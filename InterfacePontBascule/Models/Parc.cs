namespace InterfacePontBascule.Models
{
    public class Parc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Observation { get; set; }

        public bool actuel { get; set; }

    }
}
