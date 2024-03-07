_Github does not allow having a Wiki on private repositories unless you have Premium, so we'll have to do with this for now. :)_

This is a simple (WIP) guide on our design philosophy regarding events, we hope this not only helps future contributions within this repository, but also provide some assistance to those in the process of learning the art that programming is.

## Why use events?

This is a question that has been answered multiple times in the entirety of the Internet, with multiple valid reasons for their usage.

However, the primary focus we give C#'s events are their scalability. Imagine having to call every single separate script from one method, the amount of conflicts you will have! And how quickly you will start wondering where things went wrong because Unity's stacktrace won't tell you much!

This is where events come in handy, as they allow you to extend as much functionality to a specific trigger as you may want, while also allowing others to add to it.
This is particularly useful in big plugin projects, where you could create one plugin functioning as a library, and have your new features added as modules requiring the library.

## What are the guidelines on events for this repository?

We have decided upon a few rules for events, based on our personal experiences working in the modding community for the game for many years, as well as participating in numerous projects.

### **Naming**:

- **Event Scripts:**
The scripts which contain our events, we usually name them based on the origin from said event, using the following template: `CategoryEvents`.

For example, should we create events for players, the script containing all player-related events will be called `PlayerEvents`.

_⚠ Disclaimer: We name them this way to make them a lot easier to access for modders, but would generally suggest a different approach for events in most cases._

- **Events:**
The naming conventions for events should always be the verb, followed by the adecuate suffix.
For example, we want to create an event that is triggered when the player joins, so we could na

As always, everyone is free to code however they please, but as to maintain
As was discussed, we'd want to go for

ActionIng/Ed events (wheras possible)

Ing events should be modifiable, and cancellable if applicable, eg modifying damage, cancelling the damage

Ed are regarded as "have happened" and cannot be cancelled.


We'd want the classes their declared in to be static for ease of use on our end, and anyone who may have harmony patches and wants to call an event in their modified code 

And naming would also be 
CategoryEvents 

Like

PlayerEvents.Joined
PlayerEvents.PreAuthenticating
PlayerEvents.PreAuthenticated 
and obviously the events would have to be acompanied by a method to invoke it, as you cant invoke an event from a separate class without exposing a method to do so
Then with events come wrappers combined

As you cant really have Player events without the Player wrapper (using referencehub as placeholder is kinda double work tbh)
But it would all start out with the base of the event system
Which should have exception handling incase a plugin has an error, and invoke events in the order of plugin priority

Then come ServerEvents like WaitingForPlayers, RestartingRound as we may need some of these to clear lists in our wrappers, eg clear cached lists
Then we can start opening up wrapper tasks for players, map, and anything else we see fit, im not sure how far we want to go with wrappers and creating events for everything so do please let me know what your opinion is on that 
Allowing/Dissalowing events would be a boolean i'd say, plugins can check if an event is already cancelled in that way if needed
(im good at thinking of more stuff after i already post my message lmao)










If an event is cancelled the game code should not make any changes to the state of the entity the event is called for/on
so no cooldowns, no response from doors, no hitmarkers, etc

What we can do in labapi however, which we cant in the nwapi which is a big game changer for it

Is add eventargs for ForceCooldown
PlayNoise (like or rejecting door operations)
