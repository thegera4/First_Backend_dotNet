using AutoMapper;
using First_Backend_dotNet.DTOs;
using First_Backend_dotNet.Models;
using First_Backend_dotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace First_Backend_dotNet.Services
{
    public class BeerService(IRepository<Beer> beerRepository, IMapper mapper) : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private readonly IRepository<Beer> _beerRepository = beerRepository; // Repositorio para realizar las operaciones CRUD
        private readonly IMapper _mapper = mapper; // Automapper para mapear los objetos

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();
            /*return beers.Select(beer => new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            });*/
            return beers.Select(beer => _mapper.Map<BeerDto>(beer));
        }

        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if(beer == null)
            {
                return null;
            }
            /*return new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            };*/
            return _mapper.Map<BeerDto>(beer);
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            /*var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandId = beerInsertDto.BrandID
            };*/

            // Usando Automapper
            var beer = _mapper.Map<Beer>(beerInsertDto);

            await _beerRepository.Add(beer); // aqui es para decirle a entity framework que va a haber un cambio
            await _beerRepository.Save(); // en esta lineea es donde se guardan los cambios en la base de datos

            /*return new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            };*/

            // Usando Automapper
            return _mapper.Map<BeerDto>(beer);
        }

        public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);
            if(beer != null)
            {
                /*beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandId = beerUpdateDto.BrandID;*/

                // Usando Automapper
                beer = _mapper.Map(beerUpdateDto, beer);

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                /*return new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandId
                };*/
                return _mapper.Map<BeerDto>(beer);
            }
            return null;
        }

        public async Task<BeerDto?> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                /*var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandId
                };*/
                var beerDto = _mapper.Map<BeerDto>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
                
            }
            return null;
        }

    }
}