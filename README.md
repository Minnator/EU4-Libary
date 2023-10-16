# EU4 Parse Lib

### Map limitations
- expects 24bpp file
- map must be smaller than 50mb (will work with bigger but EU4 wont)
- map must have at least 7 provinces equally spaced, otherwise the provinces are to big and EU4 wont launch
- there must not be Black in the map (255, 0, 0, 0)
- Colors not mentioned in _definition.csv_ can cause exceptions and programm crashes
- map should not be edited by the programm
- exported maps can be found in _<programmLocation>//ExportedData//Maps//<name>_
