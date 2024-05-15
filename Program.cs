
using Microsoft.EntityFrameworkCore;
using SverigesFordonsFörening.Data;

namespace SverigesFordonsFörening
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Return all customer
            app.MapGet("/customer", async (ApplicationDbContext context) =>
            {
                var customers = await context.Customers.ToListAsync();
                if (customers.Count == 0)
                {
                    return Results.NotFound("ingen kunder hittades");
                }
                return Results.Ok(customers);
            });
            //Create a new customer
            app.MapPost("/customer", async (Customer customer, ApplicationDbContext context) =>
             {
                 context.Customers.Add(customer);
                 await context.SaveChangesAsync();
                 return Results.Created($"/customers/{customer.CustomerId}", customer);

             });
            //Get customerby id
            app.MapGet("/customer{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                if(customer == null)
                {
                    return Results.NotFound("customer not found");
                }
                return Results.Ok(customer);
            });
            //Edit a customer
            app.MapPut("/customer/{id:int}", async (int id, Customer uppdateCustomer, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return Results.NotFound("customer not found");
                }
                customer.FirstName = uppdateCustomer.FirstName;
                customer.LastName = uppdateCustomer.LastName;
                customer.Email = uppdateCustomer.Email;
                customer.Address = uppdateCustomer.Address;
                customer.Phone = uppdateCustomer.Phone;
                customer.SocialSecurityNumber = uppdateCustomer.SocialSecurityNumber;
                return Results.Ok(customer);
            });
            //Delete Customer by id
            app.MapDelete("/customer/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return Results.NotFound("customer not found");
                }
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                return Results.Ok($"Customer with ID: {id} deleted");
            });


            //Return all car
            app.MapGet("/car", async (ApplicationDbContext context) =>
            {
                var car = await context.Cars.ToListAsync();
                if (car.Count == 0)
                {
                    return Results.NotFound("car not found");
                }
                return Results.Ok(car);
            });
            //Create a new car
            app.MapPost("/car", async (Car car, ApplicationDbContext context) =>
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
                return Results.Created($"/car/{car.CarId}", car);

            });
            //Get car id
            app.MapGet("/car{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                if (car == null)
                {
                    return Results.NotFound("car not found");
                }
                return Results.Ok(car);
            });
            //Edit a car
            app.MapPut("/car/{id:int}", async (int id, Car uppdateCar, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                if (car == null)
                {
                    return Results.NotFound("car not found");
                }
                car.Brand = uppdateCar.Brand;
                car.Model = uppdateCar.Model;
                car.CarPrice = uppdateCar.CarPrice;
              
                return Results.Ok(car);
            });
            //Delete car by id
            app.MapDelete("/car/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                if (car == null)
                {
                    return Results.NotFound("car not found");
                }
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
                return Results.Ok($"Car with ID: {id} deleted");
            });

            //Return all motorcycle
            app.MapGet("/motorcycle", async (ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.ToListAsync();
                if (motorcycle.Count == 0)
                {
                    return Results.NotFound("motorcycle not found");
                }
                return Results.Ok(motorcycle);
            });
            //Create a new motorcycle
            app.MapPost("/motorcycle", async (Motorcycle motorcycle, ApplicationDbContext context) =>
            {
                context.Motorcycles.Add(motorcycle);
                await context.SaveChangesAsync();
                return Results.Created($"/motorcycle/{motorcycle.MotorcycleId}", motorcycle);

            });
            //Get motorcycle id
            app.MapGet("/motorcycle{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                if (motorcycle == null)
                {
                    return Results.NotFound("motorcycle not found");
                }
                return Results.Ok(motorcycle);
            });
            //Edit a motorcycle
            app.MapPut("/motorcycle/{id:int}", async (int id, Motorcycle uppdateMotorcycle, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                if (motorcycle == null)
                {
                    return Results.NotFound("motorcycle not found");
                }
                motorcycle.Brand = uppdateMotorcycle.Brand;
                motorcycle.Model = uppdateMotorcycle.Model;
                motorcycle.MotorcyclePrice = uppdateMotorcycle.MotorcyclePrice;

                return Results.Ok(motorcycle);
            });
            //Delete motorcycle by id
            app.MapDelete("/motorcycle/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                if (motorcycle == null)
                {
                    return Results.NotFound("motorcycle not found");
                }
                context.Motorcycles.Remove(motorcycle);
                await context.SaveChangesAsync();
                return Results.Ok($"motorcycle with ID: {id} deleted");
            });
            app.Run();
        }
    }
}
