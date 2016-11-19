# teolib

teolib is short for TExt Oriented Layering lIBrary. It's nothing big, just a little text-layering library for C# I threw together in my free time. Use it if you want, and if you think it can be better, send me a message.

# What is Text Layering?

If you've ever used a high-end image editor like GIMP or Krita before, the concept of layering should be familiar to you. Let's say you have two layers; a background greenish color, and a stick figure that you drew. You can see the greenish background through the transparent spaces in the stick figure, but not the places that are occupied by actual color.

This is what teolib is like, but for text. If you have a background made up entirely of green text, but you want to append your ASCII image of a dog to it, this is what it's for.

# How might one use it?

In this example, I use Teolib to create a sample dialog.

    // first, import libraries
    using System;
    using teolib;

    // we will make a sample dialog here
    public class Program {
      public static void main(string[] args) {
        // first, grab the output manager from the Teolib helper class, this starts Teolib
        OutputManager output = Teolib.GetOutputManager();

        // we're going to use a few sample methods to create a dialog box with a border
        string message = "Hello World!";
        TextLayer tl1 = TextLayer.Border(output.Width, output.Height);
        TextLayer tl2 = TextLayer.Dialog(output.Width, output.Height, message, message.Length);
 
        // add both layers to the output manager
        output.AddLayer(tl1);
        output.AddLayer(tl2);

        // output to the screen
        output.Refresh();

        // wait for user input to end
        Console.ReadChar(); 
      }
    }
