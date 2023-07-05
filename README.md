Simple RabbitMQ Application

The project consists of two applications: a message publisher and a message consumer. First one (SimpleRabbit.Publisher) sends some message to RabbitMQ and another (SimpleRabbit.Subscriber) subscribe them and consume from RabbitMQ queue and insert to databases. Also a web API application created to simply check and read the inserted data to databases. 

-	Design RabbitMQ bus in a generic way and use it in applications via abstraction layer
-	Insert received messages to SQL server and Redis
-	Auto Create database and tables (Fluent Migrator library)
-	Dapper micro ORM
-	Dependency Injection and IOC container
-	Onion architecture
-	Logging using Serilog and log into file and seq. (configurations are read from appsetting.json)
-	Exception handling
-	Fluent Validation
-	Executable the services (publisher and subscriber) by Windows Service
-	Docker compose
