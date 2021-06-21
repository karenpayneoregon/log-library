# About 

Provides easy to use logging to files. See test method for usage

Write a captured run time exception to `GeneralUnhandledException.txt` in the same folder as the main assembly. Code sample comes from LoggingUnitTestProject.

:heavy_check_mark: If log file does not exist it will created otherwise appended too


```csharp
try
{
    NullException();
}
catch (Exception exception1)
{
    ApplicationLog.Instance.WriteException(exception1, "Comment");
}
```
</br>

## Write text only

The following method writes text (any size) to `GeneralUnhandledException.txt`. See unit test `WriteTextToMainLog`

```csharp
ApplicationLog.Instance.WriteInformation("some text");
```

## Write to multiple files

The following will create a new log file for each call. First call creates Log_1.txt, second Log_2.txt etc.

```csharp
[TestMethod]
[TestTraits(Trait.IncrementalLogging)]
public void IncrementLogging()
{
    // write a simple string to a file
    ApplicationLog.Instance.Write("Some text 1");
    
    // write a paragraph of text to a file
    ApplicationLog.Instance.Write(LoremIpsumParagraph());
    
    Assert.IsTrue(File.Exists("Log_1.txt") && File.Exists("Log_1.txt"));
}
```
---

# Comments

The following is set for not creating language folders under bin\Debug

```xml
<SatelliteResourceLanguages>en;de;pt</SatelliteResourceLanguages>
```

---