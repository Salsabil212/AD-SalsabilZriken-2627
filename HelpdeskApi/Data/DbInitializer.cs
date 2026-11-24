using HelpdeskApi.Models;

namespace HelpdeskApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(HelpdeskDbContext context)
        {
            if (context.Categories.Any()) return;

            var hardware = new Category { Name = "Hardware" };
            var software = new Category { Name = "Software" };
            var netwerk = new Category { Name = "Netwerk" };
            var account = new Category { Name = "Account" };

            context.Categories.AddRange(hardware, software, netwerk, account);
            context.SaveChanges();

            context.Tickets.AddRange(
                new Ticket { CategoryId = hardware.Id, Title = "Scherm doet het niet meer", Description = "Monitor blijft zwart na opstarten.", Status = "Open", Priority = "High", CreatedBy = "jdoe", CreatedAt = DateTime.Now },
                new Ticket { CategoryId = software.Id, Title = "Outlook crasht", Description = "Outlook sluit af bij openen bijlage.", Status = "InProgress", Priority = "Medium", CreatedBy = "asmith", CreatedAt = DateTime.Now },
                new Ticket { CategoryId = netwerk.Id, Title = "Geen wifi op 2e verdieping", Description = "Wifi-signaal valt regelmatig weg.", Status = "Open", Priority = "Medium", CreatedBy = "mvandamme", CreatedAt = DateTime.Now },
                new Ticket { CategoryId = account.Id, Title = "Wachtwoord reset nodig", Description = "Gebruiker geraakt niet meer ingelogd.", Status = "Closed", Priority = "Low", CreatedBy = "pvdbossche", CreatedAt = DateTime.Now }
            );

            context.SaveChanges();
        }
    }
}