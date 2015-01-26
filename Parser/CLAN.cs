using System;
using System.Collections.Generic;
using System.Text;

namespace UselessMachineController
{
    public class CLAN:cParser
    {
        public CLAN()
        {
            AddReservedWord("lid", t_Lid_Sym);
            AddReservedWord("delay", t_Delay_Sym);
            AddReservedWord("switch", t_Switch_Sym);
            sysInstr = new List<cInstructionPtr>();
            userInstr = new List<cInstructionPtr>();
            groupInstr = new List<cInstructionPtr>();
            scriptInstr = new List<cInstructionPtr>();
        }
        public string token2string(int token)
        {
            switch (token)
            {
                case t_Lid_Sym: return "lid";
                case t_Delay_Sym: return "delay";
                case t_Switch_Sym: return "switch";
                case t_LParen: return "(";
                case t_RParen: return ")";
                case t_IntLiteral: return "integer";
                case t_Comma: return ",";
                case t_Id: return "lid/delay/switch action";
                default: return "unknown";
            }
        }

        public class cLexeme
        {
            public int TokenId;
            public int OperatorKind;
            public int Operator;
            public string TokenVal;
        }
        private enum eVariableKind { StringVal, IntegerVal, RealVal, FuncVal, ColorVal, LabelVal, FontVal };
        private class cClanValType
        {
            public eVariableKind ValType;
            public string stringValue;
            public int integerValue;
            public double realValue;
            public string FuncValue;
        }
        public List<cInstructionPtr> sysInstr, userInstr, groupInstr, scriptInstr;

        private class cClanVarType
        {
            public string VarName;
            public cClanValType ClanVal;
        }
        public class cInstructionPtr
        {
            public int InstructionNum;
            public cLexeme Lexeme;
        }
        private class cSetElement
        {
            public int Token;
        }
    }
}
