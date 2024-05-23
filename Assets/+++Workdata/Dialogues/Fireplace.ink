=== Fireplace ===

= check_lighter
{Get_State("item_lighter") and not turn_on:-> has_lighter}
I don't have anything to turn it on.
-> END

= has_lighter
~ Event("Has Lighter")
->turn_on
-> END

= turn_on
Finally some warmth in here.
~ Event("Bedroom Fireplace On")
-> END