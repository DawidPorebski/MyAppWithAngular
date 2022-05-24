FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DevOpsLearningApp/DevOpsLearningApp.csproj", "DevOpsLearningApp/"]
RUN dotnet restore "DevOpsLearningApp/DevOpsLearningApp.csproj"
COPY . .
WORKDIR "/src/DevOpsLearningApp"
RUN dotnet build "DevOpsLearningApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOpsLearningApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOpsLearningApp.dll"]