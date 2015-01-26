using System;
using System.Collections.Generic;
using System.Text;

namespace UselessMachineController
{
    public class cParser : cScanner
    {
        public void Shutdown()
        {
            ClearReservedWords();
            ClearSourceCode();
        }
        public string TokenBuffer;
        public void ExitParser(string ErrorMsg)
        {
            System.Console.WriteLine(ErrorMsg);
            ParserError();
        }
        public void ParserError()
        {
        }
        public void LexicalError(char UnexpectedChar)
        {
            ExitParser("Found unexpected character '" + UnexpectedChar + "in input. Parser aborting.");
        }
        public string GetTokenBuffer()
        {
            return TokenBuffer;
        }
        
        public bool SkipComments = true;

        public int Scanner()
        {
            char CurrentChar;

            TokenBuffer = "";
            if (IsAtEOF())
                return t_EOF_Sym;

            while (!IsAtEOF())
            {
                CurrentChar = GetNextChar();
                if (IsAtEOF())
                    break;

                switch (CurrentChar)
                {
                    case '#': // Preprocessor or possible include. Just ignore for now
                        while (CurrentChar != CR && CurrentChar != LF && CurrentChar != ';')
                            CurrentChar = GetNextChar();
                        break;
                    case ' ':
                    case TAB: break;
                    case CR:
                    case LF: // LinesProcessed++;
                        break;
                    case '.': TokenBuffer += CurrentChar; return t_FullStop;
                    case '(': TokenBuffer += CurrentChar; return t_LParen;
                    case '?': TokenBuffer += CurrentChar; return t_QuestionMark;
                    case ')': TokenBuffer += CurrentChar; return t_RParen;
                    case '{': TokenBuffer += CurrentChar; return t_LBrace;
                    case '}': TokenBuffer += CurrentChar; return t_RBrace;
                    case '<': if (Inspect() == '<')
                        {
                            Advance();
                            TokenBuffer += CurrentChar;
                            break;
                        }
                        else if (Inspect() == '=')
                        {
                            Advance();
                            TokenBuffer = "<=";
                            return t_LessOrEqual;
                        }
                        else
                        {
                            TokenBuffer += CurrentChar;
                            return t_LessThan;
                        }
                    case '>': if (Inspect() == '>')
                        {
                            Advance();
                            TokenBuffer += CurrentChar;
                            break;
                        }
                        else if (Inspect() == '=')
                        {
                            Advance();
                            TokenBuffer = ">=";
                            return t_GreaterOrEqual;
                        }
                        else
                        {
                            TokenBuffer += CurrentChar;
                            return t_GreaterThan;
                        }
                    case ';': TokenBuffer += CurrentChar; return t_SemiColon;
                    case ':': TokenBuffer += CurrentChar; return t_Colon;
                    case '=': if (Inspect() == '=')
                        {
                            Advance();
                            TokenBuffer = "==";
                            return t_EqualTo;
                        }
                        else
                        {
                            TokenBuffer += CurrentChar;
                            return t_Assignment;
                        }
                    case '*': TokenBuffer += CurrentChar; return t_Multiply;
                    case '|': TokenBuffer += CurrentChar; return t_Or;
                    case '_': TokenBuffer += CurrentChar; return t_Underscore;
                    case '+': TokenBuffer += CurrentChar; return t_UnaryAdd;
                    case '-': if (Inspect() >= '0' && Inspect() <= '9') { TokenBuffer += CurrentChar; CurrentChar = GetNextChar(); goto case '0'; }
                              TokenBuffer += CurrentChar;
                              return t_UnaryMinus;
                    case ',': TokenBuffer += CurrentChar; return t_Comma;
                    case '&': TokenBuffer += CurrentChar; return t_Ampersand;
                    case '!': if (Inspect() == '=')
                        {
                            Advance();
                            TokenBuffer = "!=";
                            return t_NotEqualTo;
                        }
                        else
                        {
                            TokenBuffer += CurrentChar;
                            break;
                        }
                    case '[': TokenBuffer += CurrentChar; return t_LBracket;
                    case ']': TokenBuffer += CurrentChar; return t_RBracket;
                    case '"': do
                        {
                            if (Inspect() == '\\')
                            {
                                CurrentChar = GetNextChar();
                                switch (Inspect())
                                {
                                    case '"': CurrentChar = GetNextChar();
                                        TokenBuffer += CurrentChar;

                                        break;
                                    default: // No known escape char. Put it back in string
                                        TokenBuffer += '\\';
                                        break;
                                }
                            }
                            if (Inspect() != '"')
                                CurrentChar = GetNextChar();
                            TokenBuffer += CurrentChar;
                        } while (Inspect() != '"');
                        Advance();
                        return t_StringLiteral;
                    case '/': if (Inspect() == '*')
                        {
                            TokenBuffer += CurrentChar;
                            Advance();
                            do
                            {
                                if ((CurrentChar == '*' && Inspect() != '/') || CurrentChar != '*')
                                {
                                    CurrentChar = GetNextChar();
                                }
                                else
                                    break;
                                TokenBuffer += CurrentChar;
                            } while (true);
                            Advance();
                            if (!SkipComments)
                                return t_Comment;
                            else
                            {
                                TokenBuffer = "";
                                break;
                            }
                        }
                        else if (Inspect() == '/')
                        {
                            while (CurrentChar != CR && CurrentChar != LF)
                            {
                                TokenBuffer += CurrentChar;
                                CurrentChar = GetNextChar();
                            }
                            if (!SkipComments)
                                return t_Comment;
                            else
                            {
                                TokenBuffer = "";
                                break;
                            }
                        }
                        else
                            return t_Divide;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': TokenBuffer += CurrentChar;
                        do
                        {
                            if (Inspect() >= '0' && Inspect() <= '9')
                            {
                                TokenBuffer += Inspect();
                                Advance();
                            }
                            else if (Inspect()=='.')
                            {
                                CurrentChar = GetNextChar(); 
                                TokenBuffer += CurrentChar;
                                CurrentChar = GetNextChar();
                                
                                goto case '0';
                            }
                            else
                            {
                                if (TokenBuffer.Contains("."))
                                    return t_RealLiteral;
                                else
                                    return t_IntLiteral;
                            }
                        } while (true);
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                    case 'g':
                    case 'h':
                    case 'i':
                    case 'j':
                    case 'k':
                    case 'l':
                    case 'm':
                    case 'n':
                    case 'o':
                    case 'p':
                    case 'q':
                    case 'r':
                    case 's':
                    case 't':
                    case 'u':
                    case 'v':
                    case 'w':
                    case 'x':
                    case 'y':
                    case 'z':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'H':
                    case 'I':
                    case 'J':
                    case 'K':
                    case 'L':
                    case 'M':
                    case 'N':
                    case 'O':
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                    case 'T':
                    case 'U':
                    case 'V':
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z': TokenBuffer += CurrentChar;
                        do
                        {
                            if ((Inspect() >= 'a' && Inspect() <= 'z') ||
                                (Inspect() >= 'A' && Inspect() <= 'Z') ||
                                (Inspect() >= '0' && Inspect() <= '9') ||
                                Inspect() == '_')
                            {

                                TokenBuffer += Inspect();
                                Advance();
                            }
                            else
                            {
                                return CheckReservedWord(TokenBuffer);
                            }
                        } while (1 == 1);
                    default: LexicalError(CurrentChar);
                        break;

                }
            }
            return t_EOF_Sym;
        }
    }
}
