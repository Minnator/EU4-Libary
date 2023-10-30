# EU4 Parse Lib

##About this project:
This project is intended to be developed into a full EU4 Mod Editor with amitious features such as automatic content generation.
I am working on this alone in my free time so don't expect to quick progress.
The Current scope of this project is:
* Userdefined custom mapmodes that can be exported [almost done]
* Fully interactive map with a customizable hover tooltip [Zoom and customization of tooltip are missing]
* (Map Editing)
* Fully automated province file creation [not done but another programm exists for that --> https://github.com/Minnator/Province-File-Creator-2.0]
* Automatic mission generation [no progress yet]
* Fully parsed EU4 Vanilla as well as Mods due to my own custom language [about 15% done --> https://github.com/Minnator/EU4-Grammar]
* Interfaces to easily create often modified content [no progress yet]
* ``.gui`` and ``.gfx`` modding with live preview without EU4 being launched [no progress yet]
* A ton of other features and quality of live options
* Very extensive statistics, with options to calculate custom scores and map them on the interactive map [in planning]
* Economic simulation as far as possible [no progress yet, least priority]

### Map limitations
- expects 24bpp file
- map must be smaller than 50mb (program will work with bigger but EU4 won't)
- map must have at least 7 provinces equally spaced, otherwise the provinces are to big and EU4 wont launch
- there must not be Black in the map (255, 0, 0, 0)
- Colors not mentioned in ``definition.csv`` can cause exceptions and programm crashes [will be fixed]
- exported maps can be found in ``<programmLocation>//ExportedData//Maps//<name>`` [not fully done yet]

### Testing and Early Builds
Currently there are no released versions or early builds of this program.
If there is intrest in early (most likely very unstable builds) message me and we can work smth out.

### Feature Requests
I am open for new ideas on how to enhance the usability and the range of this project, so feel free so suggest and request features, but don't expect to much as the scope is already huge.

### Joining my work:
If you know ``C#`` or ``ANTLR`` and want to provide some help in development, feel free to message me and we can see if we can figure something out.

### Images of how it looks right now:
The default map:

![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/37f391e6-d910-4de4-957a-dfab6d22c740)
Custom map mode creation window

![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/10664d7a-b112-4732-8abf-efe6abf8d873)
The map mode from the custom creation window rendered in realtime

![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/62624385-c41e-41b8-8663-1594596c6c0b)
Example of settings

![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/8451619e-f963-4c95-9bea-588c8acf8e80)

Example of the context menu for quick actions (Also supports any kind of custom trigger)

![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/5bb42ad5-dad9-4c9b-909a-a9c891a80627)
![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/6dd9e317-1296-43bb-a506-bfc8e667de75)
![grafik](https://github.com/Minnator/EU4-Libary/assets/50293050/40009d92-d014-4ca5-b7e8-bd1f94002e71)



