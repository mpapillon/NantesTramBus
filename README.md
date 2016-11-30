# Nantes Tram & Bus :tram: :fr:

![Captures d'écrans](screenshots.png)

_[English version below]_

*Nantes Trams & Bus* est une application Windows Phone 8 qui permet de se déplacer plus facilement sur le réseau [TAN](http://www.	tan.fr). 
L'application offre la possibilité de :
  - Visualiser les arrêts à proximité,
  - Connaître les prochains départs en temps réel à un arrêt,
  - Avoir un aperçus des arrêts desservis par une ligne.
  
> :warning: Le développement de NTB est arrêté, la nouvelle URL de l'API publique de la Semitan a quand même été ajoutée avant publication des sources.

## Compilation
Le projet est compatible avec [Visual Studio Community Edition](https://www.visualstudio.com/fr/vs/community/). Cependant, pensez à installer les outils de développement Windows Phone !  
Pour compiler NTB l'extension *SQLite pour Windows Phone 8* est nécessaire, [téléchargez-là sur le site de SQLite](https://sqlite.org/download.html) si ce n'est pas déja fait.

  1. Dans un premier temps, vous devez récupérer les dépendances NuGet ;  
     Dans VS, clic-droit sur la solution puis *Restaurer les packages NuGet*.  
  2. Assurez-vous que la référence pour **SQLite for Windows Phone (SQLite.WP80)** est résolue, le cas contraitre :
    1. Vérifiez que "SQLite pour Windows Phone 8" est installé, 
    2. Dans l'explorateur de solutions, faire un clic droit sur la section *Références > Ajouter une référence*,
    3. Ouvrez la section *Windows Phone SDK > Extensions*,
    4. Cochez **SQLite pour Windows Phone**.
    
## Licence
Les sources de *Nantes Trams & Bus* sont mises à disposition sous licence MIT, consultez le fichier [LICENCE](https://raw.githubusercontent.com/mpapillon/NantesTramBus/master/LICENSE) pour en savoir plus.

---

# Nantes Tram & Bus :tram: :us:

![Screenshots](screenshots.png)

*Nantes Trams & Bus* is a Windows Phone 8 app that allows you to move more easily over the french [TAN](http://www.	tan.fr) transports network of the city of Nantes.  
The application provides the ability to:
  - View nearby stops,
  - Know the next departures in real time at a stop,
  - Have a glimpse of stops served by a specific line.
   
> :warning: The development of NTB is stopped, the new URL of the public API of the Semitan (operator of the transport network) was added before publication of the sources.

## Compilation
The project works with [Visual Studio Community Edition](https://www.visualstudio.com/fr/vs/community/). However, consider installing the Windows Phone Development Tools!  
To compile NTB, the *SQLite for Windows Phone 8* extension is required, [download it from the SQLite website](https://sqlite.org/download.html) if not already done.

  1. First, you must recover NuGet dependencies;  
     In VS, right-clic on the solution then *Restore NuGet packages*.  
  2. Make sure that the assembly **SQLite for Windows Phone (SQLite.WP80)** is found, if not :
    1. Check if "SQLite pour Windows Phone 8" is installed, 
    2. In the Solutions explorer, right-clic on *References > Add reference*,
    3. Open *Windows Phone SDK > Extensions*,
    4. Select **SQLite pour Windows Phone**.
    
## Licence
Sources of *Nantes Trams & Bus* are covered by MIT licence, see the [LICENCE](https://raw.githubusercontent.com/mpapillon/NantesTramBus/master/LICENSE) file to know more.
