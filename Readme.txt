# What is this?

This is for controlling Magic UFO Led WIFI strip contollers.

This code is still very much work in progress.  Note that there is basically no error checking!

Currently this code compiles into a command line utility.  Here is the usage:

MagicUFOLedController <IPADDRESSES> <COMMAND>

<IPADDRESS> is a semi-colon delimited list of IP addresses.

Here are the <COMMANDS>

TURNON
TURNOFF
SETCOLOR RED GREEN BLUE WHITE
SETBRIGHTNESS VALUE 
SETRANDOMCOLOR BRIGHTNESS
CUSTOMFADES COLORSET FADEMODE SPEED


For example:

MagicUFOController.exe 192.168.1.143 TURNOFF

MagicUFOController.exe 192.168.1.143;192.168.1.144;192.168.1.145 TURNON

MagicUFOController.exe 192.168.1.143 SETCOLOR 200 60 20 0

MagicUFOController.exe 192.168.1.143 CUSTOMFADES 255,0,0,0;0,255,255,0 JUMPING 20

FADEMODE can be:

JUMPING
GRADUAL
STROBE

COLORSET is Red,Green,Blue,White sets delimted by semi-colons.The API supports up to 16.

SPEED is a number between 1 an 30 (1 slowest -- 30 fastest)

The Core API is in a class called LedApi.

<BRIGHTNESS> is optional for SETRANDOMCOLOR.  It will default to .5

Note that SETRANDOMCOLOR biases towards more saturated colors in the random number algorithm.  
