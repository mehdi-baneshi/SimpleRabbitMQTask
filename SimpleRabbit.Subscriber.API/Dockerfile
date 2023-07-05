#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SimpleRabbit.Subscriber.API/SimpleRabbit.Subscriber.API.csproj", "SimpleRabbit.Subscriber.API/"]
COPY ["SimpleRabbit.Infra.Ioc/SimpleRabbit.Infra.Ioc.csproj", "SimpleRabbit.Infra.Ioc/"]
COPY ["SimpleRabbit.Core.Domain/SimpleRabbit.Core.Domain.csproj", "SimpleRabbit.Core.Domain/"]
COPY ["SimpleRabbit.Infra.Bus/SimpleRabbit.Infra.Bus.csproj", "SimpleRabbit.Infra.Bus/"]
COPY ["SimpleRabbit.Subscriber.Application/SimpleRabbit.Subscriber.Application.csproj", "SimpleRabbit.Subscriber.Application/"]
COPY ["SimpleRabbit.Subscriber.Domain/SimpleRabbit.Subscriber.Domain.csproj", "SimpleRabbit.Subscriber.Domain/"]
COPY ["SimpleRabbit.Subscriber.DapperDataAccess/SimpleRabbit.Subscriber.DapperDataAccess.csproj", "SimpleRabbit.Subscriber.DapperDataAccess/"]
COPY ["SimpleRabbit.Subscriber.RedisDataAccess/SimpleRabbit.Subscriber.RedisDataAccess.csproj", "SimpleRabbit.Subscriber.RedisDataAccess/"]
RUN dotnet restore "SimpleRabbit.Subscriber.API/SimpleRabbit.Subscriber.API.csproj"
COPY . .
WORKDIR "/src/SimpleRabbit.Subscriber.API"
RUN dotnet build "SimpleRabbit.Subscriber.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleRabbit.Subscriber.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleRabbit.Subscriber.API.dll"]