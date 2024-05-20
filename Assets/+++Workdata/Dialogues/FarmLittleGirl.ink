-> farm_little_girl
=== farm_little_girl ===

= talk
{has_accepted_help: ->quest_unlocked}
{not has_accepted_help: ->before_quest}

= before_quest
My big sister promised to play hide and seek in the forest with me today.
But all she thinks about is work!
-> END

= quest_unlocked
Is she finally done with the preparations? I've been waiting forever!
Oh, you want to play with me? But my sister promised she would...
* [I am VERY good at hiding.]
-> quest_reply_hiding
* [Farm work is important.]
-> quest_reply_work

= quest_reply_hiding
Is that so...
Alright, I want to see if you are actually as good as you say!
Maybe you can even show me some new hiding spots.
But just so you know, I know this forest better than anyone around here.
Even better than my big sister.
So be on your guard!
-> END

= quest_reply_work
(Sigh) I know, and I understand that.
I'm really proud that my big sister works so hard for our family!
Without her, we would probably struggle to keep the farm running.
But she should also think about having fun sometimes!
What good is all that work when you never have time to play?
Well, maybe another day. Let's go!
-> END