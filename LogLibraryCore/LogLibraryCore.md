# Log Library &mdash; .NET 5

![img](assets/LogoVersion.png)

## Write Exception 

Write date/time, exception text including inner exception to `GeneralUnhandledException.txt` in the main folder of the application. The following can be executed in the unit test project, test name `UnhandledException`.

:heavy_check_mark: first parameter is an exception

:heavy_check_mark: second parameter is optional text to write along with the exception.

</br>

```charp
try
{
    NullException();
}
catch (Exception exception1)
{
    ApplicationLog.Instance.WriteException(exception1, "Comment");
}
```

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

## Sample log files

Samples are located under SampleLogs folder in the unit test project.

## Contents

- [ApplicationLog](#T-ServiceLogLibrary-Classes-ApplicationLog 'ServiceLogLibrary.Classes.ApplicationLog')
  - [#ctor()](#M-ServiceLogLibrary-Classes-ApplicationLog-#ctor 'ServiceLogLibrary.Classes.ApplicationLog.#ctor')
  - [Instance](#P-ServiceLogLibrary-Classes-ApplicationLog-Instance 'ServiceLogLibrary.Classes.ApplicationLog.Instance')
  - [Write(text)](#M-ServiceLogLibrary-Classes-ApplicationLog-Write-System-String- 'ServiceLogLibrary.Classes.ApplicationLog.Write(System.String)')
  - [WriteException(exception,text)](#M-ServiceLogLibrary-Classes-ApplicationLog-WriteException-System-Exception,System-String- 'ServiceLogLibrary.Classes.ApplicationLog.WriteException(System.Exception,System.String)')
  - [WriteInformation(text)](#M-ServiceLogLibrary-Classes-ApplicationLog-WriteInformation-System-String- 'ServiceLogLibrary.Classes.ApplicationLog.WriteInformation(System.String)')
- [ExceptionExtensions](#T-ServiceLogLibrary-Classes-ExceptionExtensions 'ServiceLogLibrary.Classes.ExceptionExtensions')
  - [GetExceptionMessages(e,result)](#M-ServiceLogLibrary-Classes-ExceptionExtensions-GetExceptionMessages-System-Exception,System-String- 'ServiceLogLibrary.Classes.ExceptionExtensions.GetExceptionMessages(System.Exception,System.String)')
- [ExceptionLogType](#T-ServiceLogLibrary-Classes-ExceptionLogType 'ServiceLogLibrary.Classes.ExceptionLogType')
  - [General](#F-ServiceLogLibrary-Classes-ExceptionLogType-General 'ServiceLogLibrary.Classes.ExceptionLogType.General')
  - [PlaceHolder](#F-ServiceLogLibrary-Classes-ExceptionLogType-PlaceHolder 'ServiceLogLibrary.Classes.ExceptionLogType.PlaceHolder')
  - [Unknown](#F-ServiceLogLibrary-Classes-ExceptionLogType-Unknown 'ServiceLogLibrary.Classes.ExceptionLogType.Unknown')
- [Exceptions](#T-ServiceLogLibrary-Classes-Exceptions 'ServiceLogLibrary.Classes.Exceptions')
  - [FileName](#F-ServiceLogLibrary-Classes-Exceptions-FileName 'ServiceLogLibrary.Classes.Exceptions.FileName')
  - [GetStackTraceLines(stackTrace)](#M-ServiceLogLibrary-Classes-Exceptions-GetStackTraceLines-System-String- 'ServiceLogLibrary.Classes.Exceptions.GetStackTraceLines(System.String)')
  - [GetUserStackTraceLines(fullStackTrace)](#M-ServiceLogLibrary-Classes-Exceptions-GetUserStackTraceLines-System-String- 'ServiceLogLibrary.Classes.Exceptions.GetUserStackTraceLines(System.String)')
  - [ToLogString(exception,environmentStackTrace)](#M-ServiceLogLibrary-Classes-Exceptions-ToLogString-System-Exception,System-String- 'ServiceLogLibrary.Classes.Exceptions.ToLogString(System.Exception,System.String)')
  - [Write(exception,exceptionLogType,text)](#M-ServiceLogLibrary-Classes-Exceptions-Write-System-Exception,ServiceLogLibrary-Classes-ExceptionLogType,System-String- 'ServiceLogLibrary.Classes.Exceptions.Write(System.Exception,ServiceLogLibrary.Classes.ExceptionLogType,System.String)')
  - [Write(text)](#M-ServiceLogLibrary-Classes-Exceptions-Write-System-String- 'ServiceLogLibrary.Classes.Exceptions.Write(System.String)')
- [Operations](#T-ServiceLogLibrary-Classes-Operations 'ServiceLogLibrary.Classes.Operations')
  - [_pattern](#F-ServiceLogLibrary-Classes-Operations-_pattern 'ServiceLogLibrary.Classes.Operations._pattern')
  - [LogFiles](#P-ServiceLogLibrary-Classes-Operations-LogFiles 'ServiceLogLibrary.Classes.Operations.LogFiles')
  - [UniqueFileNameByTicks](#P-ServiceLogLibrary-Classes-Operations-UniqueFileNameByTicks 'ServiceLogLibrary.Classes.Operations.UniqueFileNameByTicks')
  - [_baseFileName](#P-ServiceLogLibrary-Classes-Operations-_baseFileName 'ServiceLogLibrary.Classes.Operations._baseFileName')
  - [GenerateFileNameByDateAndGuid()](#M-ServiceLogLibrary-Classes-Operations-GenerateFileNameByDateAndGuid 'ServiceLogLibrary.Classes.Operations.GenerateFileNameByDateAndGuid')
  - [GetLast()](#M-ServiceLogLibrary-Classes-Operations-GetLast 'ServiceLogLibrary.Classes.Operations.GetLast')
  - [GetNextFilename(pattern)](#M-ServiceLogLibrary-Classes-Operations-GetNextFilename-System-String- 'ServiceLogLibrary.Classes.Operations.GetNextFilename(System.String)')
  - [GetNumbers(input)](#M-ServiceLogLibrary-Classes-Operations-GetNumbers-System-String- 'ServiceLogLibrary.Classes.Operations.GetNumbers(System.String)')
  - [HasAnyFiles()](#M-ServiceLogLibrary-Classes-Operations-HasAnyFiles 'ServiceLogLibrary.Classes.Operations.HasAnyFiles')
  - [NextAvailableFilename(path)](#M-ServiceLogLibrary-Classes-Operations-NextAvailableFilename-System-String- 'ServiceLogLibrary.Classes.Operations.NextAvailableFilename(System.String)')
  - [NextFileName()](#M-ServiceLogLibrary-Classes-Operations-NextFileName 'ServiceLogLibrary.Classes.Operations.NextFileName')
  - [RemoveAllFiles()](#M-ServiceLogLibrary-Classes-Operations-RemoveAllFiles 'ServiceLogLibrary.Classes.Operations.RemoveAllFiles')
  - [WriteInformation(text)](#M-ServiceLogLibrary-Classes-Operations-WriteInformation-System-String- 'ServiceLogLibrary.Classes.Operations.WriteInformation(System.String)')

<a name='T-ServiceLogLibrary-Classes-ApplicationLog'></a>
## ApplicationLog `type`

##### Namespace

ServiceLogLibrary.Classes

##### Summary

Responsible for log operations

##### Remarks

Thread safe singleton class

<a name='M-ServiceLogLibrary-Classes-ApplicationLog-#ctor'></a>
### #ctor() `constructor`

##### Summary

Called first time accessing this class, never again

##### Parameters

This constructor has no parameters.

<a name='P-ServiceLogLibrary-Classes-ApplicationLog-Instance'></a>
### Instance `property`

##### Summary

Access point to methods and properties

<a name='M-ServiceLogLibrary-Classes-ApplicationLog-Write-System-String-'></a>
### Write(text) `method`

##### Summary

Write string to a incrementing log file e.g. Log_1.txt, Log_2.txt where
an algorithm determines the file name in [Operations](#T-ServiceLogLibrary-Classes-Operations 'ServiceLogLibrary.Classes.Operations')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text to write which can be a one liner or several lines |

<a name='M-ServiceLogLibrary-Classes-ApplicationLog-WriteException-System-Exception,System-String-'></a>
### WriteException(exception,text) `method`

##### Summary

Write to any exception to log file with optional message. Exception file name is
determined by [ExceptionLogType](#T-ServiceLogLibrary-Classes-ExceptionLogType 'ServiceLogLibrary.Classes.ExceptionLogType')

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | [Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message to tag on to exception log |

<a name='M-ServiceLogLibrary-Classes-ApplicationLog-WriteInformation-System-String-'></a>
### WriteInformation(text) `method`

##### Summary

Write to main log file without an exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-ServiceLogLibrary-Classes-ExceptionExtensions'></a>
## ExceptionExtensions `type`

##### Namespace

ServiceLogLibrary.Classes

##### Summary

Helper methods for logging exceptions

<a name='M-ServiceLogLibrary-Classes-ExceptionExtensions-GetExceptionMessages-System-Exception,System-String-'></a>
### GetExceptionMessages(e,result) `method`

##### Summary

Get InnerException if there is one as text.

##### Returns

Inner exception text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| e | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| result | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-ServiceLogLibrary-Classes-ExceptionLogType'></a>
## ExceptionLogType `type`

##### Namespace

ServiceLogLibrary.Classes

##### Summary

Indicates type of exception which dictates which file to write too.

##### Remarks

Add or more members as needed except for General

<a name='F-ServiceLogLibrary-Classes-ExceptionLogType-General'></a>
### General `constants`

##### Summary

Broad stroke exception type

<a name='F-ServiceLogLibrary-Classes-ExceptionLogType-PlaceHolder'></a>
### PlaceHolder `constants`

##### Summary

Add more as needed

<a name='F-ServiceLogLibrary-Classes-ExceptionLogType-Unknown'></a>
### Unknown `constants`

##### Summary

Unknown exception

<a name='T-ServiceLogLibrary-Classes-Exceptions'></a>
## Exceptions `type`

##### Namespace

ServiceLogLibrary.Classes

##### Summary

Provides writing run time exceptions to a text file

##### Remarks

What's here works while there will be many modifications later on.

<a name='F-ServiceLogLibrary-Classes-Exceptions-FileName'></a>
### FileName `constants`

##### Summary

Write to file name

<a name='M-ServiceLogLibrary-Classes-Exceptions-GetStackTraceLines-System-String-'></a>
### GetStackTraceLines(stackTrace) `method`

##### Summary

Gets a list of stack frame lines, as strings.

##### Returns

Stack trace lines

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stackTrace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Stack trace string. |

<a name='M-ServiceLogLibrary-Classes-Exceptions-GetUserStackTraceLines-System-String-'></a>
### GetUserStackTraceLines(fullStackTrace) `method`

##### Summary

Gets a list of stack frame lines, as strings, only including those for which line number is known.

##### Returns

Stack trace lines

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fullStackTrace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Full stack trace, including external code. |

<a name='M-ServiceLogLibrary-Classes-Exceptions-ToLogString-System-Exception,System-String-'></a>
### ToLogString(exception,environmentStackTrace) `method`

##### Summary

Provides full stack trace for the exception that occurred.

##### Returns

Formatted exception with stack trace

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception object. |
| environmentStackTrace | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Environment stack trace, for pulling additional stack frames. |

<a name='M-ServiceLogLibrary-Classes-Exceptions-Write-System-Exception,ServiceLogLibrary-Classes-ExceptionLogType,System-String-'></a>
### Write(exception,exceptionLogType,text) `method`

##### Summary

Write Exception information to UnhandledException.txt in the executable folder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Strong typed |
| exceptionLogType | [ServiceLogLibrary.Classes.ExceptionLogType](#T-ServiceLogLibrary-Classes-ExceptionLogType 'ServiceLogLibrary.Classes.ExceptionLogType') | Type of exception which determines which file to log to.
Not passing this parameter will default to the general log file |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | append string to log entry |

<a name='M-ServiceLogLibrary-Classes-Exceptions-Write-System-String-'></a>
### Write(text) `method`

##### Summary

Write text to log file

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text to write |

<a name='T-ServiceLogLibrary-Classes-Operations'></a>
## Operations `type`

##### Namespace

ServiceLogLibrary.Classes

##### Summary

Several choices for unique file names, pick one remove the others.

<a name='F-ServiceLogLibrary-Classes-Operations-_pattern'></a>
### _pattern `constants`

##### Summary

Pattern for base file name incrementing together with [_baseFileName](#P-ServiceLogLibrary-Classes-Operations-_baseFileName 'ServiceLogLibrary.Classes.Operations._baseFileName')

<a name='P-ServiceLogLibrary-Classes-Operations-LogFiles'></a>
### LogFiles `property`

##### Summary

Get all log files

<a name='P-ServiceLogLibrary-Classes-Operations-UniqueFileNameByTicks'></a>
### UniqueFileNameByTicks `property`

##### Summary

Create a unique file name by date ticks

<a name='P-ServiceLogLibrary-Classes-Operations-_baseFileName'></a>
### _baseFileName `property`

##### Summary

Base file name together with [_pattern](#F-ServiceLogLibrary-Classes-Operations-_pattern 'ServiceLogLibrary.Classes.Operations._pattern')

<a name='M-ServiceLogLibrary-Classes-Operations-GenerateFileNameByDateAndGuid'></a>
### GenerateFileNameByDateAndGuid() `method`

##### Summary

Create a unique file name by date ticks and a guid

##### Parameters

This method has no parameters.

<a name='M-ServiceLogLibrary-Classes-Operations-GetLast'></a>
### GetLast() `method`

##### Summary

Get last log file by int value

##### Returns

Last log file

##### Parameters

This method has no parameters.

<a name='M-ServiceLogLibrary-Classes-Operations-GetNextFilename-System-String-'></a>
### GetNextFilename(pattern) `method`

##### Summary

Work horse for

##### Returns

Next file name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| pattern | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | [_pattern](#F-ServiceLogLibrary-Classes-Operations-_pattern 'ServiceLogLibrary.Classes.Operations._pattern') |

##### Remarks

DO NOT Change code in this method w/o talking to Karen

<a name='M-ServiceLogLibrary-Classes-Operations-GetNumbers-System-String-'></a>
### GetNumbers(input) `method`

##### Summary

Strip characters from string

##### Returns

string with numbers only

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| input | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-ServiceLogLibrary-Classes-Operations-HasAnyFiles'></a>
### HasAnyFiles() `method`

##### Summary

Determine if there are any files

##### Returns



##### Parameters

This method has no parameters.

<a name='M-ServiceLogLibrary-Classes-Operations-NextAvailableFilename-System-String-'></a>
### NextAvailableFilename(path) `method`

##### Summary

Wrapper for [GetNextFilename](#M-ServiceLogLibrary-Classes-Operations-GetNextFilename-System-String- 'ServiceLogLibrary.Classes.Operations.GetNextFilename(System.String)')

##### Returns

Next incremented file name

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-ServiceLogLibrary-Classes-Operations-NextFileName'></a>
### NextFileName() `method`

##### Summary

Wrapper for to obtain next available file name in a specific folder

##### Returns

Unique ordered file name

##### Parameters

This method has no parameters.

##### Remarks

Path is set to main assembly location with a base name of Import.txt

<a name='M-ServiceLogLibrary-Classes-Operations-RemoveAllFiles'></a>
### RemoveAllFiles() `method`

##### Summary

Remove all files

##### Parameters

This method has no parameters.

<a name='M-ServiceLogLibrary-Classes-Operations-WriteInformation-System-String-'></a>
### WriteInformation(text) `method`

##### Summary

Write to main/general log file w/o any exception information

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| text | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text to log |
