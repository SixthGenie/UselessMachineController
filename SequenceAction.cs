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
using System.Linq;
using System.Text;

namespace UselessMachineController
{
    public class SequenceAction
    {
        public enum ActionType : byte { DelayAction = 1, LidAction = 2, SwitchAction = 4, };

        public int StartMs { get; set; }
        public int EndMs { get; set; }
        public int Duration { get; set; }
        public ActionType Action { get; set; }

        public SequenceAction(ActionType Action, int startMs, int endMs, int duration)
        {
            this.Action = Action; this.StartMs = startMs;
            this.EndMs = endMs;
            this.Duration = duration;
        }
    }
}
