FOnline WorldEditor
========
WorldEditor is an editor for the FOnline engine, created to make the development process more accessible and faster. It was long developed as an internal tool exclusive to the FOnline: 2238 team, but is now released to the greater public.

The editor is capable of parsing almost all of the serverside gamedata used with the FOnline engine and makes editing of the worldmap very easy. No in-depth knowledge is required of most of files, and with the powerful scripting engine which utilizes C#, more useful utilities can be added quickly and with less effort than in a conventional scripting languge. It's even possible to make your extension scripts, scriptable!

1. System Requirements
--------
* Microsoft Windows XP/Vista/7/8
  Older versions of Windows can't run .NET 4.0
  It may work on Linux with Mono, or Wine, not tested yet.
* .NET 4.0 framework
  If you don't already have it install, the application may crash upon start.
  The .NET redistributable install can be downloaded freely from http://www.microsoft.com/download/en/details.aspx?id=17718.


2. Installation & Instructions
--------
WorldEditor.cfg contains installation specific config options, while WorldEditor.user.cfg contains user preferences, this is designed for usage in a source control system.

These are the command-line switches available:

--shell - Display the FOnline command shell, which can be used to interactively query the game data in a fast way with for example LINQ.
--nogui - Start the application with no GUI but loads all gamedata like usual, if you want to use only the shell.

"FOnline Shell.bat" simply launches the program with both switches.

3. Documentation
--------
API documentation is available in the "docs" directory, if you want to start writing scripts, the easiest way is to look at the extension scripts provided in the "scripts" directory.

4. Extension scripts
--------
WE has a fully featured scripting engine which exposes the internal API, enabling anyone to write scripts in C# to provide powerful custom features. There are no limits, you can use all features of C# 4.0 and the whole .NET library. Check the scripts directory for some examples.

5. Credits & Authors
--------
WorldEditor + base extensions by Ghosthack

Critter Proto editor by Atom

http://www.fodev.net

WorldEditor uses the following libraries and code:

C# Script execution engine, (c) 2004-2013 Oleg Shilo.
http://www.csscript.net

ObjectListView, (c) 2006-2013 Phillip Piper.
http://objectlistview.sourceforge.net

.NET 4.0 framework, (c) Microsoft
http://en.wikipedia.org/wiki/.NET_Framework
