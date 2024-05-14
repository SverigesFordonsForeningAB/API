
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
            app.Run();
        }
    }
}
