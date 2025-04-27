using eCommerce.ProductsService.BusinessLogicLayer;
using eCommerce.ProductsService.DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
//Add Business Logic Layer and DAL
builder.Services.AddBusinessLogicLayer();
builder.Services.AddDataAccessLayer();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//Auth
app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/", () => "Hello World!");

app.Run();
