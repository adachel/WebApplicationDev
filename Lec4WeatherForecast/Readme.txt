


# ��� 1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env  # ������� ����� SDK .NET 7.0 � �������� ������ ��� ����� ������.
WORKDIR /app	# ������� ������� � ����������, ���� ����� ������������ � ���������� ����� �������.
COPY *.csproj ./	# k������� ���� ������� � ������� ������� � ����������.
RUN dotnet restore # ��������� ������� dotnet restore ��� �������������� ������������ �������
COPY . ./	# �������� ��� � ������� � ��� ���������
RUN dotnet publish -c Relese -o out		# ��������� ������� dotnet publish ��� ������ ������� � ������ "Release" � �������� ���������� � ������� "out"

# ��� 2
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime # ��� ����� ����������
WORKDIR /app # ������ ������� ������� � ���������� ��� ����� ����������.
# ENV ASPNETCORE_ENVIRONMENT=Development	# ���������� ���������, ��� ����������� ��������� swagger
EXPOSE 80	# ����� ���� ����� ������� ���������
COPY --from=build-env /app/out .	# �������� ���������� ������ �� ����� ������ (������� "out") � ������� ������� ������� � ���������� ����� ����������.
ENTRYPOINT ["dotnet", "/app/Lec4WeatherForecast.dll"]	# ��� ������ ������ ����� ����� ��� ����������, ��������, ����� ������� ������� ��������� ��� ������� ����������.


�������� docker build -t weatherforecast . - ������ �����. ����� ������ ���� �������

�������� docker run -p 80:90 weatherforecast -- -p ������ ������� ��������������� ������. weatherforecast - ��� ������.