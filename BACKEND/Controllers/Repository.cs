namespace BACKEND.Controllers
{
    public class Repository
    {
        public static List<PersonaDatos> persona = new List<PersonaDatos> 
        { 
            new PersonaDatos()
            {
                id = 1,
                age = 25,
                name ="Delmy Michelle Moreno de Menjivar",
                email = "delmymoreno@gmail.com"
            },
            new PersonaDatos()
            {
                id = 2,
                age = 1,
                name ="Rocio Michelle Menjivar Moreno",
                email = "rociomenjivar@gmail.com"
            },
            new PersonaDatos()
            {
                id = 3,
                age = 1,
                name ="Mathias Lisandro Menjivar Moreno",
                email = "mathiasmenjivar@gmail.com"
            }
        };
    }

    public class PersonaDatos
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
    }
}
