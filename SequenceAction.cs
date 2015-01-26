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
