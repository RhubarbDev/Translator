#include <iostream>
#include <fstream>
#include <bits/stdc++.h>
 
using namespace std;

void readlines();

static string path;
static string outputpath;

enum class Types
{
	Comment,
	Variable,
	Output,
	Input
};

map<string, Types> mapStringToTypes = {
	{"#", Types::Comment},
	{"VAR", Types::Variable},
	{"OUTPUT", Types::Output},
	{"INPUT", Types::Input}

};

map<Types, string> mapTypesToString = {
	{Types::Comment, "#"},
	{Types::Variable, "VAR"},
	{Types::Output, "OUTPUT"},
	{Types::Input, "INPUT"}
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

void writelines(vector<string> lines){
	// TODO: get path of output folder then write line
	if (outputpath != ""){ 
		ofstream pythonfile;
		pythonfile.open(outputpath, ios_base::app);
		for (string line : lines){
			pythonfile << line << endl;
		}
		pythonfile.close();
	}else{
		cout << "Please pick an output path:" << endl;
		cin >> outputpath;
		writelines(lines);
	}
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

string translate(string line){
	string firstword = tokenizer(line, 0);
	string splitline = line.substr(firstword.length() + 1);
	Types keyword;
	try{
		keyword = mapStringToTypes[firstword];
	}catch (exception){
		keyword = Types::Comment;
	}
	string translated;
	switch (keyword){
		case Types::Comment:
		  translated = "# " + splitline;  
		  break;
		case Types::Variable:
		  translated = tokenizer(splitline, 0);
		  splitline = splitline.substr(tokenizer(splitline, 0).length() + 1);
		  translated = translated + " = " + splitline;
		  break;
        case Types::Output:
		  translated = ("print(f\"" + splitline + "\")");
		  break;
		case Types::Input:
		  translated = tokenizer(splitline, 0);
		  splitline = splitline.substr(tokenizer(splitline, 0).length() + 1);
		  translated = translated + (" = input(\"" + splitline + "\")");
		  break;
		default:
		  translated = "# " + splitline + " (default)";
		  break;
	}
	return translated;
}

void readlines(){
	try{
	    cout << "Please pick a file\n";
	    cin >> path;
		ifstream file(path);
		string line;

        // TODO: check that path is valid

		vector<string> translatedlines;

        translatedlines.push_back("# Translator by RhubarbDev - https://github.com/RhubarbJamm");
		while (getline(file, line)){
			if (line == ""){ continue; }
			translatedlines.push_back(translate(line));
		}
		writelines(translatedlines);
	}
	catch (exception){
		cout << "ERROR :-(";
		exit(0);
	}
}