using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sds.DentistChair.Api.Configurations;
using Sds.DentistChair.Api.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAutoMapperProfiles();
builder.Services.AddVersionedSwagger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseVersionedSwagger(provider);


app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
