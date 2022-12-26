using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TinyLanguageCompilerProject
{
    public enum TokenClass
    {
        DataTypeInt, DataTypeFloat, DataTypeString, ReservedWordRead, ReservedWordWrite, ReservedWordMain,
        ReservedWordRepeat, ReservedWordUntil, ReservedWordIf, ReservedWordElseIf, ReservedWordElse, ReservedWordThen, ReservedWordReturn, ReservedWordEndl, ReservedWordEnd, Dot,
        Semicolon, Comma, LeftParentheses, RightParentheses,
        Equal, NotEqualOp, LessThanOp, GreaterThanOp, AndOp, OrOp,
        PlusOp, MinusOp, MultiplyOp, DivideOp, AssignOp, Identifier, Number, Comment, LCurlyBraces, RCurlyBraces, StringLiteral

    }

    public class Token
    {
        public string Lexeme;
        public TokenClass TokenType;
    }
    public class Scanner
    {

        // List to Store Tokens Generated
        public List<Token> Tokens= new List<Token>();

        //  List to store errors detected by scanner
        public List<string> ErrorsList = new List<string>();

        // Dictionary to store each reserved word linked with it's token
        Dictionary<string, TokenClass> reservedWords = new Dictionary<string, TokenClass>();

        // Dictionary to store each Operator linked with it's token
        Dictionary<string, TokenClass> operators = new Dictionary<string, TokenClass>();

        // Identifier Regex
        private readonly Regex idenifierRx = new Regex(@"(^[a-zA-Z])([0-9]|[a-zA-Z])*$",
            RegexOptions.Compiled);

        // Literal Strings Regex  (Ex: "test")
        private readonly Regex literalStringRx = new Regex("\"([^*]|[\r\n]|(\"+([^*/]|[\r\n])))*\"+");

        // Comments Regex ( will be used to ignore comments ) 
        private readonly Regex commentRx = new Regex(@"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/");

        // Numbers Regex (Decimals & Integers )
        private readonly Regex numberRx = new Regex(@"([0-9])+(\.[0-9]+)?$");

        /* Invalid identifier Regex
         (will be used to detect identifiers start with a number 
             to add them to errors list) */
        private readonly Regex _invalidIdentifierRx = new Regex(@"([0-9])([a-zA-Z])$",
            RegexOptions.Compiled);

        /* Invalid float Regex
         (will be used to detect floats start with a dot 
             to add them to errors list) */
        private readonly Regex _invalidFloatRx = new Regex(@"^(?![0-9])(\.)([0-9]*)$", 
            RegexOptions.Compiled);

        public Scanner(string lexeme)
        {
            //Initialize Reserved Words list and Operators list
            InitializeLists();
        }
        public void Scan(string sourceCode)
        {
            //Clear all lists
            Tokens.Clear();
            ErrorsList.Clear();
            //Variable to store last index
            var lastIndex = -1;

            /*
             * In case that the source code is only one character there is no need to
             * use loop, Just find it's token
             */
            if (sourceCode.Length==1)
            {
                FindTokenClass(sourceCode);
            }
            else
            /*
             * Loop on Each char in the source code
             */
                for (var i = 0; i < sourceCode.Length - 1;)
                {
                    // Variables to store current char and current lexeme
                    var currentChar = sourceCode[i];
                    var currentLexeme = currentChar.ToString();

                    // Var to store index of next char
                    var j = i + 1;

                    // If current char is a delimiter then skip it
                    if (currentChar == ' ' || currentChar == '\r' ||
                        currentChar == '\n' || currentChar == '\t')
                    {
                        //Move to next index
                        i = j;
                        lastIndex = j;
                        //Skip the delimters
                        continue;
                    }

                    //If current char is Letter then get then loop to the whole lexeme
                    if (char.IsLetter(currentChar))
                    {
                        currentChar = sourceCode[j];

                        /*
                        * While currentChar is a 'char' or 'Number'
                        *  add it to currentLexeme var 
                        */
                        while ((currentChar >= 'a' && currentChar <= 'z') ||
                               (currentChar >= 'A' && currentChar <= 'Z') ||
                               numberRx.IsMatch(currentChar.ToString()))
                        {
                            currentLexeme += currentChar.ToString();
                            j++;
                            if (j < sourceCode.Length)
                                currentChar = sourceCode[j];
                            else
                                break;
                        }
                    }

                    /*
                     * If currentChar is a number
                     * then loop to get all digits of the number in source code
                     */
                    else if (numberRx.IsMatch(currentChar.ToString()))
                    {
                
                        currentChar = sourceCode[j];
                        //Check if currentChar is number or '.' dot (Decimal point)
                        while (numberRx.IsMatch(currentChar.ToString()) || currentChar == '.')
                        {
                       
                            currentLexeme += currentChar.ToString();
                       
                            j++;
                            if (j < sourceCode.Length)
                                currentChar = sourceCode[j];
                            else
                                break;
                        }
                    }

                    /*
                     * Check if currentChar is a start of a comment then loop on the source code till
                     * the end of the comment and store it in currentLexeme var
                     */
                    else if (currentChar == '/')
                    {
                        currentChar = sourceCode[j];
                        if (currentChar == '*')
                        {
                            currentLexeme += currentChar.ToString();
                            j++;
                            currentChar = sourceCode[j];
                            var k = j + 1;
                            while (j < sourceCode.Length && k < sourceCode.Length)
                            {
                                var nextChar = sourceCode[k];
                                if (currentChar == '*' && nextChar == '/')
                                {
                                    j += 2;
                                    currentLexeme += "*/";
                                    break;
                                }
                                else
                                {
                                    currentLexeme += currentChar.ToString();
                                    j++;
                                    k++;
                                    currentChar = nextChar;
                                }
                            }
                           
                        }
                    }
                    /*
                     * Check if currentChar is a start of a Literal string and loop on the source code till
                     * the end of the string and store it in currentLexeme var
                     */
                    else if (currentChar == '"')
                    {
                        currentChar = sourceCode[j];
                        while (currentChar != '"')
                        {
                            currentLexeme += currentChar.ToString();
                            j++;
                            if (j < sourceCode.Length)
                                currentChar = sourceCode[j];
                            else
                                break;
                        }
                        if (j < sourceCode.Length && currentChar == '"')
                        {
                            currentLexeme += currentChar.ToString();
                            j++;
                        }
                    }
                    else
                    {
                    
                        var nextChar = sourceCode[j];
                        // Checking for invalid usage of operators 
                        if (currentChar == '/' && nextChar == '/' ||
                            currentChar == '-' && nextChar == '-' ||
                            currentChar == '+' && nextChar == '+' ||
                            currentChar == '>' && nextChar == '=' ||
                            currentChar == '>' && nextChar == '>' ||
                            currentChar == '>' && nextChar == '<' ||
                            currentChar == '<' && nextChar == '<' ||
                            currentChar == '=' && nextChar == '=' ||
                            currentChar == '<' && nextChar == '=')
                        {
                            currentLexeme += nextChar.ToString();
                            j++;
                        }

                        // checking the operators
                        if (currentChar == '&' && nextChar == '&' ||
                            currentChar == '|' && nextChar == '|' ||
                            currentChar == '<' && nextChar == '>' ||
                            currentChar == ':' && nextChar == '=')
                        {
                            currentLexeme += nextChar.ToString();
                            j++;
                        }
                    }
                    /*
                     * Check if currentLexeme is invalid identifier
                     * ( starts with a number )
                     * or invalid float number
                     * ( starts with a decimal point )
                     */
                    if (_invalidFloatRx.IsMatch(currentLexeme)||_invalidIdentifierRx.IsMatch(currentLexeme+currentChar))
                    {
                        /*
                         * Loop to store the whole invalid lexeme in currentLexeme var
                         * and add it to errors list
                         */
                        while (currentChar != ' ')
                        {
                            if (currentChar != '.' )
                            {
                                if (numberRx.IsMatch(currentChar.ToString()) || idenifierRx.IsMatch(currentChar.ToString()))
                                currentLexeme += currentChar.ToString();
                                else
                                    FindTokenClass(currentChar.ToString());
                                j++;
                            }

                            if (j < sourceCode.Length && currentChar!= '\n' &&  currentChar != ' ' && currentChar != '=')
                            {
                                currentChar = sourceCode[j];
                            }
                            else
                                break;
                        }

                        if (currentLexeme.Contains("\n")&& currentLexeme.StartsWith("."))
                        {
                            var a = currentLexeme.Split('\n');
                            ErrorsList.Add(a[0]);
                            int cnt = 0;
                            foreach (var s in a)
                            {
                                if (cnt == 0)
                                {
                                    cnt++;
                                    continue;
                                }
                                else
                                {
                                    currentLexeme = s;
                                    FindTokenClass(currentLexeme);
                                    
                                }
                            }
                            
                        }
                        else
                        ErrorsList.Add(currentLexeme);
                    }
                    //else if current lexeme is valid then find it's token
                    else
                        FindTokenClass(currentLexeme);
                    //Increase the counter
                    i = j;
                    lastIndex = j;
                }
            //Special if condition for the last char in the source code
            if ( sourceCode.Length!=0 && lastIndex == sourceCode.Length - 1)
                FindTokenClass(sourceCode[lastIndex].ToString());
         
        }
        /*
         * Function that finds a token for a given lexeme
         * then create a token object and store it in tokens list
         */
        private void FindTokenClass(string lex)
        {
            //If lexeme length if zero them exit
            if (lex.Length == 0)
                return;

            //Token class object to store the lexeme token
            TokenClass tokenClass;

            //Token object that stores the lexeme and will store the token type
            var token = new Token { Lexeme = lex };

            //Check if the lexeme is a reserved word
            if (reservedWords.ContainsKey(token.Lexeme))
            {
                tokenClass = reservedWords[token.Lexeme];
                token.TokenType = tokenClass;
                Tokens.Add(token);
            }
            //Check if the lexeme is a comment
            else if (commentRx.IsMatch(token.Lexeme))
            {
                tokenClass = TokenClass.Comment;
                token.TokenType = tokenClass;
            }

            //Check if the lexeme is a identifier
            else if (idenifierRx.IsMatch(token.Lexeme))
            {
                tokenClass = TokenClass.Identifier;
                token.TokenType = tokenClass;
                Tokens.Add(token);
            }

            //Check if the lexeme is a number
            else if (numberRx.IsMatch(token.Lexeme))
            {
                tokenClass = TokenClass.Number;
                token.TokenType = tokenClass;
                Tokens.Add(token);
            }

            //Check if the lexeme is an operator
            else if (operators.ContainsKey(token.Lexeme))
            {
                tokenClass = operators[token.Lexeme];
                token.TokenType = tokenClass;
                Tokens.Add(token);
            }

            //Check if the lexeme is a literal string (Ex: "test")
            else if (literalStringRx.IsMatch(token.Lexeme))
            {
                tokenClass = TokenClass.StringLiteral;
                token.TokenType = tokenClass;
                Tokens.Add(token);
            }
           
            //In case that the lexeme doesn't match any condition,
            //add it to errors list
            else
                ErrorsList.Add(lex);
        }

        //Initialize reservedWords list and operators list
        private void InitializeLists()
        {
            reservedWords.Add("int", TokenClass.DataTypeInt);
            reservedWords.Add("float", TokenClass.DataTypeFloat);
            reservedWords.Add("string", TokenClass.DataTypeString);
            reservedWords.Add("read", TokenClass.ReservedWordRead);
            reservedWords.Add("write", TokenClass.ReservedWordWrite);
            reservedWords.Add("repeat", TokenClass.ReservedWordRepeat);
            reservedWords.Add("until", TokenClass.ReservedWordUntil);
            reservedWords.Add("if", TokenClass.ReservedWordIf);
            reservedWords.Add("elseif", TokenClass.ReservedWordElseIf);
            reservedWords.Add("else", TokenClass.ReservedWordElse);
            reservedWords.Add("then", TokenClass.ReservedWordThen);
            reservedWords.Add("return", TokenClass.ReservedWordReturn);
            reservedWords.Add("endl", TokenClass.ReservedWordEndl);
            reservedWords.Add("end", TokenClass.ReservedWordEnd);
            reservedWords.Add("main", TokenClass.ReservedWordMain);

            operators.Add(".", TokenClass.Dot);
            operators.Add(";", TokenClass.Semicolon);
            operators.Add(",", TokenClass.Comma);
            operators.Add("(", TokenClass.LeftParentheses);
            operators.Add(")", TokenClass.RightParentheses);
            operators.Add("{", TokenClass.LCurlyBraces);
            operators.Add("}", TokenClass.RCurlyBraces);
            operators.Add("=", TokenClass.Equal);
            operators.Add("<", TokenClass.LessThanOp);
            operators.Add(">", TokenClass.GreaterThanOp);
            operators.Add("<>", TokenClass.NotEqualOp);
            operators.Add("+", TokenClass.PlusOp);
            operators.Add("-", TokenClass.MinusOp);
            operators.Add("–", TokenClass.MinusOp);
            operators.Add("*", TokenClass.MultiplyOp);
            operators.Add("/", TokenClass.DivideOp);
            operators.Add("||", TokenClass.OrOp);
            operators.Add("&&", TokenClass.AndOp);
            operators.Add(":=", TokenClass.AssignOp);
        }
    }
}