# BeerShop

I started up to create a simple example combining AutoFac and Abstract Factories. The BeerShopService uses differnt implementations of IRepository to save data in memory or in JSON file. At the end of the day, I am not convinced if abstract factories are the best choice to solve the problem. The primary problem is to keep the constructor injection simple and avoid implementation complexity.

## When to use (Abstract) Factories

If you use AutoFac it's really simple to replace implementation by using the ContainerBuilder, but factories can help you to pass arguments and control initialization.

```csharp
public interface IPerson{
    string Name{get;};
}

public class Person : IPerson{
    public string Name {get; private set;}
    
    public Person(string name){
        Name = name;
    }
}

[...]

// not my favorite
container.Resolve<IPerson>(new TypedParamater(typeof(string), "Hans"));
                  
// like it
var factory = container.Resolve<IPersonFactory>();
var person = factory.Create("Hans");

```

The create method is allowed to resolve the instance, but hides the complexity.

```csharp
public IPerson Create(string name){
   return _lifeTimeScope.Resolve<IPerson>(new TypedParamater(typeof(string), name));
}
```
## Increasing Complexity

But what if we need different kind of persons? We have to define differnt classes implementing IPerson.

```csharp

container.RegisterType<IPerson>().Keyed<PersonCeo>(PositionInCompany.CEO));
container.RegisterType<IPerson>().Keyed<PersonEmp>(PositionInCompany.EMP));

[...]

public IPerson Create(string name, PositionInCompany position){
   return _lifeTimeScope.ResolveKeyed<IPerson>(position, new TypedParamater(typeof(string), name)); 
}
```

## Links

* [StackOverflow Discussion](http://stackoverflow.com/questions/40298802/using-autofac-to-switch-a-concrete-implemetation-of-an-abstract-factory)
* [Ctr Injection Simple](http://blog.ploeh.dk/2011/03/03/InjectionConstructorsshouldbesimple/)
* [Abstract Factories are a code smell](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=100)


