var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSession();

app.MapGet("/", (HttpContext context) =>
{
    var id = context.Session.GetString("_ID");
    if (string.IsNullOrEmpty(id))
    {
        id = Guid.NewGuid().ToString();
        context.Session.SetString("_ID", id);
    }

    return $"Session ID: {id}";
})
.WithOpenApi();

app.Run();
