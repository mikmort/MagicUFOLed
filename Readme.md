# MagicUFOLed API

This is a c# for controlling Magic UFO Led WIFI strip contollers.  For example:

http://www.amazon.com/LEDENET%C2%AE-Controller-Android-Smartphone-Control/dp/B00MDKOSN0/ref=sr_1_1?ie=UTF8&qid=1462662194&sr=8-1&keywords=magic+ufo+led

## Code Status

This code is still very much work in progress.  Note that there is basically no error checking!

## Usage

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