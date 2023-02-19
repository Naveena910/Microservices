using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace XUnitUser
{
    public class InMemorydataContext
    {
        public static RepositoryContext inmemory()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>().UseInMemoryDatabase(databaseName: "MyDatabase").Options;
            var context = new RepositoryContext(options);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            data(context);

            return context;
        }
        private static void data(RepositoryContext context)
        {

            User user = new User();

            user.Id = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26");
        

            user.Password = "Navee@2002";
            user.FirstName = "Naveena";
            user.LastName = "T";
            user.Email = "Naveena@gmail.com";
            user.UserType = "Admin";
            user.DateCreated = DateTime.Now;
            user.DateUpdated = DateTime.Now;
            user.IsActive = true;
            user.Address = new List<Address>();
            Address address1 = new Address()
            {
                UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"),
                Id = Guid.Parse("d7374434-18c3-4100-5e95-08dae952ed30"),
                Line1 = "12131",
                Line2 = "street",
                State = "TamilNadu",
                Pincode = 699910,
                City = "Chennai",
                Country = "India",
                DateCreated= DateTime.Now,
                DateUpdated= DateTime.Now,
                Delivery_Phone=9344958244,
                IsActive= true,
                Type="Home"
           };
            user.Address.Add(address1);
            user.Payment=new List<Payment>();
            Payment payment = new Payment()
            {
                UserId = Guid.Parse("c572c99e-ee1f-4d17-b69c-08dae952ed26"),
                Id=Guid.Parse("abd49ed6-1204-4472-7bca-08dae952ed37"),
                Type= "CreditCard",
                AccountNumber= "1234567890123056",
                Expiry="09/2022",
                DateCreated= DateTime.Now,
                DateUpdated= DateTime.Now,
                IsActive= true,
             

            };
            user.Payment.Add(payment);
            context.User.Add(user);
            context.SaveChanges();

        }

    }
}
