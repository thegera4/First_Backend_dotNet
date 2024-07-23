using First_Backend_dotNet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// esta es la vieja forma de inyeccion de dependencias
// builder.Services.AddSingleton<IPeopleService, People2Service>(); // Aqui estamos registrando la interfaz y la clase que la implementa para que se pueda inyectar en cualquier parte de la aplicacion
// esta es la nueva forma de inyeccion de dependencias con key
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService"); // aqui estamos registrando la interfaz y la clase que la implementa con una key para que se pueda inyectar en cualquier parte de la aplicacion
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service"); // con esto podemos tener varias implementaciones de la misma interfaz y solo inyectar la que necesitamos

// Ejemeplo de inyeccion con los 3 diferentes tipos: Singleton, Scoped y Transient.
// Singleton es para que solo se cree un solo objeto de la clase en toda la aplicacion (nivel global aunque sea inyectado en diferentes clases, por diferentes clientes)
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
// Scoped es para que se cree un objeto nuevo por cada cliente que envie request (aunque sera el mismo objeto para todas las request de un mismo cliente)
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped"); 
// Transient es para que se cree un objeto nuevo cada vez que se inyecta aunque sea la misma clase
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
