#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 7001
EXPOSE 443


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ASP.NET Core Service/ServerFTM/ServerFTM.csproj", "ASP.NET Core Service/ServerFTM/"]
COPY ["WPF Client App/DTO/Library.csproj", "WPF Client App/DTO/"]
COPY ["ASP.NET Core Service/API.Shared/API.Shared.csproj", "ASP.NET Core Service/API.Shared/"]
RUN dotnet restore "ASP.NET Core Service/ServerFTM/ServerFTM.csproj"
COPY . .
WORKDIR "/src/ASP.NET Core Service/ServerFTM"
RUN dotnet build "ServerFTM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServerFTM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerFTM.dll"]