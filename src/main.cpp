#include <iostream>
#include <fstream>
#include <bits/stdc++.h>
 
using namespace std;

void readlines();

static string path;

enum class Types
{
	Comment,
	Variable
};

map<string, Types> mapStringToTypes = {
	{"#", Types::Comment},
	{"VAR", Types::Variable}

};

map<Types, string> mapTypesToString = {
	{Types::Comment, "#"},
	{Types::Variable, "VAR"}
};


int main(int argc, char *argv[])
{
	cout << "Code Generator V2:\n";
	bool t = true;
	do {
		readlines();
		char yn;
		cout << "======\n\nWould you like to translate another file? Y/N\n";
		cin >> yn; yn = toupper(yn);
		switch (yn){
			case 'Y':
			  break;
			case 'N':
			  t = false;
			  break;
			default:
			  cout << "Invalid - Assuming False\n";
			  t = false;	    
		}
	} while (t);
	exit(0);	
}

void writelines(string line){

}

string tokenizer(string s, int i){
	stringstream stream(s);
	vector<string> str;
	string word;

	while (stream >> word){
		str.push_back(word);
	}
	return str[i];
}

void translate(string line){
	string firstword = tokenizer(line, 0);
	string splitline = line.substr(line.find_first_of(firstword) + firstword.length());
	Types keyword;
	try{
		keyword = mapStringToTypes[firstword];
	}catch (exception){
		keyword = Types::Comment;
	}
	string translated;
	switch (keyword){
		case Types::Comment:
		  translated = "#" + splitline;  
		  break;
		case Types::Variable:
		  translated = tokenizer(splitline, 0);
		  splitline = splitline.substr(splitline.find_first_of(translated) + firstword.length());
		  translated = translated + " = " + splitline;
		  break;
		default:
		  // ! if its default just comment it?
		  break;
	}
	cout << translated << endl;
	// TODO: pass to translated func

}

void readlines(){
	try{
	    cout << "Please pick a file\n";
	    cin >> path;
		ifstream file(path);
		string line;

        // TODO: check that path is valid

        translate("# Translator by RhubarbDev - https://github.com/RhubarbJamm");
		while (getline(file, line)){
			translate(line);
		}
	}
	catch (exception){
		cout << "ERROR :-(";
		exit(0);
	}

}