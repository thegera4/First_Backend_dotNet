using First_Backend_dotNet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Services
{
    // Esta interfaz pueede ser usada por cualquier controlador que necesite realizar operaciones CRUD, siempre y cuando tenga la
    // misma cantidad de opeeracion que se definieron aqui (por ejemplo para insertar un venta, un producto, un cliente, etc).
    // Con generics, podemos definir el tipo de DTO al momento que se implementa la interfaz, por ejemplo para el caso de Beers, se recibe un BeerInsertDto.

    public interface ICommonService<T, TI, TU> // con generics podemos definir
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T?> GetById(int id); // es tarea del controlador regresar un ActionResult ya que ActionResult esta ligado a la respuesta HTTP
        Task<T> Add(TI beerInsertDto); 
        Task<T?> Update(int id, TU beerUpdateDto);
        Task<T?> Delete(int id);
        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}
