This is for controlling Magic UFO Led WIFI strip contollers.

This code is still very much work in progress.

Currently this code compiles into a command line utility.  Here is the usage:

MagicUFOLedController <IPADDRESSES> <COMMAND>

<IPADDRESS> is a semi-colon delimited list of IP addresses.

Here are the <COMMANDS>

TURNON
TURNOFF
SETCOLOR RED GREEN BLUE WHITE
SETBRIGHTNESS VALUE 
CUSTOMFADES COLORSET FADEMODE SPEED

For example:

MagicUFOController.exe 192.168.1.143 TURNOFF

MagicUFOController.exe 192.168.1.143 TURNON

MagicUFOController.exe 192.168.1.143 SETCOLOR 200 60 20 0

MagicUFOController.exe 192.168.1.143 CUSTOMFADES 255,0,0,0;0,255,255,0 JUMPING 20

The Core API is in a class called LedApi.
