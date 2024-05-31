-> farm_girl
=== farm_girl ===

= introduction
Hello there!
Welcome to our farm.
I would love to show you around, but I'm very busy right now.
It's apple harvesting time, and we aren't even half way done!
Everyone has to help, even my little sister. But I promised to play with her first...
Oh, there's no time for that!
Do you want to help me?
+ [Sure, why not? I have time!]
~ Event("Start Farm Girl Quest")
-> help_accepted
+ [That sounds exhausting... Sorry, I'll pass.]
-> help_declined

= help_accepted
That's awesome, we could really use another pair of hands!
But first, why don't you talk to my sister?
You could play with her while I prepare everything we need!
-> END

= help_declined
I understand, hard work on the farm isn't for everyone.
Come back if you change your mind though!
-> END

= quest_running
I'm making preparations right now! Please talk to my little sister.
-> END

= quest_declined
Welcome back! Have you changed your mind about helping us?
+ [Sure, why not? I have time!]
~ Event("Start Farm Girl Quest")
-> help_accepted
+ [That sounds exhausting... Sorry, I'll pass.]
-> help_declined