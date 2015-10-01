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
