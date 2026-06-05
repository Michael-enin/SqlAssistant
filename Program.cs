using SqlAssistant.Services.Generators;
using SqlAssistant.Services.Metadata;
using SqlAssistant.Services.OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddOpenApi();

// add controllers
builder.Services.AddControllers();

// swagger/openapi
builder.Services.AddEndpointsApiExplorer();
//builder.Services.Addswa();
builder.Services.AddSwaggerGen();
//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//add dependency injection for services
builder.Services.AddScoped<MetadataService>();
builder.Services.AddScoped<RelationshipService>();
builder.Services.AddScoped<AIPromptBuilderService>();
builder.Services.AddScoped<SPGeneratorService>();
builder.Services.AddScoped<OpenAiService>();
var app = builder.Build();
app.UseCors("AllowAll");
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();



app.Run();


