# Soundboard
A soundboard built into a RichTextbox so it can be used to both read the script of a theatre play and play music and/or sound effects from buttons integrated to the script itself!

## How to use?

- Create a .script file
- Import it
- Do what you want!

### Known issues

- Because the program loads sound files when the mouse is hovered over a button so that sound is not delayed on press, it will cause a crash if the file is not found at where it should have been. This is technically not my fault but yours, but I thought some of you may create an issue called "hover over button to crash" to me. So, before opening an issue, check if file exists, check if the path in the script is correct and the file integrity.

### Anything you recommend?

- I highly recommend keeping sound files in the same directory as the executable.

### Frequently Asked Questions
#### What is a .script file?

It is just plain text file, where buttons are inserted into the text using the following format:

`[[file=Path/To/Sound/File.wav|text=What you want the button to say.]]`

Where, the following file:

`test text 1 [[file=Laugh.wav|text=Laugh button test]] test text 2`

will be converted into this:

[![image](https://image.prntscr.com/image/ksAYMkAIS1qd8Fvqfj4PhA.png)

#### Does the sound files have to be `.wav`?

Yes.

#### Do I have to create .script files manually?

Yes.

#### Why don't you just add a window to do that with a UI?

It's not worth the effort. Anyone *should be able to* create the file in a text editor. I doubt anyone will ever use this program anyways. Except for me, where I used this for a silly theater play in high school half a decade ago. I still live its trauma to this day, despite not even being on the stage.
