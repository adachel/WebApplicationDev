namespace WebApplicationDev
{
    // Controllers - �������� ����������� ����������. �������� �� ��������� �������� �� �������� � ���������� ������.
    // � ����������� ���������� ��������� �������� �� �������

    // Models - ����������� ������ ����������. ������ ��� ������. 

    // Views - ������������� ����������. ����������� ������.

    // Properties - ��������� ����������.

    // wwwroot - �������� ����������� ����� ����������: �����, css, js.

    // appsettings - ���������
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build(); // ������ ������ ����������. builder ������������ �� ����������, ��� �� ����� ������ ��� ����������

            // �������� ��� ����������, ����� ��� ����� ���������������� � ���������

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) // ���������, ����� ��� �� �����, �.� ������ ��� ��������. ������������� � appsettings.json. 
            {
                // ���� �������
                app.UseExceptionHandler("/Home/Error"); // ������ ���������� ������
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // ����������� Hsts. �������� �� ������� �������� ���������� ��������, ��� �������� �������
            }

            app.UseHttpsRedirection(); // ��� �������, ��� ������ �� Http ����� ���������� � Https, �.� ����� ��������� ����������
            app.UseStaticFiles(); // �������� �� ��������� ��������� ������. � ������ ������ ��������� � �������� wwwroot

            app.UseRouting(); // ���������� ��������� ��������. � ������� �������� ���������� ������� ���������������� � ������������

            app.UseAuthorization(); // �������������� ������� � ������������

            app.MapControllerRoute(  // �������� �� ������ ���� ������� � ����������. ������ - ������ url c ������� ���
                                     // ���������� � ������� � ����������� ����� url � ������������ � ������� ����� �����������,
                                     // ��� �������� �� ��������� ����� �������.
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                // ������ ������ �������������� ������� ����:
                // http://localhost/home/index/1
                // http://localhost/home/index/
                // http://localhost/home/
                // http://localhost/
                
            app.Run();
        }
    }
}
