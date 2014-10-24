# Spectrum

## What is it?

Spectrum is a library to manipulate colours in the .NET framework, specifically in the RGB, HSL and HSV colour spaces.

It's currently not designed to the be all and end all in terms of colour but more to assist developers in building harmonious colour schemes for their applications.

It exists as two parts:

 1. **Spectrum** is the core library that contains the classes you'd expect in a library such as this. It's a portable class library covering all the major platforms (math is very portable).
 2. **Spectrum.Universal** is the interesting part and is where I hope to extend most of all, it's a playground for how we can use colour space calculations in our apps. It's portable across Windows 8 and Windows Phone 8.1.

## How does it work?

The core Spectrum library has only three classes `Color.RGB`, `Color.HSL` and `Color.HSV`, these naturally have methods to convert between themselves.

``` csharp
var accent = new Color.RGB(240, 150, 9);

var hsl = accent.ToHSL();

Assert.AreEqual(new Color.HSL(36.6, 0.928, 0.488), hsl);
```

The `Color.HSL` class has number of helper methods to create other related colours.

``` csharp
var hsl = new Color.HSL(90, 0.5, 0.5);

var compliment = hsl.ShiftHue(180.0d);

var analogous = hsl.GetAnalogous();
```

It gets more interesting when we start to use **Spectrum.Universal**. This contains a few Xaml Value Converters that can shift between `Windows.UI.Color` instances for us.

Given resource declarations like the following:

``` xml
<converters:ComplimentConverter x:Key="Compliment" />
<converters:ShiftLightnessConverter x:Key="ShiftLightness" />
<converters:ShiftHueConverter x:Key="ShiftHue" />
```

and our base accent colour:

``` xml
<Color x:Key="AccentColor">#FF0088D1</Color>
<SolidColorBrush x:Key="AccentBrush" Color="{StaticResource AccentColor}"/>
```

We can create lighter and darker versions of that same colour using

``` xml
<SolidColorBrush x:Key="LightenedAccentBrush" 
	Color="{Binding 
		Source={StaticResource AccentColor}, 
		Converter={StaticResource ShiftLightness}, 
		ConverterParameter=0.3}"/>

<SolidColorBrush x:Key="DarkenedAccentBrush"
	Color="{Binding 
		Source={StaticResource AccentColor}, 
		Converter={StaticResource ShiftLightness}, 
		ConverterParameter=-0.3}"/>
```

as well as create a minor accent colour

``` xml
<SolidColorBrush x:Key="SecondaryAccentBrush" 
	Color="{Binding 
		Source={StaticResource AccentColor}, 
		Converter={StaticResource ShiftHue}, 
		ConverterParameter=-120}"/>
```

We can now manage an app's full palatte of colour resources by tweaking a few base colours.

## Where I can get this?

Source is obviously here, and is also on [nuget](http://www.nuget.org/packages/Spectrum.Universal/).


