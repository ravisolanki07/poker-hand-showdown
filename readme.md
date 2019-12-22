#Assumtions:  
This allows minimum 2 and maximum 9 players.  
The validations in input parameters are not implemented.  
The input validation for repeating the same card in same suit is not implemented.  
Card Suit and Rank have to be in capital letters.
The player names validation is not implemented.  
The two pairs scenario is not considered.  
Logging is not implemented.  

CARD EXAMPLES:
  
#High Card  
--SAME  
KS QC AD 8H 4S  
KS AH QS 8D 4S  
4S QH AS 8C KD   
  
5S 4C 6D 7H 3S  
5S 4C 6D 7H 3S  
5S 4C 8D JH 3S  
  
#Flush  
2H 8H AH 5H 3H  
9H 8H AH 6H 4H  
9C 8C AC 6C 3C  
2S 8S AS 5S 3S  
  
#Four of a kind  
KS KC KD KH 3S  
AS AC AD AH 5S  
  
#Three of a kind  
KS KC KD 5H 3S  
8S 8C 8D 5H 3C  
3S 3C 3D 5H 2S  
  
#Two of a kind  
KS KH 2C 5D 3S  
AS AH 2C 5D 3S  
JS JH 2C 5D 3S  
  
