# Translator
Translator is a project I've been working on to turn Pseudo Code into Python, it was originally written in C# when I first started programming but I have decided to rewrite it in c++ hopefully a bit better than last time

Currently it can translate 4 things:
- Comments
- Variables
- Inputs
- Outputs

for example:

VAR x = "hello"  
OUTPUT {x}world  
INPUT y this is an input  
OUTPUT {y}  

Will translate to:  
  
x = "hello"  
print(f"{x}world")  
y = input("this is an input")  
print(f"{y}")  

