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
* Fully parsed EU4 Vanilla as well as Mods due to my own custom language [about 15% done]
* Interfaces to easily create often modified content [no progress yet]
* _.gui_ and _.gfx_ modding with live preview without EU4 being launched [no progress yet]
* A ton of other features and quality of live options
* Very extensive statistics, with options to calculate custom scores and map them on the interactive map [in planning]
* Economic simulation as far as possible [no progress yet, least priority]

### Map limitations
- expects 24bpp file
- map must be smaller than 50mb (program will work with bigger but EU4 won't)
- map must have at least 7 provinces equally spaced, otherwise the provinces are to big and EU4 wont launch
- there must not be Black in the map (255, 0, 0, 0)
- Colors not mentioned in _definition.csv_ can cause exceptions and programm crashes [will be fixed]
- exported maps can be found in _<programmLocation>//ExportedData//Maps//<name>_ [not fully done yet]

### Testing and Early Builds
Currently there are no released versions or early builds of this program.
If there is intrest in early (most likely very unstable builds) message me and we can work smth out.

### Feature Requests
I am open for new ideas on how to enhance the usability and the range of this project, so feel free so suggest and request features, but don't expect to much as the scope is already huge.

### Joining my work:
If you know C# or ANTLR and want to provide some help in development, feel free to message me and we can see if we can figure something out.
