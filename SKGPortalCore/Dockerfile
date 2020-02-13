#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1903 AS build
WORKDIR /src
COPY ["SKGPortalCore/SKGPortalCore.csproj", "SKGPortalCore/"]
COPY ["SKGPortalCore.Model/SKGPortalCore.Model.csproj", "SKGPortalCore.Model/"]
COPY ["SKGPortalCore.Lib/SKGPortalCore.Lib.csproj", "SKGPortalCore.Lib/"]
COPY ["SKGPortalCore.Graph/SKGPortalCore.Graph.csproj", "SKGPortalCore.Graph/"]
COPY ["SKGPortalCore.Repository/SKGPortalCore.Repository.csproj", "SKGPortalCore.Repository/"]
COPY ["SKGPortalCore.Data/SKGPortalCore.Data.csproj", "SKGPortalCore.Data/"]
RUN dotnet restore "SKGPortalCore/SKGPortalCore.csproj"
COPY . .
WORKDIR "/src/SKGPortalCore"
RUN dotnet build "SKGPortalCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SKGPortalCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SKGPortalCore.dll"]