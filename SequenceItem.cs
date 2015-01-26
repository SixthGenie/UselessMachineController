using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UselessMachineController
{
    public class SequenceItem
    {
        public string SequenceName { get; set; }
        public int DependsUpon { get; set; }
        
        public bool Enabled { get; set; }
        public string Sequence { get; set; }
        public byte[] SequenceData { get;  set; }
        public int AngerLevel { get; set; }

        private string lastParserError = "";

        CLAN clan = new CLAN();
        public SequenceItem()
        {
            DependsUpon = 0;
            Enabled = true;
        }
        public int SequenceSize
        {
            get
            {
                return SequenceData.Length +
                    //1 + // Enabled
                    1; // Anger level
                    //1;  // Can only be executed after
            }
        }
        public void Decompile()
        {
            int index = 0;
            byte[] sequence = SequenceData;
            string Source = "";
            while (index < sequence.Length)
            {
                switch (sequence[index] & 0x0f) // Four lower bits denotes the action
                {
                    case 1://(byte)SequenceKind.DelayAction:
                        {
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            Source += string.Format("Delay({0})\r\n", duration);
                            index++;
                        }
                        break;
                    case 2://(byte)SequenceKind.LidAction:
                        {
                            int fromPos = sequence[++index] * 10;
                            int toPos = sequence[++index] * 10;
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            Source += string.Format("Lid({0},{1},{2})\r\n", fromPos, toPos, duration);
                            index++;
                        }
                        break;
                    case 4://(byte)SequenceKind.SwitchAction:
                        {
                            int fromPos = sequence[++index] * 10;
                            int toPos = sequence[++index] * 10;
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            Source += string.Format("Switch({0},{1},{2})\r\n", fromPos, toPos, duration);
                            index++;
                        }
                        break;
                }
            }
            Sequence = Source;
        }

        public override string ToString()
        {
            return SequenceName;
        }
        bool Expect(char expect, string t)
        {
            return (t[0] == expect);
        }
        string Match(int token)
        {
            int t = clan.Scanner();
            if (t != token && t != CLAN.t_Comment)
                throw new Exception(string.Format("Expected {0}, got {1}",clan.token2string(token),clan.token2string(t)));
            else
                return clan.TokenBuffer;
        }
        public bool Parse()
        {
            clan.LoadProgram(Sequence);
            string Token = "";
            string ParsedSoFar = "";
            int t = clan.Scanner();
            List<byte> sequenceData = new List<byte>();
            while (t != CLAN.t_RParen && t != 9999)
            {
                Token = clan.TokenBuffer;
                switch (Token.ToLower())
                {
                    case "lid":
                        {
                            ParsedSoFar = Token;
                            var c = lidAction(ref ParsedSoFar);
                            if (c != null)
                            {
                                sequenceData.AddRange(c);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Error parsing lid command {0}. {1}",ParsedSoFar, lastParserError));
                                return false;
                            }
                        } 
                        break;
                    case "delay":
                        {
                            var c = delayAction(ref ParsedSoFar);
                            if (c != null)
                            {
                                sequenceData.AddRange(c);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Error parsing delay command {0}. {1}",ParsedSoFar, lastParserError));;
                                return false;
                            }
                        } break;
                    case "switch":
                        {
                            var c = switchAction(ref ParsedSoFar);
                            if (c != null)
                            {
                                sequenceData.AddRange(c);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Error parsing lid command {0}. {1}", ParsedSoFar, lastParserError));
                                return false;
                            }
                        } break;
                    default: MessageBox.Show(string.Format("Unknown action {0}", Token)); return false;
                }
                t = clan.Scanner();
                ParsedSoFar = "";
            }
            SequenceData = sequenceData.ToArray();
            return true;
        }
        int readNextTokenInt()
        {
            int t = CLAN.t_Comment;
            while (t == CLAN.t_Comment) t = clan.Scanner();
            if (t != CLAN.t_IntLiteral)
                throw new Exception("Expected integer literal");
            return Convert.ToInt32(clan.TokenBuffer);
        }
        string readNextTokenString()
        {
            int t = clan.Scanner();
            return clan.TokenBuffer;
        }
        List<byte> lidAction(ref string ParsedSoFar)
        {
            try
            {
                Match(CLAN.t_LParen);
                ParsedSoFar += clan.TokenBuffer;
                int fromPos = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_Comma);
                ParsedSoFar += clan.TokenBuffer;
                int toPos = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_Comma);
                ParsedSoFar += clan.TokenBuffer;
                int duration = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_RParen);
                ParsedSoFar += clan.TokenBuffer;
                System.Console.WriteLine("Parsed lidAction({0},{1},{2})", fromPos, toPos, duration);
                List<byte> b = new List<byte>();
                b.Add((byte)SequenceAction.ActionType.LidAction);
                b.Add((byte)(fromPos / 10));
                b.Add((byte)(toPos / 10));
                if (duration == 0)
                    duration = 1;
                byte lowByte = (byte)duration;
                byte highByte = (byte)(duration >> 8);
                b.Add(lowByte);
                b.Add(highByte);
                return b;
            }
            catch(Exception e)
            {
                lastParserError = e.Message;
                return null;
            }
        }
        List<byte> delayAction(ref string ParsedSoFar)
        {
            try
            {
                Match(CLAN.t_LParen);
                ParsedSoFar += clan.TokenBuffer;
                int duration = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_RParen);
                ParsedSoFar += clan.TokenBuffer;
                
                List<byte> b = new List<byte>();
                b.Add((byte)SequenceAction.ActionType.DelayAction);
                if (duration == 0)
                    duration = 1;
                byte lowByte = (byte)duration;
                byte highByte = (byte)(duration >> 8);
                System.Console.WriteLine("Parsed delayAction({0})", duration);
                b.Add(lowByte);
                b.Add(highByte);
                return b;
            }
            catch (Exception e)
            {
                lastParserError = e.Message;
                return null;
            }
        }
        List<byte> switchAction(ref string ParsedSoFar)
        {
            try
            {
                Match(CLAN.t_LParen);
                ParsedSoFar += clan.TokenBuffer;
                int fromPos = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_Comma);
                ParsedSoFar += clan.TokenBuffer;
                int toPos = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_Comma);
                ParsedSoFar += clan.TokenBuffer;
                int duration = readNextTokenInt();
                ParsedSoFar += clan.TokenBuffer;
                Match(CLAN.t_RParen);
                ParsedSoFar += clan.TokenBuffer;
                System.Console.WriteLine("Parsed switchAction({0},{1},{2})", fromPos, toPos, duration);

                List<byte> b = new List<byte>();
                b.Add((byte)SequenceAction.ActionType.SwitchAction);
                b.Add((byte)(fromPos / 10));
                b.Add((byte)(toPos / 10));
                if (duration == 0)
                    duration = 1;
                byte lowByte = (byte)duration;
                byte highByte = (byte)(duration >> 8);
                b.Add(lowByte);
                b.Add(highByte);
                return b;
            }
            catch (Exception e)
            {
                lastParserError = e.Message;
                return null;
            }
        }
    }
}
