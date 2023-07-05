Simple RabbitMQ Application

The project consists of two applications: a message publisher and a message consumer. First one sends some message to RabbitMQ and another subscribe them and consume from RabbitMQ queue and insert to databases. Also a web API application created to simply check the inserted data to databases. 

-	Design RabbitMQ bus in a generic way and use it in applications via abstraction layer
-	Insert received messages to SQL server and Redis.
-	Auto Migrate database and tables
-	Dapper micro ORM
-	Dependency Injection and IOC container
-	Onion architecture
-	Logging using Serilog and log into file and seq. (configurations are read from appsetting.json)
-	Exception handling
-	Fluent Validation
-	Executable by Windows Service
-	Docker compose
