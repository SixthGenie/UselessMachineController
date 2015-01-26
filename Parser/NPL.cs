using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UselessMachineController
{
    public class cNPL
    {
        #region Tokens
        public const int t_NullToken = 0;
        public const int t_Underscore = 1;
        public const int t_Comment = 2;
        public const int t_WORK_Sym = 3;
        public const int t_LParen = 4;
        public const int t_RParen = 5;
        public const int t_LBrace = 6;
        public const int t_RBrace = 7;
        public const int t_Colon = 8;
        public const int t_Comma = 9;
        public const int t_SemiColon = 10;
        public const int t_Id = 11;
        public const int t_IntLiteral = 12;
        public const int t_RealLiteral = 13;
        public const int t_StringLiteral = 14;
        public const int t_Assignment = 15;
        public const int t_FullStop = 16;
        public const int t_Divide = 17;
        public const int t_Multiply = 18;
        public const int t_UnaryAdd = 19;
        public const int t_UnaryMinus = 20;
        public const int t_QuestionMark = 21;
        public const int t_STRUCT_Sym = 22;
        public const int t_TYPEDEF_Sym = 23;
        public const int t_Main_Sym = 24;
        public const int t_CharLiteral = 25;
        public const int t_SingleQuote = 26;
        public const int t_Quote = 27;
        public const int t_Ampersand = 28;
        public const int t_LBracket = 29;
        public const int t_RBracket = 30;
        public const int t_LessThan = 31;
        public const int t_GreaterThan = 32;
        public const int t_EqualTo = 33;
        public const int t_NotEqualTo = 34;
        public const int t_LessOrEqual = 35;
        public const int t_GreaterOrEqual = 36;
        public const int t_Or = 37;

        public const int t_Lid_Sym = 100;
        public const int t_Delay_Sym = 101;
        public const int t_Switch_Sym = 102;

        public const int t_EOF_Sym = 9999; // Must be the last in the list

        #endregion
        public const int GlobalScope = 0;
        public const int FunctionScope = 1;
        public const int LabelScope = 2;
        public const int LocalScope = 3;
        private Hashtable HT = new Hashtable();

        private class CNList
        {
            public string Literal;
            public int TokenValue;
            public int TokenKind;
        }
        private CNList LookupReservedWord(string Identifier)
        {
            if (HT.ContainsKey(Identifier))
                return (CNList)HT[Identifier];
            else
                return null;
        }
        public int CheckReservedWord(string Word2Check)
        {
            CNList np = LookupReservedWord(Word2Check.ToLower());
            if (np == null)
                return t_Id;
            else
                return np.TokenValue;
        }
        public void ClearReservedWords() { HT.Clear(); }
        public bool AddReservedWord(string Identifier, int Token)
        {
            if (LookupReservedWord(Identifier.ToLower()) == null)
            {
                CNList np = new CNList();
                np.Literal = Identifier.ToLower();
                np.TokenValue = Token;
                HT[Identifier] = np;
                return true;
            }
            else
            {
                System.Console.WriteLine("Reserved word already defined");
                return false;
            }
        }

    }

}
