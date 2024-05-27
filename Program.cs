using Microsoft.EntityFrameworkCore;
using SverigesFordonsFörening.Data;

namespace SverigesFordonsFörening
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Konfigurera DbContext med SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Lägg till tjänster i containern
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Konfigurera HTTP-request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            ////////////////////////////// CUSTOMERS ///////////////////////////////////////

            // Returnera alla kunder
            app.MapGet("/customers", async (ApplicationDbContext context) =>
            {
                var customers = await context.Customers.ToListAsync();
                return customers.Count == 0 ? Results.NotFound("Inga kunder hittades") : Results.Ok(customers);
            });

            // Skapa en ny kund
            app.MapPost("/customer", async (Customer customer, ApplicationDbContext context) =>
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
                return Results.Created($"/customers/{customer.CustomerId}", customer);
            });

            // Hämta kund efter id
            app.MapGet("/customer/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                return customer == null ? Results.NotFound("Kund hittades inte") : Results.Ok(customer);
            });

            // Uppdatera en kund
            app.MapPut("/customer/{id:int}", async (int id, Customer updateCustomer, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return Results.NotFound("Kund hittades inte");
                }

                customer.FirstName = updateCustomer.FirstName;
                customer.LastName = updateCustomer.LastName;
                customer.Email = updateCustomer.Email;
                customer.Address = updateCustomer.Address;
                customer.Phone = updateCustomer.Phone;
                customer.SocialSecurityNumber = updateCustomer.SocialSecurityNumber;
                await context.SaveChangesAsync();
                return Results.Ok(customer);
            });

            // Ta bort en kund efter id
            app.MapDelete("/customer/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return Results.NotFound("Kund hittades inte");
                }
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                return Results.Ok($"Kund med ID: {id} raderad");
            });

            ////////////////////////////// CARS ///////////////////////////////////////

            // Returnera alla bilar
            app.MapGet("/cars", async (ApplicationDbContext context) =>
            {
                var cars = await context.Cars.ToListAsync();
                return cars.Count == 0 ? Results.NotFound("Inga bilar hittades") : Results.Ok(cars);
            });

            // Skapa en ny bil
            app.MapPost("/car", async (Car car, ApplicationDbContext context) =>
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
                return Results.Created($"/cars/{car.CarId}", car);
            });

            // Hämta bil efter id
            app.MapGet("/car/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                return car == null ? Results.NotFound("Bil hittades inte") : Results.Ok(car);
            });

            // Uppdatera en bil
            app.MapPut("/car/{id:int}", async (int id, Car updateCar, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                if (car == null)
                {
                    return Results.NotFound("Bil hittades inte");
                }

                car.Brand = updateCar.Brand;
                car.Model = updateCar.Model;
                car.CarPrice = updateCar.CarPrice;
                await context.SaveChangesAsync();
                return Results.Ok(car);
            });

            // Ta bort en bil efter id
            app.MapDelete("/car/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var car = await context.Cars.FindAsync(id);
                if (car == null)
                {
                    return Results.NotFound("Bil hittades inte");
                }
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
                return Results.Ok($"Bil med ID: {id} raderad");
            });

            ////////////////////////////// MOTORCYCLES ///////////////////////////////////////

            // Returnera alla motorcyklar
            app.MapGet("/motorcycles", async (ApplicationDbContext context) =>
            {
                var motorcycles = await context.Motorcycles.ToListAsync();
                return motorcycles.Count == 0 ? Results.NotFound("Inga motorcyklar hittades") : Results.Ok(motorcycles);
            });

            // Skapa en ny motorcykel
            app.MapPost("/motorcycle", async (Motorcycle motorcycle, ApplicationDbContext context) =>
            {
                context.Motorcycles.Add(motorcycle);
                await context.SaveChangesAsync();
                return Results.Created($"/motorcycles/{motorcycle.MotorcycleId}", motorcycle);
            });

            // Hämta motorcykel efter id
            app.MapGet("/motorcycle/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                return motorcycle == null ? Results.NotFound("Motorcykel hittades inte") : Results.Ok(motorcycle);
            });

            // Uppdatera en motorcykel
            app.MapPut("/motorcycle/{id:int}", async (int id, Motorcycle updateMotorcycle, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                if (motorcycle == null)
                {
                    return Results.NotFound("Motorcykel hittades inte");
                }

                motorcycle.Brand = updateMotorcycle.Brand;
                motorcycle.Model = updateMotorcycle.Model;
                motorcycle.MotorcyclePrice = updateMotorcycle.MotorcyclePrice;
                await context.SaveChangesAsync();
                return Results.Ok(motorcycle);
            });

            // Ta bort en motorcykel efter id
            app.MapDelete("/motorcycle/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var motorcycle = await context.Motorcycles.FindAsync(id);
                if (motorcycle == null)
                {
                    return Results.NotFound("Motorcykel hittades inte");
                }
                context.Motorcycles.Remove(motorcycle);
                await context.SaveChangesAsync();
                return Results.Ok($"Motorcykel med ID: {id} raderad");
            });

            ///////////////////////////// /ORDERS ///////////////////////////////////////

            // Returnera alla ordrar
            app.MapGet("/orders", async (ApplicationDbContext context) =>
            {
                var orders = await context.Orders.ToListAsync();
                return orders.Count == 0 ? Results.NotFound("Inga ordrar hittades") : Results.Ok(orders);
            });

            // Skapa en ny order
            app.MapPost("/order", async (Order order, ApplicationDbContext context) =>
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
                return Results.Created($"/orders/{order.OrderId}", order);
            });

            // Hämta order efter id
            app.MapGet("/order/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);
                return order == null ? Results.NotFound("Order hittades inte") : Results.Ok(order);
            });

            // Uppdatera en order
            app.MapPut("/order/{id:int}", async (int id, Order updateOrder, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);
                if (order == null)
                {
                    return Results.NotFound("Order hittades inte");
                }

                order.FkCustomerId = updateOrder.FkCustomerId;
                order.FkCarId = updateOrder.FkCarId;
                order.FkMotorcycleId = updateOrder.FkMotorcycleId;
                await context.SaveChangesAsync();
                return Results.Ok(order);
            });

            // Ta bort en order efter id
            app.MapDelete("/order/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var order = await context.Orders.FindAsync(id);
                if (order == null)
                {
                    return Results.NotFound("Order hittades inte");
                }
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                return Results.Ok($"Order med ID: {id} raderad");
            });

            app.Run();
        }
    }
}