# UglyMapper

## Description 
I have a hatred of auto mapping black magic to do simple property mapping, however I like the configurable mapping of much more complex frameworks, so I find myself recreating this code over and over, so decided finally to commit the latest version of to GitHub, and share it, if it saves me a few hours in a couple of projects time that's the point, if it saves you a few hours too then that's a big bonus!

It's safe for IOC, It's safe for refactoring, and it's pretty trivial.

It's really simple to use, in the constructor of the mapping class inherit BaseMapperConfiguration specifying the To and From types, then use the set the commands

Map(From => From.Property).To(To => (To, FromProp) => { To.PropertyToMapTo = FromProp; })

This will instantiate the object using a parameterless constructor.

I will probably extend this further, when I need to use constructors with parameters, or to nest configurations.

##Factory Class
When instantiated the factory class needs to be provided with a list of mapping configurations, from there the mapping class can be used to to map, when called it will map, if it knows how to, otherwise an exception of type `NoMappingExistsException` will be thrown.
When using the factory class it's very easy to use nested mapping classes.

##Examples

###A simple mapping class:
```C#
public class Class1ToClass2Mapper : UglyMapper.BaseMapperConfiguration<Class1, Class2>
{
	public Class1ToClass2Mapper() {
		Map(x.FromProperty1).To((y, z) => y.ToProperty1 = z);
		Map(x.FromProperty2).To((y, z) => y.ToProperty2 = z);
	}
}
```

###Creating using a constructor
```C#
public class Class1ToClass2Mapper : UglyMapper.BaseMapperConfiguration<Class1, Class2>
{
	public Class1ToClass2Mapper() {
		ConstructBy(x => new Class2(x.FromProperty1, x.FromProperty2))
	}
}
```

###Nest Mapping classes using the factory (The initial mapping class must be called via the factory, or MappingFactory() will be null)
```C#
public class Class1ToClass2Mapper : UglyMapper.BaseMapperConfiguration<Class1, Class2>
{
	public Class1ToClass2Mapper() {
		Map(x => x).To((y, z) => y.HoldsClass3 = MappingFactory().Map<Class1, Class3>(z));
	}
}
```