using First_Backend_dotNet.DTOs;
using First_Backend_dotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace First_Backend_dotNet.Services
{
    public class BeerService(StoreContext context) : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private readonly StoreContext _context = context; // Contexto para conexion a base de datos (Entity Framework)

        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beers.Select(b => new BeerDto
        {
            Id = b.BeerId,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandId
        }).ToListAsync();

        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if(beer == null)
            {
                return null;
            }
            return new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            };
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol,
                BrandId = beerInsertDto.BrandID
            };

            await _context.Beers.AddAsync(beer); // aqui es para decirle a entity framework que va a haber un cambio
            await _context.SaveChangesAsync(); // en esta lineea es donde se guardan los cambios en la base de datos

            return new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            };
        }

        public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);
            if(beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandId = beerUpdateDto.BrandID;
                await _context.SaveChangesAsync();
                return new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandId
                };
            }
            return null;
        }

        public async Task<BeerDto?> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandId
                };
                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();
                return beerDto;
                
            }
            return null;
        }

    }
}