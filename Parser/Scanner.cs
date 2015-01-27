/*
COPYRIGHT NOTICE for the  UseLessMachineController

Copyright (c) 2014-2015 Kjetil Næss.

This program is free software for personal use only. You may modify and/or distribute as you like as long as this message
is included in the distribution.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY whatsoever. It if works it works, if it doesn't, you have the source to fix it
(remember to let me know so that I can fix it on my end).
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace UselessMachineController
{
    public class cScanner : cNPL
    {
        public const char TAB = '\t';
        public const char CR = '\r';
        public const char LF = '\n';

        private char[] SourceCode;
        private int MaxBufferPointer, BufferPointer, LinesProcessed;
        public bool IsAtEOF()
        {
            return BufferPointer == MaxBufferPointer;
        }
        public string Source { get { return new string(SourceCode); } }
        public char GetNextChar()
        {
            if (BufferPointer == MaxBufferPointer)
                return (char)255;
            else
            {
                char c;
                c = SourceCode[BufferPointer++];
                if (c == 10)
                    LinesProcessed++;
                return c;
            }
        }
        public char GetSourceCode(int Position)
        {
            return Inspect(Position);
        }
        public void InsertCode(string Code)
        {
            int InsertPoint = BufferPointer;

            char[] NewSource = new char[SourceCode.Length+Code.Length];
            
            Array.Copy(SourceCode, 0, NewSource, 0, InsertPoint); // Copy original upto insertion point
            Array.Copy(Code.ToCharArray(),0,NewSource,InsertPoint, Code.Length);

            InsertPoint += Code.Length;
            MaxBufferPointer += Code.Length;
            Array.Copy(SourceCode, BufferPointer, NewSource, InsertPoint, SourceCode.Length - BufferPointer);

            SourceCode=NewSource;
        }
        public int GetCurrentPosition()
        {
            return BufferPointer;
        }

        public void Advance()
        {
            if (BufferPointer < MaxBufferPointer)
                BufferPointer++;
        }
        public char Inspect(int i)
        {
            return SourceCode[i];
        }
        public void ClearSourceCode() { SourceCode = null; }
        public char Inspect()
        {
            return Inspect(BufferPointer);
        }
        public bool LoadProgram(string ProgramName)
        {
            SourceCode = ProgramName.ToCharArray();

            MaxBufferPointer = SourceCode.Length;
            BufferPointer = 0;

            return true;
        }
    }
}
