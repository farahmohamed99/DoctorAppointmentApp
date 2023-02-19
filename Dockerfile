# add maven docker image from docker hub which also contains java
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /source

COPY DoctorAppointmentApp/*csproj .

RUN dotnet restore

# copy all files from current local dir to the container
COPY DoctorAppointmentApp/. .

RUN dotnet publish -c release -o /app
# set default dir so that next commands executes in /home/app/trella-transactions-api-test dir

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app

COPY --from=build /app .

# this command will run when executing docker run
ENTRYPOINT ["dotnet", "DoctorAppointmentApp.dll"]



