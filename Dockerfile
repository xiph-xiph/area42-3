# build frontend
FROM node:20 AS frontend-build

WORKDIR /frontend

COPY frontend/package*.json ./
RUN npm ci

COPY frontend/ ./
RUN npm run build


# build backend
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS backend-build

WORKDIR /src

COPY backend/*.slnx ./

COPY backend/Backend-Area42-3/ Backend-Area42-3/
COPY backend/Business/ Business/
COPY backend/Data/ Data/

RUN dotnet restore *.slnx

COPY backend/ .

COPY --from=frontend-build /frontend/dist Backend-Area42-3/wwwroot

RUN dotnet publish Backend-Area42-3/Backend-Area42-3.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=backend-build /app/publish ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "Backend-Area42-3.dll"]