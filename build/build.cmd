@echo off

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /t:Clean /t:Rebuild /nologo /m /v:q /p:Configuration=Release ..\src\EfficientlyLazy.IdentityGenerator.sln
.\nuget.exe pack ./EfficientlyLazy.IdentityGenerator.nuspec -outputDirectory ./Artifacts

pause