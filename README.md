# Message Manager Service

## Description

ASP .Net Core 5 Restfull API service. 
Used InMemoryDatabase with Dbcontext.
Dependecy Injection + AutoMapper.

Models Layer for inernal use.
DTOs Layer for Internal \ External communication.

Dbcontext as a preperation layer for SQL Database, communicating with Repository and Models.

Repository communicating with upper layer of Controller which acts as top layer communcation request controller.

Using xUnit framework for UnitTesting.

* Swagger available for easy GUI testing and api documentation. 
    * To use swagger:
        * Open 'launchSettings.json'  
        * Replace 
        ```
        "launchUrl": "api/MessagesManager"
        ```
        With 
        ```
        "launchUrl": "swagger"
        ```
        
* For further testing, Available is a Postman Json file:
```
MessageManager.postman_collection.json
```
* xUnit Testing (Details below, under 'Testing program')




## Getting Started

### Dependencies

* Windows 10
* Visual Studio 2022
* .Net Core 5 Framework
* Nuget Packages:
    * AutoMapper.Extensions.Microsoft.DependencyInjection
    * Microsoft.EntityFrameworkCore
    * Microsoft.EntityFrameworkCore.InMemory
    * Swashbuckle.AspNetCore
    * FluentAssertions (xUnit Project)
 

### Installing

* https://github.com/IdanShnitzki/MessageManager

### Executing program
* Run Visual Studio.
* Build.
* Run.


### Testing program
* Use the project 'MessageManagerUnitTesting' under the Solution.



## Version History

* 0.10
    * Added xUnit testing project
    * See [8cf75dc8b26ac1ef4d78fd69406463d77a84df38]()
* 0.9
    * Added Controller
    * See [3baeb365db7c540729f9fbe5fc07088fe6ef27fe]()
* 0.9
    * Added Controller
    * See [3baeb365db7c540729f9fbe5fc07088fe6ef27fe]()
* 0.8
    * Rearrange Model folder stracture
    * See [260466b7ebf6825b80747c08e05c56d4ca416a26]()
* 0.7
    * Added Dtos
    * See [051dcdc7d97d77959c4a40156efa35574d3b58f9]()
* 0.6
    * Added PrepDb for seeding initial data
    * See [612054f75a1deeb4804d08376704119fb1e09586]()
* 0.5
    * Added repository
    * See [02ef7f6de47975fba4748ced956db43c484ec2b7]()
* 0.4
    * added DbContext - InMemoryDatabase
    * See [062fc7f99e7df8bb303223cc14173221875f7bd2]()
* 0.3
    * Added Message Model
    * See [44be532d12c69c0140b88402ce40ecf89979ac32]()
* 0.2
    * Add project files
    * See [6b94fe7548538fd43f96834cc92b85055c36994b]()
* 0.1
    * Initial Release
