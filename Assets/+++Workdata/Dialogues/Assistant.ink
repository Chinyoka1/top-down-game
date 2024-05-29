-> assistant
=== assistant ===

= get_sword_quest
You want to go into the marshland? I wouldn't try that without a weapon!
It's not as peaceful as the flower forest, there are quite a few dangerous animals and monsters lurking there.
But you don't look like you have much experience in fighting... Oh dear...
Hmm. Go to the blacksmith in town and get a sword. Maybe that will be enough for now to defend yourself.
If you can manage to not cut off your own arm with it...
~ Event("Start Sword Quest")
-> END

= get_sword_running
You haven't bought a sword yet? I can't stop you, but I would strongly advise against going into the marshland unarmed...
-> END

= get_sword_deliver
You're back! And you still have all your limbs where they belong! That's promising.
Alright, at least you can defend yourself now if you get attacked by anything.
But you should still be careful!
Oh, and take this with you. It's a healing potion. Just in case.
~ Event("Deliver Sword Quest")
~ Add_State("item_health_potion", 1)
-> END

= get_sword_finished
Good luck out there!
-> END