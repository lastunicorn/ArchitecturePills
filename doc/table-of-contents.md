# Software Architecture - Table of Contents

# Chapter I

(Split the Monolith)

1) **The monolith application**
   - Create a fairly simple application with a WPF GUI, some business logic and data loaded from a json file. All of these responsibilities are placed in the one and only component: the monolith.
2) **Introducing the Domain (Critical Business Logic)**
   - Extract into a different component the algorithm that converts the amount of money based on the inflation rates: the `Calculator` class.
   - This isolates the most important logic (the critical business logic) from the rest of the application.
3) **Introducing the Use Cases (The Application Component)**
   - Identify the actions that the user can perform on the system. The logic that handles each action can be extracted in a different class
   - Then, all these use case classes, can be extracted in a different assembly: the Application component.
4) **Extract the Data Access**
   - The user data is usually stored in a persistent location like a file on disk, a database, sent to a web service, etc.
   - Let's create a dedicated component to contain the details of accessing that storage and hide them from the rest of the application. In this way it can be replaced with minimum effort when the need arise.
   - Also, from Hexagonal Architecture, I adopted the idea, of having a Port component to contain the interfaces and and Adaptor (the Data Access component) to contain the actual implementation for the port.
5) **Separate the Presentation and the Bootstrapper**
   - The Presentation component contains all the details of how the internal state and data are displayed to the user, including Views, Styles, View Models, Value Converters (used for displaying), etc.
   - The other important aspect that the Presentation component is doing is being sensitive for actions provided by the user. Each actions, then, is calling a dedicated Use Case implementation from the Application component.

# Chapter II

(General purpose techniques and improvements)

1) **Introducing Autofac**
   - Autofac provides a cleaner way of resolving class dependencies.
2) **Introducing MediatR**
   - MediatR provides a cleaner way of executing the use case implementations.
3) **Execution context the RequestBus**
   - The RequestBus will wrap the usage of the MediatR and, using also the Autofac, will provide a different execution context for each use case.
1) **State data** 
   - This is temporary data that will be forgotten when the application is restarted.
   - Where do we keep the state?
     - Should it be kept in the GUI controls?... Maybe in the View Model from behind the GUI?... What about a completely different class? But, what is that class?
2) **Introducing the Event Bus**
   - Sometimes, the user may do some actions in one part of the GUI, but other parts of the GUI must also react to that change.
   - My mechanism of choice for enabling such kind of communication is the Event Bus. It is nothing else but the pub-sub pattern implementation.
3) **Error handling**
   - If the Use Cases cannot perform their happy flow, for any reason, they will throw an exception. These exceptions, it si useful, to be caught in a centralized way and displayed to the user.

# Chapter III

(Additional improvements)

1) **Later user interaction**
   - What if the application, during the execution of a use case, needs to ask something to the user? A confirmation, for example or additional data needed for the computation?
   - This can be implemented as a secondary port/adapter.
2) **Config and user settings**
   - Another port, another adapter.