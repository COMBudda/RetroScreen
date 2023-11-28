# RetroScreen
C# to visualize a digital signal like MDA, CGA and EGA using an USB logic analyzer

It is loosely based on the sigrok2EGA project and uses the hardware connections defined there (see at the end).

If started without arguments, it tries to start sigrok in 24Mhz mode. Currently the sigrok location is hardcoded here:
"C:\Program Files\sigrok\sigrok-cli\sigrok-cli"

It can also be started in a CMD window with the "-" argument and accepts any 8 bit data stream as stdin. ("myprogramm | CGATest -").

Analyzer Channel - EGA 9-Pin connector

Channel 1 - 5 B

Channel 2 - 4 G

Channel 3 - 3 R

Channel 4 - 6 Gint

Channel 5 - 2 Rint

Channel 6 - 7 Bint

Channel 7 - 8 Hsync

Channel 8 - 9 Vsync
