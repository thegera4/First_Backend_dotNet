using First_Backend_dotNet.Controllers;

namespace First_Backend_dotNet.Services
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(People people) // esta validacion podria cambiar en el futuro y si solo agregamos aqui mas condiciones,
        {
            if(string.IsNullOrEmpty(people.Name)) // tenemos la ventaja de que solo cambiamos aqui y no en todos los lugares donde se validar
            {
                return false;
            }
            return true;
        }
    }
}
