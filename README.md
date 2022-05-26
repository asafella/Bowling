# Bowling
Bowling Scoring Code

The Code, written in .NET Core 3.0 C# as console application, implements the Bowling scoring algorithm.

The code starts by displaying the instructions, and then awaits the user's input.
the input can be either the score of a roll (as a number between 0-10), or a command. every frames (there are basically 10 frames but the code uses, for simplicity, another optional frame in case there's a 3rd roll for the 10th frame) holds 2 positions for ball rolling.

In order to use the application that illustrates the use of the algorithm, run the code (the executable application can be found in the /bin folder), and follow the following instructions:

1. Enter the number of knocked down pins for each throw.
2. Use '/' for spare, 'x' for strike.
3. Scoring table, listing all the frames, can be presented by entering 'p'.
4. In order to display these instructions again, just time 'i'.
5. If you want to terminate this app, enter 'exit'.\n");

Just remember to always end your entry with 'Enter'

important: there are several approached for implementing this algorithm. Although strike affects the score of max to positions backwards, I have decided to loop through all of the positions for every ball rolling and update the frames score instead of referring to the current position and calculating the different options that need to be handled due to the complexity of the code in that case (quite a lot of specific cases to check) and the fact that there are only 10 frames (therefore not too many returns by using a loop as i did). The alternatives were evaluated during the process of writing this code.

Enjoy :)
