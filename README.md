## Description
.Net Core 6 DoctorAppointment Web Application that contains Get/Create/Update/Delete Doctor, Patient, and Appointment

## App Breakdown

|                    |                                                                                                                                                                                                                                                                  |
| -------------------|--------------------------------------------------------------------------------------|
| Controller         | Exposes endpoints                                                                    |
| Services           | Contains the logic                                                                   |
| Repository         | Data Access Layer that talks to Protgres DB                                          |
| Models             | Contains DB classes                                                                  |
| DTOs               | Conatains the interactive models which will be mapped to the actual models in DB     |
| Seed.cs            | Seeds data to the DB on the intial run                                               |

### Database
- Postgres:
  - Name: `DoctorAppointment`
  - Username: `DoctorAppointment`
  - Password: `password`
  - port: `5432`


### Run app locally
- Clone the repository to your machine
- Connect to Postgres DB
- Run the application
- Navigate to: `http://localhost:8080/swagger`


### Run app using docker
- Clone the repository to your machine
- Open the terminal and navigate to project directory: `cd Projects/../DoctorAppointmentApp/DoctorAppointmentApp`
- Open docker
- Open the terminal and run this command: `docker-compose up`
- Navigate to: `http://localhost:8080/swagger`



