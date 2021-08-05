# Translator
Translator is a project I've been working on to turn Pseudo Code into Python, it was originally written in C# when I first started programming but I have decided to rewrite it in c++ hopefully a bit better than last time

Currently it can translate 4 things:
- Comments
- Variables
- Inputs
- Outputs
- If
- Else
- Elif

for example:

VAR x = "hello"  
OUTPUT {x}world  
INPUT y pick a number:  
IF y == 5
!OUTPUT you picked the number 5!  

Will translate to:  
  
x = "hello"  
print(f"{x}world")  
y = input("pick a number:")  
if y == 5:  
    print(f"you picked the number 5!")  
