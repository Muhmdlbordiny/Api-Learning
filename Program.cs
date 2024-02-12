using AspNetBeginner;
using AspNetBeginner.Authentication;
using AspNetBeginner.Data;
using AspNetBeginner.Filters;
using AspNetBeginner.MiddleWare;
using AspNetBeginner.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config.json");
builder.Services.AddLogging(Aef=>Aef.AddDebug());

//var attachment = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();
//builder.Services.AddSingleton(attachment);
//var Attachment = new AttachmentOptions();
//builder.Configuration.GetSection("Attachments").Bind(Attachment);
//builder.Services.AddSingleton(Attachment);
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));



// Add services to the container.
builder.Services.AddControllers(options=>
{ 
    options.Filters.Add<LogActivityFilter>(); 
});
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
//builder.Services.AddScoped<IWeatherForeCastServices, WeatherForeCastServices>();// dependence Registration
//builder.Services.AddTransient<WeatherForeCastServices>();
//builder.Services.AddSingleton<WeatherForeCastServices>();
builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic",null)
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseMiddleware<rateLimitingMiddleWare>();
app.UseMiddleware<ProfilingMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
