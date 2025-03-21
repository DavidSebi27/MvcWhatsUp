using MvcWhatsUp.Repositories;

namespace MvcWhatsUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUsersRepository, DbUsersRepository>();
            builder.Services.AddScoped<IChatsRepository, DbChatsRepository>(); //Important -- Dependency injection, as I understand is fairly important, the repo needs to be 
            // registered here. For some reason it doesnt know how to provide an instance of DummyUsers? Looked it up and only works if this is specified
            // When the app starts, this is set up. Dummy Repo is registered as a singleton -only one instance of a class exists throughout the code (important for changes)-which gets
            //automatically provided from here, no need to create a manual instance in different controllers

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

            app.Run();
        }
    }
}
