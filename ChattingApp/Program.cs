using ChattingApp.Error_Model;
using ChattingApp.Extensions;
using ChattingApp.Hubs;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAppDbContext(builder.Configuration);
builder.Services.ConfigureCorsPolicy();
builder.Services.ConfigureRepoBase();
builder.Services.ConfigureUserRepo();
builder.Services.ConfigureRoomRepo();
builder.Services.ConfigureMessageRepo();
builder.Services.ConfigureConversationRepo();
builder.Services.AddSignalR();

// configure error response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    /// that is default return badRequest 
    /// and i use InvalidModelStateResponseFactory to return shape of response to custome on it
    options.InvalidModelStateResponseFactory = actionContetxt =>
    {
        var errors = actionContetxt.ModelState.Where(E => E.Value.Errors.Count() > 0)
            .SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToArray();
        var validationErrorReponse = new ApiValidationErrorResponse()
        { Errors = errors };
        return new BadRequestObjectResult(validationErrorReponse);

    };
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors("CorsPolicy");

app.MapHub<ChatHub>("/chat");
 

app.MapControllers();

app.Run();
