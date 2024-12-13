using ToDoList.Presentation.DI;
using ToDoList.Presentation.MiddlewareManagement;

var builder = WebApplication.CreateBuilder(args);

// Adding all dependencies
builder.Services.AddAllDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using all middlewares
app.UseAllMiddlewares();
app.Run();