using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimeiraAPI;
using PrimeiraAPI.Application.Map;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;
using PrimeiraAPI.Infraestrutura.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddApiVersioning(o =>
//{
//	o.AssumeDefaultVersionWhenUnspecified = true;
//	o.DefaultApiVersion = new ApiVersion(1, 0);
//});

//builder.Services.AddVersionedApiExplorer(setup =>
//{
//	setup.GroupNameFormat = "'v'VVV";
//	setup.SubstituteApiVersionInUrl = true;
//});

builder.Services.AddSwaggerGen(c =>
{
	//c.OperationFilter<SwaggerDefaultValues>();

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
	{
		new OpenApiSecurityScheme
		{
		Reference = new OpenApiReference
			{
			Type = ReferenceType.SecurityScheme,
			Id = "Bearer"
			},
			Scheme = "oauth2",
			Name = "Bearer",
			In = ParameterLocation.Header,

		},
		new List<string>()
		}
	});


});


builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));


var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error-development");
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
