namespace BACKEND.Controllers
{
    public class Repository
    {
        public static List<PersonaDatos> persona = new List<PersonaDatos> 
        { 
            new PersonaDatos()
            {
                id = 1,
                age = new DateTime(1999,08,09),
                name ="Delmy Michelle Moreno de Menjivar"
            },
            new PersonaDatos()
            {
                id = 2,
                age = new DateTime(2023,10,31),
                name ="Rocio Michelle Menjivar Moreno"
            },
            new PersonaDatos()
            {
                id = 3,
                age = new DateTime(2023,10,31),
                name ="Mathias Lisandro Menjivar Moreno"
            }
        };
    }

    public class PersonaDatos
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime age { get; set; }
        public string email { get; set; }
    }
}
