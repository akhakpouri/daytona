[![Build & Pack](https://github.com/akhakpouri/daytona/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/akhakpouri/daytona/actions/workflows/ci.yml)
# Daytona

This is a simple little library built to solve a complex problem - wrap your queries inside of a single `unit-of-work` to use accordingly.

### How to get started?

First, configure the `DbContext` using a desired database framework. Make sure you're pasing through the `DbContextOptions` when creating your model.

```csharp
public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }
    public YourDbContext() { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ///... modelbuilder configs
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
```
Larger appliacations will require multiple type configurations, and as the scope of the application grows, the developer will have to remember to register all new type configurations. A new extension methid, `ApplyConfigurationsFromAssembly`, was introduced in `.net-standard-2.2` which scans a given assembly for all types and implement `IEntityTypeConifugration`, and registers each one automatically.

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    ///.. optionbuildr configs
    base.OnConfiguring(optionsBuilder);
}
```

When creating an entity, make sure you either inherit from `BaseEntity` or `BaseCompleteEntity` depending on which properties you like to inherit.

Create a project level `IYourProjectUnitOfWork` interface inheriting `IUnitOfWork<YourDbContext>` passing in the `DbContext` created in the first step.

```csharp
public interface IYourUnitOfWork : IUnitOfWork<YourDbContext>
{
    IRepository<MyEntity> MyRepository { get; }
    IRepository<FooEntity> FooRepository { get; }
    IRepository<BarEntity> BarRepository { get; }
}

public class YourUnitOfWork : UnitOfWork<YourDbContext>, IYourUnitOfWork
{
    public IRepository<MyEntity> MyRepository => new Repository<MyEntity>(Context);
    public IRepository<FooEntity> FooRepository => new Repository<FooEntity>(Context);
    public IRepository<BarEntity> BarRepository => new Repository<BarEntity>(Context);
    
    public YourUnitOfWork(YourDbContext context) : base(context) { }
}
```

When creating the entity type configuration, you must choose between `BaseNpsqlEnttityConfiguration` or `BaseSqlEntityConfiguration` depending on your database's flavor. You can `override` the `Configure` method to include your additional property/column configurations.

### How do I use it in the manager layer?

Daytona's EfCore is more than just a data-layer extension. It can also provide you with an robust library to use the `UnitOfWork` as easily as possible.

First, Create an interface for your manager. Feel free to make it generic, and inherit it from `IManager<IYourUnitOfWork, TDto>`; but, you have to make sure `TDto` is inheriting from the `BaseDto`. Something like this:

```csharp
public interface IYourManager<TDto> : IManager<IYourUnitOfWork, TDto> where TDto : BaseDto {}
```
After this, you can choose to create more specific manager level interfaces by inheriting your wrapping interface, or continue to use this interface for all CRUD operations.

#### Let's talk Implementation!

When implementing your manager, you must inherit from the base abstract `Manager` class as well as the interface defined above.

`IMapper` and `IUnitOfWork` are already registredd into the abstract manager's contructor; therefore, you don't have to worry about creating `private readonly` variables for those.

#### Eager Loading
Ef-Core suppors eager loading of related entities, using `Include()` iextension method and projection query. In addition, you can also use `ThenInclude()` method to load multiple levels of relation entities.

Look at the following example:

```csharp
var x  = (await _unitOfWork.FooRepository.GetAll(a => true, a => a.Include(a => a.Bar))).FirstOrDefaultAsync();
```

In the above example, `.Include(a => a.Bar)` passes the lambda expression `a => a.Bar` to specify a reference property to loadedwith `Foo` entity data from the database in a single SQL query. The above query executes the following SQL query in the database

```sql
SELECT TOP(1) [a].*
FROM [Foo] AS [a]
LEFT JOIN [Bar] AS [a.Bar] ON [a].[BarId] = [s.Bar].[Id]
WHERE 1 = 1 
```

### Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, add [`akhakpouri`](https://nuget.pkg.github.com/akhakpouri/index.json) as a source to your nuget configuration. Lastly, install [Daytona](https://github.com/akhakpouri/daytona/pkgs/nuget/Daytona) from the package manager console:

```
PM> Install-Package Daytona --source "akhakpouri"
```
Or from the .NET CLI as:
```
dotnet add package Daytona --source "akhakpouri"
```
