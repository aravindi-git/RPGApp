# RPGApp 
This is a simple example for a layered architecture project in .NET core and EF Core. This is a Web API application with CRUD operations.  The structure of the solution has several important layers with unique responsibilities. 


Domain Layer

The domain layer serves as the core layer of the application. It contains all the business logic definitions(interfaces), models, domain-specific entities, DTOs, rules, and validations that define the core problem domain. This layer remains independent of any other application layers.

Data Access Layer (DAL)

The Data Access Layer is responsible for managing all database-related operations, including data processing, data retrieval, and persistence. Components like Entity Framework, DbContext, repository implementations, and database configurations are placed within this layer. It depends on the Domain layer only.

Service Layer

The Data Access Layer (DAL) is solely responsible for database operations. The Service Layer utilizes the data access methods, processes the data, and returns the processed result to the Presentation Layer, which in this case is the Web API project. The Service Layer should not depend on the DAL but only on the Domain Layer. Acting as an intermediary between the Domain and DAL, the Service Layer integrates them by retrieving data from the DAL and processing it according to the rules defined in the Domain Layer.

Presentation Layer

ASP.NET Core Web API project. This uses the Service layer to fetch the results. The Dependency Injection container is in this layer.
