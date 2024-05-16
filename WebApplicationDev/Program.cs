namespace WebApplicationDev
{
    // Controllers - содержит контроллеры приложений. Отвечает за обработку запросов от клиентов и возвращают ответы.
    // В контроллере начинается обработка запросов от клиента

    // Models - содержаться модели приложения. Модели это данные. 

    // Views - представление приложения. Отображение данных.

    // Properties - настройки приложения.

    // wwwroot - содержит статические файлы приложения: изобр, css, js.

    // appsettings - настройки
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build(); // билдим объект приложения. builder конструирует из информации, что он имеет оюъект вэб приложения

            // получили веб приложение, далее его нужно сконфигурировать и запустить

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) // проверяем, девел или не девел, т.е разраб или продакшн. Настраивается в appsettings.json. 
            {
                // если продашн
                app.UseExceptionHandler("/Home/Error"); // конфиг обработчик ошибок
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // настраиваем Hsts. Отвечает за строгую проверку заголовков запросов, кот приходят серверу
            }

            app.UseHttpsRedirection(); // все запорсы, кот придут по Http будут направлены в Https, т.е будет добавлено шифрование
            app.UseStaticFiles(); // отвечает за поддержку статичных файлов. в данном случае находятся в каталоге wwwroot

            app.UseRouting(); // активируем компонент роутинга. с помощью роутинга приходящие запросы перенаправляются к контроллерам

            app.UseAuthorization(); // авторизовывает запросы к контроллерам

            app.MapControllerRoute(  // отвечает за мапинг всех вызовов в контроллер. Мапинг - разбор url c помощью кот
                                     // обратились к сервису и соотношение этого url с контроллером и методом этого контроллера,
                                     // кот отвечает за обработку соотв запроса.
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                // сейчас должны обрабатываться запросы вида:
                // http://localhost/home/index/1
                // http://localhost/home/index/
                // http://localhost/home/
                // http://localhost/
                
            app.Run();
        }
    }
}
