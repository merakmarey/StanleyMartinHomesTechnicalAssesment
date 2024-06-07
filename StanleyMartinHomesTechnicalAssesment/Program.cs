using StanleyMartinHomesTechnicalAssesment.DataEntities;
using StanleyMartinHomesTechnicalAssesment.DataEntities.Models;
using StanleyMartinHomesTechnicalAssesment.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApiContext>();

builder.Services.AddScoped<IProductsRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApiContext>();
        context.Database.EnsureCreated();
        if (!context.Products.Any())
        {
            SeedData(context);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
    }
}


app.Run();





void SeedData(ApiContext context)
{
        // in memory data hydration

        List<Metro> metros = new List<Metro>() {
                new Metro() {
                    MetroAreaID = 1006,
                    MetroAreaTitle = "Richmond",
                    MetroAreaStateAbr = "VA",
                    MetroAreaStateName = "Virginia",
                },
                new Metro() {
                    MetroAreaID = 1007,
                    MetroAreaTitle = "Atlanta",
                    MetroAreaStateAbr = "GA",
                    MetroAreaStateName = "Georgia",
                },
                new Metro() {
                    MetroAreaID = 1008,
                    MetroAreaTitle = "Charleston",
                    MetroAreaStateAbr = "SC",
                    MetroAreaStateName = "South Carolina",
                }
            };

        List<Project> projects = new List<Project>()
            {
                new Project()
                {
                    ProjectGroupID = 23,
                    MetroAreaID = 1007,
                    FullName="Edgewater",
                    Status = 'a'
                },
                    new Project()
                {
                    ProjectGroupID = 25,
                    MetroAreaID = 1007,
                    FullName="Inwood",
                    Status = 'a'
                },
                    new Project()
                {
                    ProjectGroupID = 41,
                    MetroAreaID = 1008,
                    FullName="Estuary at Bowen Village",
                    Status = 'a'
                },
                    new Project()
                {
                    ProjectGroupID = 43,
                    MetroAreaID = 1008,
                    FullName="Mixson",
                    Status = 'i'
                },
                    new Project()
                {
                    ProjectGroupID = 47,
                    MetroAreaID = 1008,
                    FullName="Oldfield",
                    Status = 'a'
                }
            };

        List<Product> products = new List<Product>() {
                new Product() {
                    ProductID = "Y58",
                    ProductName = "The Prescott",
                    ProjectGroupID = 23,
                    ProjectName = "Edgewater"
                },
                new Product() {
                    ProductID = "980",
                    ProductName = "The Davis",
                    ProjectGroupID = 23,
                    ProjectName = "Edgewater 50"
                },
                new Product() {
                    ProductID = "E15",
                    ProductName = "The Amelia",
                    ProjectGroupID = 23,
                    ProjectName = "Edgewater 50"
                },
                new Product() {
                    ProductID = "Y54",
                    ProductName = "The Lockwood",
                    ProjectGroupID = 23,
                    ProjectName = "Edgewater 50"
                },
                new Product() {
                    ProductID = "U68",
                    ProductName = "The Stono",
                    ProjectGroupID = 41,
                    ProjectName = "Estuary"
                },
                new Product() {
                    ProductID = "1601",
                    ProductName = "The Moultrie",
                    ProjectGroupID = 41,
                    ProjectName = "Estuary"
                },
                new Product() {
                    ProductID = "980",
                    ProductName = "The Dupree",
                    ProjectGroupID = 25,
                    ProjectName = "Inwood SFD"
                },
                new Product() {
                    ProductID = "E15",
                    ProductName = "Inwood",
                    ProjectGroupID = 25,
                    ProjectName = "Inwood SFD"
                },
                new Product() {
                    ProductID = "1674",
                    ProductName = "The Stella",
                    ProjectGroupID = 43,
                    ProjectName = "Mixson"
                },
                new Product() {
                    ProductID = "1665",
                    ProductName = "The Tidalview",
                    ProjectGroupID = 47,
                    ProjectName = "Oldfield"
                },
                new Product() {
                    ProductID = null,
                    ProductName = null,
                    ProjectGroupID = 47,
                    ProjectName = "Oldfield"
                }
};


        context.Metros.AddRange(metros);
        context.Projects.AddRange(projects);
        context.Products.AddRange(products);

        context.SaveChangesAsync();
}


