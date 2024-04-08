# MoonHotels Connectors
This project was developed using an API Gateway approach, where the client sends requests to the Hub, which internally forwards them to the connectors. These connectors are independent services, all structured under a microservices architecture.

# Project Structure
The project is organized into different layers, each with its specific responsibility:

## MoonHotels.Connectors.Api
This project contains generic controllers that can be used by any connector. Currently, only the controller for the search operation is implemented. It also includes extensions to facilitate dependency injection in the connectors.

- Controllers: Available controllers for the connectors.
- Extensions: Extensions that simplify dependency injection.


## MoonHotels.Connectors.Application
Here, business processes are managed. Entities are generated using domain contracts, and a series of services are provided to standardize certain operations, such as sending requests.

- Connection: Contains logic related to connection and communication between systems.
- Operations: This is where common connector operations are located, such as search, quoting, booking, etc. Currently, only the search operation is implemented. Each operation must implement the IConnectorOperation<TConnectorRequest, TConnectorResponse, TSupplierRequest, TSupplierResponse> interface, which defines the basic functions that every operation should have.
- Pipelines: Responsible for processing data for each operation. For each operation, there is a pipeline that executes it, checks for errors, and returns the appropriate response.
- Serializers: Contains serialization services used by the connectors.


## MoonHotels.Connectors.Domain
This is where the contracts and data models exposed by the API to clients are located. Currently, it includes two models: ErrorMessage for handling errors and Search for search request and response.

## MoonHotels.Connectors.Infrastructure
This project provides access to external services such as databases, Azure containers, messaging services, etc. Currently empty, as no external dependencies have been required, but maintained to respect the project's structure.

## How to Test the Project
To test the project, you can use the Postman collection.

Make sure to start the services of the connectors (HotelLegs) and the Hub. You can access the connector directly or through the Hub for testing.

