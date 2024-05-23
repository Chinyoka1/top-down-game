-> quest_board
=== quest_board ===

= apple_quest
The bakery wants to offer apple pie today, but some of the apples from yesterdays delivery were stolen! Please help us out by collecting 5 apples from the forest. Payment 3 Gold Coins
* [Accept]
~ Event("Start Apple Quest")
->END
+ [Not right now.]
->END

= apple_running
You should collect 5 apples and bring them back to the board.
->END

= apple_deliver
Deliver the apples?
* [Give 5 apples]
Thank you for your contribution! + 3 Gold Coins
~ Event("Deliver Apple Quest")
~ Add_State("item_apple", -5)
~ Add_State("item_gold_coin", 3)
->END
+ [No, keep my apples]
-> END

= apple_finished
Come back later for more quests!
-> END