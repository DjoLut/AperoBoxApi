# AperoBox
L’application "ApéroBox" est une application permettant de commander des box apéritives et de se les faire livrer à domicile.

## Prérequis
* Visual Studio Code : [VSC](https://code.visualstudio.com "Visual Studio Code Home Page")
* Git pour cloner le projet : [Git](https://git-scm.com/downloads "Git Download Page")
* Nous utilisons la version 3.0.100 de dotnet Core (dotnet --version), si vous ne l'avez pas, vous pouvez l'installer à cette adresse : [Dotnet](https://dotnet.microsoft.com/download "Dotnet Core Download Page")

## Installation
Nous recommandons l'utilisation de Visual Studio Code.

1. Lancez Visual Studio Code et ouvrez un terminal
2. Après, positionnez-vous dans le répertoire où vous souhaitez installer le projet avec la commande "cd url" (exemple : cd C:\Users\XXXXX\Desktop)
3. Lancez la commande "git clone https://github.com/etu32766/AperoBoxApi" et déplacez-vous vers dans le dossier src avec "cd Src"
4. Si Visual Studio Code vous propose de faire un "restore" alors acceptez, s'il ne le propose pas automatiquement alors utilisez la commande "dotnet restore"
5. Compilez le projet avec la commande "dotnet build"
6. Puis lancez le projet avec la commande "dotnet run" et utilisez votre testeur d'api préféré sur l'url suivante : https://localhost:5001/api/XXXX (Exemple : Postman)

(Documentation Swagger : https://localhost:5001/swagger/index.html)