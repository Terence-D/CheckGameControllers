# CheckGameControllers
Checks if specified game controllers are plugged in before launching your game

## Command Line Parameters
You can specify 2 command line parameters, BOTH are optional:
* --controllers: A comma separated list of controllers you want to check for, see below for details.  If not included, it will look at a file called controllers.txt
* --app:  Specify the app to load if all game controllers are found.  If not specified, it returns an error code value of how many controllers are missing

## Examples
### CheckGameControllers.exe
This will look for a list of controllers in controllers.txt.  If any are missing, a message box will display showing what is missing, and an error code specifying how many 
controllers are missing will be returned.  Otherwise it will return an error code of 0.
### CheckGameControllers.exe --controllers "Controller A,GamepadB,Case Is ImportaNT" 
This will look for 3 controllers. If any are missing, a message box will display showing what is missing, and an error code specifying how many 
controllers are missing will be returned.  Otherwise it will return an error code of 0.
### CheckGameControllers.exe --app "C:\Program Files\Some Awesome Game\game.exe" 
This will look for a list of controllers in controllers.txt.  If any are missing, a message box will display showing what is missing, and an error code specifying how many 
controllers are missing will be returned.  Otherwise it will run "C:\Program Files\Some Awesome Game\game.exe" 
### CheckGameControllers.exe --controllers "Controller A" --app "C:\Program Files\Some Awesome Game\game.exe" 
This will look for 1 controller. If any are missing, a message box will display showing what is missing, and an error code specifying how many 
controllers are missing will be returned.  Otherwise it will run "C:\Program Files\Some Awesome Game\game.exe" 
### Batch File (included)
```batch
@echo off
CheckGameControllers.exe
if %ERRORLEVEL% NEQ 0 EXIT

REM Put the commands you want to run below this line, full path to the file included
REM C:\Program Files\.... 

```
