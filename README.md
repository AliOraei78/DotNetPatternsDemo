# Day 1 - AdvancedDotNetPatternsDemo
An educational sample project demonstrating advanced .NET patterns, including:

- Design Patterns (GoF)
- Dependency Injection Patterns
- CQRS with MediatR
- Domain Events
- Outbox Pattern

Day 1 – Foundations and Setup
- Created Solution using Clean Architecture
- Installed MediatR and Entity Framework Core
- Established initial folder structure
- GitHub repository preparation

## Day 2 – SOLID Principles and Introduction to GoF Design Patterns
### SOLID Principles

- **S**: Single Responsibility Principle – One class, one responsibility
- **O**: Open/Closed Principle – Open for extension, closed for modification
- **L**: Liskov Substitution Principle – Substitutability without altering expected behavior
- **I**: Interface Segregation Principle – Small, focused interfaces
- **D**: Dependency Inversion Principle – Depend on abstractions, not concretions


### GoF Pattern Categories (23 Patterns)
1. **Creational** (5 patterns): Abstract Factory, Builder, Factory Method, Prototype, Singleton
2. **Structural** (7 patterns) Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy
3. **Behavioral** (11 patterns) Chain of Responsibility, Command, Iterator, Mediator, Observer, State, Strategy, etc.

These principles form the foundation for implementing CQRS, Domain Events, and more advanced architectural patterns in the project.

## Day 3 – Creational Patterns (Part 1)

### 1. Singleton
- Ensures a single instance with global access
- Modern implementation: Lazy<T> (thread-safe)
- Example: LoggerSingleton

### 2. Factory Method
- Defines an interface for creating an object, but subclasses decide what to instantiate
- Example: NotificationFactory for Email and SMS

### 3. Abstract Factory
- Creates a family of related objects without depending on concrete classes
- Example: UiFactory for Light and Dark themes

All examples are located in the Application/Patterns folder.

## Day 4 – Creational Patterns (Part 2)

### 1. Builder Pattern
- Step-by-step construction of complex objects with a Fluent Interface
- Example: OrderBuilder for creating orders with validation
- Benefits: readability, control over optional parameters, avoids large constructors

### 2. Prototype Pattern
- Creating new objects by copying (cloning) an existing instance
- Example: OrderTemplate with Deep Copy (manual list copying)
- Benefits: fast creation in expensive scenarios, preserves initial state

All code is located in the Application/Patterns folder.

## Day 5 – Structural Patterns (Part 1)

### 1. Adapter Pattern
- Converts an incompatible interface into a compatible one
- Example: LegacyPaymentAdapter for integrating a legacy gateway

### 2. Bridge Pattern
- Separates abstraction from implementation for independent changes
- Example: RemoteControl with TV and Radio devices

### 3. Composite Pattern
- Tree structure (part-whole) with uniform behavior for leaves and composites
- Example: MenuComponent (MenuGroup and MenuItem)

All implementations are located in the Application/Patterns folder.

## Day 6 – Structural Patterns (Part 2) – Completing the Structural Category

### 1. Decorator Pattern
- Dynamically adds responsibilities without modifying the original class
- Example: Logging and Timestamp for the notification service

### 2. Facade Pattern
- Simplified interface to a complex subsystem
- Example: OrderProcessingFacade for full order processing

### 3. Flyweight Pattern
- Shares small, repetitive objects to save memory
- Example: ProductType with a Flyweight factory

### 4. Proxy Pattern
- Controls access, caching, lazy loading, etc.
- Example: CachingProxy for an expensive service

All implementations are located in the Application/Patterns folder.

## Day 7 – Behavioral Patterns (Part 1)

### 1. Chain of Responsibility
- Processing requests through a chain of handlers
- Example: Leave approval based on management level

### 2. Command
- Encapsulating a request as a standalone object (foundation of CQRS)
- Example: Light on/off controller with undo functionality

### 3. Interpreter
- Interpreting the grammar of a simple language
- Example: Evaluating a mathematical expression (addition, subtraction, variables)

### 4. Iterator
- Sequential access to elements of a collection without exposing its internal structure
- Example: Depth-First traversal of a tree

All implementations are located in the `Application/Patterns` folder.

## Day 8 – Behavioral Patterns (Part 2)

### 1. Mediator
- Centralizes communication between objects
- Example: `ChatRoom` as a messaging mediator

### 2. Memento
- Saves and restores state without breaking encapsulation
- Example: `TextEditor` with undo capability

### 3. Observer
- Automatically notifies dependents of state changes
- Example: `NewsPublisher` and its subscribers

### 4. State
- Changes an object's behavior based on its internal state
- Example: `VendingMachine` with multiple states

All implementations are located in the `Application/Patterns` folder.

## Day 9 – Behavioral Patterns (Part 3) – Completing the GoF Patterns

### 1. Strategy
- Swapping algorithms at runtime
- Example: Different discount strategies for an order

### 2. Template Method
- Defining the skeleton of an algorithm while allowing step customization
- Example: Online order processing workflow

### 3. Visitor
- Adding new operations without modifying existing classes
- Example: Reporting and total calculation for physical and digital products

**End of GoF Design Patterns section (23 patterns)**
All implementations are located in the `Application/Patterns` folder.

## Day 10 – Dependency Injection Patterns

### Main DI Patterns in .NET
- **Constructor Injection** → Best practice and recommended (explicit, testable)
- **Property Injection** → Only for optional dependencies (less recommended)
- **Method Injection** → Useful for handlers and middleware (`[FromServices]`)
- **Ambient Context** → Use with caution; often an anti-pattern (alternative: Scoped DI)

### Implementation in a Project
- Register services in `Program.cs` using `AddScoped` / `AddSingleton` / `AddTransient`
- Example: `OrderService` using Constructor Injection with `IOrderRepository` and `ILoggerService`

These patterns form the foundation for **MediatR** and **CQRS** in the upcoming days.

## Day 11 – CQRS with MediatR

- New domain: `TodoItem` (task management)
- Separation of Command (write side) and Query (read side)
- Using MediatR to send Commands and Queries
- Commands: `CreateTodoCommand`, `CompleteTodoCommand`
- Queries: `GetTodoByIdQuery`, `GetAllTodosQuery`
- Separate repositories for write and read (currently InMemory)
- Registering MediatR and testing with Minimal API endpoints

## Day 12 – Domain Events in DDD + Integration with CQRS

- Define Domain Events (`TodoCreatedEvent`, `TodoCompletedEvent`)
- Collect events inside the Aggregate (`TodoItem`)
- Publish events using MediatR `IPublisher` in the Command Handler
- Sample handlers for logging and side-effects
- Separate domain logic from side-effects → ensures **Single Responsibility** and **loose coupling**

## Day 13 – Outbox Pattern

- `OutboxMessage` table for atomic event storage
- Persisting domain events to the Outbox within the Command Handler
- Background processing with Hangfire (recurring job + retry mechanism)
- Handling distributed transactions without 2PC (Two-Phase Commit)
- Hangfire dashboard for monitoring and manual retry
