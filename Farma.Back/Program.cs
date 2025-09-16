using Microsoft.EntityFrameworkCore;
using farma_back.Data;
using farma_back.Services;
using farma_back.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure SQLite
builder.Services.AddDbContext<FarmaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                     "Data Source=farma.db"));

// Register services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IGeoService, GeoService>();

// Configure CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowAll");
}

// Apply database migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FarmaContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(context);
}

app.UseHttpsRedirection();

// Custom authentication middleware
app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapGet("/", () => new
{
    service = "Farma API",
    version = "1.0.0",
    timestamp = DateTime.UtcNow,
    status = "Running"
});

app.Run();
