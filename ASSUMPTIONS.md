### Assumptions

Given the nature of this problem I made some assumptions:
- We can have as many landing areas as we want to
- We can have as many landing platforms as we want to
- We can have as many rockets as we want to
- Landing areas are unique and have their own identifier
- A landing area can be up to `int.MaxValue`x`int.MaxValue`
- A landing area min size is `1x1`
- A landing area size can not be a negative value
- A landing area can contain many landing platforms as long as they fit inside boundaries and do not overlap each other
- A landing platform size can be up to `int.MaxValue`x`int.MaxValue`
- A landing platform min size size is `1x1`
- A landing platform size can not be a negative value
- Landing platforms are unique and have their own identifier
- A landing platform can not occupy the same space as another landing platform (they can not overlap each other)
- Rockets are unique and they have their own identifier
- A rocket can occupy only one slot inside a landing platform plus the extra radius around it
- The radius around the rocket is equal to one. e.g.
```
     0     1     2     3     4 
                               
0   [ ]   [X]   [X]   [X]   [ ]
                               
                               
1   [ ]   [X]   [R]   [X]   [ ]
                               
                               
2   [ ]   [X]   [X]   [X]   [ ]
                               
                               
3   [ ]   [ ]   [ ]   [X]   [X]
                                                             
                                                             
4   [ ]   [ ]   [ ]   [X]   [R] 
```
- The rocket radius can go outside the landing area boundires
- The rocket radius can go outside the landing platform boundires
- A rocket can not occupy more than one landing platform at the same time
- A rocket can land and take off from a platform at any time
- A landing platform can be removed from the landing area only if it does not contain any rocket
- A landing platform can be added to a landing area only if it fits inside the boundaries and does not overlap with another landing platform