namespace HungryPizza.Infra.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
    }
}
