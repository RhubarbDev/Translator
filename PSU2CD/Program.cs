using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PSU2CD
{
    class Program
    {
        private static string path = "false";

        static void Main(string[] args)
        {
            Console.WriteLine("please enter the directory of the temp file: ");
            string direc = Console.ReadLine();
            string[] dirs = Directory.GetFiles(direc, "*.txt"); 
            Console.WriteLine("translating: " + dirs[0]);
            string path = dirs[0]; 
            Console.WriteLine(File.ReadAllText(path));
            int linenum = File.ReadLines(path).Count();
            using (StreamReader reader = new StreamReader(path))
            {
                for (int i = 0; i < linenum; i++)
                {
                    var line = reader.ReadLine();
                    var indent = false;
                    if (line.StartsWith("&"))
                    {
                        indent = true;
                        line = line.Remove(0, 1);
                    }
                    string output = Read(line);                 
                    if (indent == true)
                    {
                        output = "    " + output;
                    }

                    Console.WriteLine(output);

                    Add(output);
                }
            }

            var random = new Random();

            Console.ForegroundColor = (ConsoleColor)random.Next((int)ConsoleColor.Black, (int)ConsoleColor.Yellow);
            Console.WriteLine("Finished Translating!");
            Console.ResetColor(); 

        }

        private static string Read(string line)
        {
            string[] linearr = line.Split(" ");
            string key = linearr[0];
            int linecount = line.Length - 1;
            linearr = linearr.Where(w => w != linearr[0]).ToArray();
            string varname;
            string pyline;
            string str;
            switch (key)
            {
                case "VAR":
                    varname = linearr[0];
                    linearr = linearr.Where(w => w != linearr[0]).ToArray();
                    string temp = linearr[0];
                    if (temp == "&") // not string
                    {
                        linearr = linearr.Where(w => w != linearr[0]).ToArray();
                        pyline = varname + " = " + CheckVar(linearr);
                        return pyline;
                    }
                    else
                    {
                        pyline = varname + " = '" + CheckVar(linearr) + "'";
                        return pyline;
                    }

                case "INPUT":
                    varname = linearr[0];
                    linearr = linearr.Where(w => w != linearr[0]).ToArray();
                    str = CheckVar(linearr);
                    pyline = (varname + " = input(f'" + str + "')");
                    return pyline;

                case "OUTPUT":
                    str = CheckVar(linearr);
                    pyline = ("print(f'" + str + "')");
                    return pyline;

                case "MATH":
                    varname = linearr[0];
                    linearr = linearr.Where(w => w != linearr[0]).ToArray();
                    List<int> ilist = new List<int>();
                    List<string> slist = new List<string>();
                    foreach (var item in linearr)
                    {
                        int num = 0;
                        bool isNum = int.TryParse(item, out num);
                        if (isNum == true)
                        {
                            ilist.Add(num);
                        }
                        else
                        {
                            slist.Add(item);
                        }
                    }
                    int val = DoMath(ilist, slist);
                    pyline = (varname + " = " + val);
                    return pyline;

                case "IF":
                    str = CheckVar(linearr);
                    pyline = ("if (" + str + "):");
                    return pyline;

                case "ELSE":
                    str = CheckVar(linearr);
                    pyline = ("if (" + str + "):");
                    return pyline;

                case "ELIF":
                    str = CheckVar(linearr);
                    pyline = ("elif (" + str + "):");
                    return pyline;

                case "LOOP":
                    // loop types, while for
                    string ltype = linearr[0];
                    if (ltype == "WHILE") 
                    {
                        linearr = linearr.Where(w => w != linearr[0]).ToArray();
                        str = CheckVar(linearr);
                        pyline = "while (" + str + "):";
                        return pyline;
                    }else if (ltype == "FOR")
                    {
                        linearr = linearr.Where(w => w != linearr[0]).ToArray();
                        str = CheckVar(linearr);
                        pyline = "for (" + str + "):";
                        return pyline;
                    }
                    else
                    {
                        pyline = "# loop type not given";
                        return pyline;
                    }

                case "#":
                    str = CheckVar(linearr); // just to turn it into a string not to checkvar
                    pyline = "# " + str;
                    return pyline;

                default:
                    return null;
            }
        }

        private static void Add(string line)
        {

            if (path == "false")
            {
                Console.WriteLine("please specify an output path: ");
                string direc = Console.ReadLine();
                var file = (direc + "\\output.py");
                path = file;
            }

            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(line);
            }
        }

        private static string CheckVar(string[] arr)
        {

            string content = "";

            foreach (var word in arr)
            {
                if (arr.First() == word)
                {
                    content = word;
                }
                else
                {
                    if (word.StartsWith("&") && word.Length != 1)
                    {
                        string varname = word.Remove(0, 1);
                        content = content + " {" + varname + "}";
                    }
                    else
                    {
                        content = content + " " + word;
                    }
                }
            }
            return content;
        }

        private static int DoMath(List<int> ilist, List<string> slist)
        {
            int x = ilist.Count;
            int y = slist.Count;
            int temp1 = ilist[0];
            string temp2 = slist[0];
            int temp3 = ilist[1];
            int workingval = doop(temp1, temp3, temp2);
            ilist.RemoveAt(0);
            ilist.RemoveAt(0);
            slist.RemoveAt(0);
            try
            {
                while (x > 0)
                {
                    temp3 = ilist[0];
                    temp2 = slist[0];
                    workingval = doop(workingval, temp3, temp2);
                    ilist.RemoveAt(0);
                    slist.RemoveAt(0);
                }
                return workingval;
            }
            catch (Exception)
            {
                return workingval;
            }

            static int doop(int num1, int num2, string op)
            {
                int temp;

                switch (op)
                {
                    case "+":
                        temp = num1 + num2;
                        return temp;
                    case "-":
                        temp = num1 - num2;
                        return temp;
                    case "*":
                        temp = num1 * num2;
                        return temp;
                    case "/":
                        temp = num1 / num2;
                        return temp;
                    case "%":
                        temp = num1 % num2;
                        return temp;
                }
                return 0;
            }

        }
    }

}

