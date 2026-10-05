using apiFestivosColombia.InyeccionDependencias;

//crear el CONSTRUCTOR de la aplicación web
var builder = WebApplication.CreateBuilder(args);

//establecer objeto de configuracion
var configuracion = builder.Configuration;

//establecer los objetos a inyectar
builder.Services.AgregarDependencias(configuracion);

//instanciar los controladores
builder.Services.AddControllers();

//agregar el servicio de SWAGGER
builder.Services.AddSwaggerGen();

//crear la aplicación web
var app = builder.Build();

//permitir SWAGGER (documentación técnica de la API)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
