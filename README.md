# Tasks #
- [x] 1) Implement rest of Company controller functions, all the way down to data access layer

- [x] 2) Change all Company controller functions to be asynchronous

- [x] 3) Create new repository to get and save employee information with the following data model properties:

* string SiteId,
* string CompanyCode,
* string EmployeeCode,
* string EmployeeName,
* string Occupation,
* string EmployeeStatus,
* string EmailAddress,
* string Phone,
* DateTime LastModified

- [x] 4) Create employee controller to get the following properties for client side:

* string EmployeeCode,
* string EmployeeName,
* string CompanyName,
* string OccupationName,
* string EmployeeStatus,
* string EmailAddress,
* string PhoneNumber,
* string LastModifiedDateTime

- [x] 5) Add logger to solution and add proper error handling

## Project Notes & Remarks ##
### Ninject Removed ###
- It has some kind of inMemoryDatabase error because it can't handle singleton.
- Ninject removed for the sake of code clarity

### New Dependency Injector ###
[![NuGet](https://img.shields.io/nuget/v/SimpleInjector.svg)](https://www.nuget.org/packages/simpleinjector) SimpleInjector implemented 

### Work Item 4 ###
- Employee object is mapped with Company Object to get CompanyName

### Filters ###
ActivityHandlerFilter and ExceptionHandlerFilter are implemented to catch records.
All record queries and Exception saving in the logger db.

### LoggerDatabase Implemented ###
I decided to implement separate inMemoryDatabase for logging. Because we don't need siteId and companyId for a key, instead we only need Guid.


