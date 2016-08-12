# UglyMapper
I have a hatred of auto mapping black magic to do simply property mapping, so I find myself recreating this code over and over, so decided finally to commit the latest version of to GitHub, and share it, if it saves me a few hours in a couple of projects time that's the point, if it saves you a few hours too then that's a big bonus!

It's safe for IOC, It's safe for refactoring, and it's pretty trivial.

It's really simple to use, in the constructor of the mapping class inherit BaseMapperConfiguration specifying the To and From types, then use the set the commands

Map(From => From.Property).To(To => (To, FromProp) => { To.PropertyToMapTo = FromProp; })

This will instantiate the object using a parameterless constructor.

I will probably extend this further, when I need to use constructors with parameters, or to nest configurations.