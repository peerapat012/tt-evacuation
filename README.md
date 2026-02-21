## .NET API for test assignment
this api make for simulate an Evacuation Planning and Monitoring API for a Disaster Response Team.

## Prerequisites
- .NET 10 SDK
- Docker
- Git
  
## Clone Repository
```bash
git clone https://github.com/peerapat012/tt-evacuation.git
cd tt-evacuation
cd tt-api
```

## Restore Dependencies
```bash
dotnet restore
```

## Run Redis with Docker Compose
you need to run redis before running api app by using docker compose
```bash
docker compose up -d
```
redis port setting in appsettings.json
```
{
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

## Run Application Locally
```bash
dotnet run
```
this app will run on path: http://localhost:5078

## Use API Document for run and test by Scalar
```bash
http://localhost:5078/scalar/
```

## Project Structure
/Controllers -> API endpoints

/Services -> Business logic

/Models -> Data Type

/Utils -> utility function

/Datas -> Mockup data

/Dtos -> Data Type for API response and request
