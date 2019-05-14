# ULTRA

ULTRA is a 2D fighting game in Unity. It's a lot like Street Fighter. I plan to release it for iOS and Android.

Controls (planned, subject to change throughout development):

    - Press the arrow buttons on screen to walk left and right.

    - Tap the screen while standing to attack, and keep tapping to do several combos. Whichever direction
    you tap relative to your character is the direction in which you'll attack. If they all hit, the first 
    three attacks will do light damage, and the fourth will be a "heavy attack" - see below. You don't need 
    to successfully hit the opponent to keep attacking.

    - Swipe in the direction that you're facing, or tap four times while standing to do a "heavy attack" - 
    if it hits, it'll do decent damage and send the enemy flying back.

    - Swipe in the opposite direction that you're facing to quickly roll backwards.
    
For now, I'm using existing sprites from Street Fighter Arcade and Samurai Showdown 4. When I release it, I'll change these to my own sprites.

There's a lot of stuff that I plan to add, but didn't yet have the time to.

Fun facts:

    - There is very little move lag. It's most noticeable when rolling back; you can act almost immediately after the frame on which it ends. I kept this in because I thought it was funny, and the fast-paced action is fun. 

    - Since it's inconvenient for me to test on mobile (because I have an iPhone, but no Mac to deploy the app with), I wrote an API to control the characters with one keyboard. So, you can play with two people on a computer, but not on mobile yet (because I didn't implement local or internet-based multiplayer).

    - The samurai is way more buff and detailed-looking than the wizard or fighter because he's from a different game. It's so jarring that I actually considered making him an unlockable character, but I didn't have time to do it.

    - The victory/losing message displays a random word to describe the loser after the game ends. Some examples of possible words are "wrecked", "bodied", "annihilated", and "thrashed."
