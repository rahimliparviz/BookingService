# Booking Service API
 
---

##  Tech Stack
- .NET 8 Web API  
- xUnit for testing  
- In-memory data repository  

---

##  How to Run
1. Clone this repository:
   ```bash
   git clone https://github.com/rahimliparviz/BookingService.git
2.  cd BookingService
3.  dotnet run --project BookingService.API
   
### Access Swagger UI
http://localhost:5184/swagger

## How to Test
Run "dotnet test" inside the main "BookingService" directory

## API Endpoint

### Example
Get: /api/available-homes?StartDate=2025-12-3&EndDate=2025-12-5

### Response
[
  {
    "homeId": 9,
    "homeName": "Home Nine",
    "availableSlots": [
      "2025-12-01T00:00:00",
      "2025-12-02T00:00:00",
      "2025-12-03T00:00:00",
      "2025-12-04T00:00:00",
      "2025-12-05T00:00:00",
      "2025-12-06T00:00:00",
      "2025-12-07T00:00:00",
      "2025-12-08T00:00:00",
      "2025-12-09T00:00:00",
      "2025-12-10T00:00:00"
    ]
  }
]

## Architecture and Filtering Logic
The project follows Clean Architecture principles with a layered structure: API, Application, Domain, Infrastructure, plus a dedicated IntegrationTests project.

### API Layer: 
Uses the Result pattern to keep controller code clean and readable.

### Application Layer: 
Implements the Specification pattern to make filtering operations more dynamic. Business rules are passed into repositories via specifications, ensuring the service layer only receives already filtered data.

Two specifications were created:

AvailableHomeInRangeSpecification – Returns homes that are available for the given startDate and endDate. Filtering logic first generates a collection of dates within the requested range and converts it into a HashSet. Unlike a List, a HashSet is backed by a Hash Table, which provides better performance for membership checks during filtering.

HomeWithOlderThanDateSlotSpecification – Identifies homes that only have outdated availability slots, used for background cleanup.

Filtering Process:
The system iterates through all active homes in memory and checks whether each home has available slots covering the requested date range. Matching homes are returned as a List.
To prevent blocking the main API thread under heavy load or large data sets, the filtering process is executed asynchronously using Task.Run, offloading the work to the Thread Pool.

### Infrastructure Layer:
Includes a DataCleanUpBackgroundService, which runs once per day. It removes outdated availability slots based on HomeWithOlderThanDateSlotSpecification. If a home has no active slots left, the home itself is also removed from memory.
Also used List<Home> for in-memory storage instead of Dictionary<string, List<string>>, because in the project we do not do key-based lookups. Filtering is done efficiently via LINQ, which keeps the code simpler and flexible.

### Domain Layer:
Contains the core entity Home with its slots.



