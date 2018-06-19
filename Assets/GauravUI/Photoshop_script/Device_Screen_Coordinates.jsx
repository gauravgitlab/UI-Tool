var doc = app.activeDocument; 
var strtRulerUnits = app.preferences.rulerUnits; 
var strtTypeUnits = app.preferences.typeUnits; 
app.preferences.rulerUnits = Units.PIXELS; 
app.preferences.typeUnits = TypeUnits.PIXELS;
var file = new File(Folder.desktop + "/Device_Screen_Coordinates.txt");
file.open("w", "TEXT", "????");
$.os.search(/windows/i)  != -1 ? file.lineFeed = 'windows'  : file.lineFeed = 'macintosh';
file.writeln("#LayerName,Left,Top,Right,Bottom");
processLayers(app.activeDocument);
file.close();
file.execute();
app.preferences.rulerUnits = strtRulerUnits; 
app.preferences.typeUnits = strtTypeUnits; 
function processLayers (objectRef) {
var myNumber = objectRef.layers.length; 
for (var i = 0; i < myNumber ; i++) { 
var myLayer = objectRef.layers[i]; 
  activeDocument.activeLayer = myLayer;  
  if(myLayer.kind == LayerKind.NORMAL){
      var LB = app.activeDocument.activeLayer.bounds; 
      var LayerInfo = app.activeDocument.activeLayer.name +",";
      LayerInfo += LB[0].value +","+LB[1].value +","+LB[2].value +","+LB[3].value;
      file.writeln(LayerInfo);
      LayerInfo = '';
      }
if (myLayer.typename == 'LayerSet') { 
  processLayers (myLayer);
        } 
    } 
}