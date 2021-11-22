using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProductDataClient,ProductDataClient>();
builder.Services.AddHttpClient<IProductDataClient,ProductDataClient>(client => 
{
    client.BaseAddress = builder.Configuration.GetServiceUri("eShop-Catalog-API");
});
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPurchaseRepo,PurchaseRepo>();
builder.Services.AddControllers().AddDapr();
//builder.Services.AddHostedService<MessageBusSubscriber>();
//builder.Services.AddSingleton<IEventProcessor, EventProcessor>(); 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});
PrepDb.PrepPopulation(app);

app.Run();
