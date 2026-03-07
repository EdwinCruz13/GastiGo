@echo off
REM ================================================
REM SUERTE Y QUE LA FUERZA LOS ACOMPA�E :D
REM ================================================

REM para primera migracion puedes usar un nombre llamado "InitialCreate"
REM pero si cambias alguna columna, entonces usa otro nombre "Mi cambioColumna"
REM luegos ejecutas update 

set /p name=Migration name:

dotnet ef migrations add %name% ^
--project Infrastructure ^
--startup-project GastiGo.API


dotnet ef database update ^
--project Infrastructure ^
--startup-project GastiGo.API

pause