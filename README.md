# Orchestrated Sagas with .NET and Azure Functions

This repository provides a comprehensive example of how to implement a Saga pattern using Azure Functions and RabbitMQ in a .NET application. It focuses on orchestrating distributed transactions across multiple microservices, ensuring data consistency through the asynchronous execution of compensating actions.

The code demonstrates the use of Azure Functions as the presentation layer for handling HTTP requests and triggering various steps of a Saga, such as booking a car, hotel, and flight across different services. It also showcases the integration of RabbitMQ for reliable message passing between services, allowing for the coordination of complex workflows.

This implementation serves as a practical guide for .NET developers looking to incorporate robust, distributed transaction management into their microservices architecture, leveraging Azure Functions for orchestration and RabbitMQ for seamless service communication.
