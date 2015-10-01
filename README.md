[![Downloads](https://img.shields.io/badge/download-nuget-blue.svg)](https://www.nuget.org/packages/archimedes.localisation)


# Archimedes.Localisation
Provides a localisaiton / translation solution for .NET Applicaitons which is more flexible than the default resource satelite assemblies.


# Usage of the library

### Localisation in code
```csharp
// Simple text value
var myText = Translator.GetTranslation("dialogs.cusomter.title");


// If the text has formating place-holders - uses string.Format() syntax
var ageMessage = Translator.GetTranslation("dialogs.cusomter.age", customer.Age);
```

### In WPF / XAML
Using the Markup-Extension **LocalisationExtension**, it is very easy to localize your views.
```xaml
 <TextBlock  Text="{loc:Localisation Id=label.customer.name}" />
```


# Configuration and conventions

To just use this library in a simple setup, all you have to do is create  _*.properties files_  in the right place. The rest is automatically handlet for you:

1. In your project root, create the Folder **/Resources/messages**. 
2. In the messages folder, you can create simple text files with the *.properties extension which contain your translations.

Sample content: 
* `/Resources/messages/myApp.properties`       - Contains the default translation (usually english)
* `/Resources/messages/myApp_de.properties`    - Contains the German translation
* `/Resources/messages/myApp_fr.properties`    - Contains the French translation


## Format of the properties files

The format follows the standard properties syntax, where each line is a single property. 
Each line starts with the property key followed by the property value, separated by an '='.

myApp.properties
```properties
# English translation
label.customer.name=Name:
label.customer.age=Age:
```

myApp_de.properties
```properties
# German translation
label.customer.name=Nachname:
label.customer.age=Alter:
```


# The internals - very flexible

The core of this library is the LocalisationService, which handles all the resolving of the translations. Using the ILocalisationService.MessageSources property, you can register your own translation sources.

By default, a simple file based 
