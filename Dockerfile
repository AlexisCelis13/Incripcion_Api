# Utiliza la imagen oficial de .NET para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AcademicEnrollmentApi/AcademicEnrollmentApi.csproj"
RUN dotnet publish "AcademicEnrollmentApi/AcademicEnrollmentApi.csproj" -c Release -o /app/publish

# Utiliza la imagen oficial de .NET para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AcademicEnrollmentApi.dll"] 