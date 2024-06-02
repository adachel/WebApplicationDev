


# шаг 1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env  # базовый образ SDK .NET 7.0 в качестве основы для этапа сборки.
WORKDIR /app	# рабочий каталог в контейнере, куда будут копироваться и собираться файлы проекта.
COPY *.csproj ./	# kопирует файл проекта в рабочий каталог в контейнере.
RUN dotnet restore # Запускает команду dotnet restore для восстановления зависимостей проекта
COPY . ./	# копируем код в каталог в кот находимся
RUN dotnet publish -c Relese -o out		# Запускает команду dotnet publish для сборки проекта в режиме "Release" и помещает результаты в каталог "out"

# шаг 2
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime # для этапа выполнения
WORKDIR /app # Задает рабочий каталог в контейнере для этапа выполнения.
# ENV ASPNETCORE_ENVIRONMENT=Development	# переменное окружение, для возможности запускать swagger
EXPOSE 80	# какой порт будет слушать контейнер
COPY --from=build-env /app/out .	# Копирует результаты сборки из этапа сборки (каталог "out") в текущий рабочий каталог в контейнере этапа выполнения.
ENTRYPOINT ["dotnet", "/app/Lec4WeatherForecast.dll"]	# Эта строка задает точку входа для контейнера, указывая, какую команду следует выполнить при запуске контейнера.


командой docker build -t weatherforecast . - билдим образ. докер должен быть запущен

командой docker run -p 80:90 weatherforecast -- -p задает правило перенаправления портов. weatherforecast - имя образа.