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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;
using System.IO.Ports;
using System.Threading;

namespace UselessMachineController
{
    public class Box
    {
        int boxXpos = 130;
        int boxYpos = 605;

        const int servoSpeed60deg_ms = (int)(0.1 * 1000);
        const float servoSpeed1deg_ms = (float)servoSpeed60deg_ms / 60.0f;
        public bool switchState = false;
        public int lidCurrentPos, switchCurrentPos;
        int lidTargetPos, switchOnOffTargetPos;
        int lidStartPos, lidEndPos;
        int switchOnOffStartPos, switchOnOffEndPos;
        public delegate void refreshHandler();
        public event refreshHandler Refresh;
        Queue<SequenceAction> ActionsQueue = new Queue<SequenceAction>();
        SequenceAction currentAction;
        int lidMoveDirection, switchOnOffMoveDirection;
#if usingLidTicker
        public float lidRotateAngle, switchRotateAngle;
        public float lidStartAngle, lidEndAngle;
        System.Timers.Timer lidTicker = new System.Timers.Timer();
        float lidTargetAngle, switchTargetAngle;

        public float switchOnOffStartAngle, switchOnOffEndAngle;
#endif
        public const int lidMinPos = 900;
        public const int lidMaxPos = 1400;
        public const int switchMinPos = 1050;
        public const int switchMaxPos = 2200;

        public const int lidServoStartPos = 900;
        public const int switchServoStartPos = 1050;
        Brush lidBrush = new SolidBrush(Color.FromArgb(200, 88, 88, 88));
        Brush lidArmBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 200));
        Brush switchArmBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        const int startMoveLidAtDegree = 20;
        public Box()
        {
#if usingLidTicker
            lidTicker.Elapsed += new ElapsedEventHandler(lidTicker_Tick);
            lidTicker.Enabled = false;
#endif
            switchState = false;
            lidCurrentPos = 900;
            switchCurrentPos = 1050;
 Thread t = new Thread(Ticker_Thread);
            t.Name = "Ticker thread";
            t.Start();
        }

        int msecs2deg(int msecs)
        {
            return map(msecs, 800,2400, 0, 180);
        }
        int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        int c2p(Graphics g, double Centimeter)
        {
            double pixel = -1;
            pixel = (Centimeter * g.DpiY) / 2.5399999d;
            return (int)pixel;
        }
        void drawBase(Graphics g, Pen p)
        {
            int xo = boxXpos - 30;// 100;
            g.FillPolygon(lidBrush, new Point[]{new Point( xo + 100, boxYpos - 100),new Point( xo + 100, boxYpos + 150),
                new Point(xo + 600, boxYpos + 150),
//new Point( xo + 100, boxYpos - 100),
            new Point(xo + 600, boxYpos - 100)
            });
            //return;
            g.DrawLine(p, xo + 100, boxYpos - 100, xo + 100, boxYpos + 150);
            g.DrawLine(p, xo + 100, boxYpos + 150, xo + 600, boxYpos + 150);
            g.DrawLine(p, xo + 600, boxYpos + 150, xo + 600, boxYpos - 100);
            g.DrawLine(p, xo + 100, boxYpos - 100, xo + 600, boxYpos - 100);
        }
        void drawLid(Graphics g, Pen p)
        {
            int xo = boxXpos - 30;// 100;

            var gp = new GraphicsPath();
            gp.AddLines(new Point[]{new Point(xo + 100, boxYpos - 142),
                new Point(xo + 100, boxYpos - 102),new Point(xo + 600,boxYpos - 102),
                new Point(xo + 600, boxYpos - 142)});
            Point start = new Point(xo + 100, boxYpos - 142);
            Point control1 = new Point(xo + 225, boxYpos - 200);
            Point control2 = new Point(xo + 475, boxYpos - 200);
            Point end1 = new Point(xo + 600, boxYpos - 142);
            Point[] bezierPoints = { start, control1, control2, end1, };
            gp.AddBezier(start, control1, control2, end1);
            g.FillPath(lidBrush, gp);
        }
        void drawLidArm(Graphics g, Pen p)
        {
            int yo = 580;
            PointF[] pointList = {new PointF(0,0),
                                  new PointF(c2p(g,6.5),0),
                                  new PointF(c2p(g,6.5),-c2p(g,1.8)),
                                  new PointF(c2p(g,1.2),-c2p(g,1.8)),
                                  new PointF(0,-c2p(g,0.5))
                                 };
            var t = g.Transform;
            g.ResetTransform();
            using (Matrix m = new Matrix())
            {
                var angle = msecs2deg(lidCurrentPos)-11;
                m.RotateAt(angle, new PointF(450,yo+20));
                g.Transform = m;
                g.TranslateTransform(250, yo);
                g.FillPolygon(lidArmBrush, pointList);
            }
            g.Transform = t;
        }
        void drawSwitchArm(Graphics g)
        {

            Pen p = new Pen(Color.FromArgb(255, 255, 0, 0));
            PointF[] pointList = {new PointF(c2p(g,4.7),-c2p(g,0.5)),new PointF(c2p(g,5.6),-c2p(g,0.5)),new PointF(c2p(g, 6.7), -c2p(g, 4.9)),
                                 new PointF(c2p(g,5.4), -c2p(g, 5.15)),new PointF(c2p(g, 4.1), -c2p(g, 5.05)),new PointF(c2p(g, 2.9), -c2p(g, 4.7)),
                                 new PointF(c2p(g,1.7),-c2p(g,3.9)),new PointF(c2p(g,0.9),-c2p(g,2.9)),new PointF(c2p(g,0.3),-c2p(g,1.7)),
                                 new PointF(c2p(g,0.1),-c2p(g,0.3)),new PointF(c2p(g,0.1),-c2p(g,0.1)),
                                 new PointF(c2p(g,0.8),-c2p(g,0.3)),new PointF(c2p(g,0.9),-c2p(g,1.35)),
                                 new PointF(c2p(g,1.4),-c2p(g,2.4)),new PointF(c2p(g,2.1),-c2p(g,3.25)),new PointF(c2p(g,2.9),-c2p(g,3.9)),
                                 new PointF(c2p(g,4.15),-c2p(g,4.2)),
                                 };

            g.FillPolygon(switchArmBrush, pointList);

            g.ResetTransform();
        }
        void drawSwitch(Graphics g, bool state)
        {
            int xo = boxXpos - 25;// 105;
            int yo = boxYpos - 10;// 340;
            
            PointF[] pl = {new PointF(c2p(g,2.5),-c2p(g,0.2)),new PointF(0,-c2p(g,0.4)),
                           new PointF(0,c2p(g,0.4)),
                           new PointF(c2p(g,2.5),c2p(g,0.2)),

                          };
            float angle = -15;
            if (state)
            {
                angle = 15;
            }
            else
            {
                angle = -15;
            }
            using (Matrix m = new Matrix())
            {
                m.RotateAt(angle, new PointF(xo + c2p(g, 2.5), yo));
                g.Transform = m;
                g.TranslateTransform(xo, yo);
                g.DrawPolygon(Pens.Black, pl);

            }
        }
        public void RenderObject(System.Drawing.Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            using (Matrix m = new Matrix())
            {
                var angle = 110 - (msecs2deg(switchCurrentPos) - 28);
                m.RotateAt(angle, new PointF(boxXpos + c2p(g, 5.0), boxYpos));
                g.Transform = m;
                g.TranslateTransform(boxXpos, boxYpos);

                drawSwitchArm(g);
                g.ResetTransform();
            }
            drawLidArm(g, Pens.Black);

            drawBase(g, Pens.Black);

            using (Matrix m = new Matrix())
            {
                var angle = (msecs2deg(lidCurrentPos)-11)*0.5f;
                if (angle > startMoveLidAtDegree)
                    angle -= startMoveLidAtDegree;
                else
                    angle = 0;
                m.RotateAt(angle, new PointF(boxXpos + 600,boxYpos-102));// 248));
                g.Transform = m;
                drawLid(g, Pens.Black);
                g.ResetTransform();
            }

            drawSwitch(g, switchState);
        }
#if usingLidTicker
        public void sweepLid(int startAngle, int endAngle, int msecs)
        {
            lidStartAngle = startAngle;
            lidEndAngle = endAngle;
            lidRotateAngle = startAngle;
            var numSteps = (endAngle - startAngle) * 2;
            int tickInterval = msecs / numSteps;
            lidTicker.Interval = tickInterval;
            lidTicker.Start();
        }
#endif
        SequenceAction lidAction(int start, int end, int duration)
        {
            return new SequenceAction(SequenceAction.ActionType.LidAction, start, end, duration);
        }
        SequenceAction delayAction(int msecs)
        {
            return new SequenceAction(SequenceAction.ActionType.DelayAction, 0, 0, msecs);
        }
        SequenceAction switchAction(int start, int end, int duration)
        {
            return new SequenceAction(SequenceAction.ActionType.SwitchAction, start, end, duration);
        }
        public void Sequence0()
        {
            switchState = true;
            currentAction = null;
            ActionsQueue.Enqueue(lidAction(900, 1300, 4000));
            ActionsQueue.Enqueue(delayAction(1000));
            ActionsQueue.Enqueue(lidAction(1300, 1400, 500));
            ActionsQueue.Enqueue(delayAction(1000));
            ActionsQueue.Enqueue(switchAction(1050, 1900, 2000));
            ActionsQueue.Enqueue(delayAction(300));
            ActionsQueue.Enqueue(switchAction(1900, 2200, 500));
            ActionsQueue.Enqueue(delayAction(100));
            ActionsQueue.Enqueue(switchAction(2200, 1050, 2000));
            ActionsQueue.Enqueue(delayAction(100));
            ActionsQueue.Enqueue(lidAction(1400, 900, 2000));
#if usingLidTicker
            lidTicker.Start();
#endif
        }

        public void Sequence1()
        {
            switchState = true;
            currentAction = null;
             ActionsQueue.Enqueue(delayAction(700));
            ActionsQueue.Enqueue(lidAction(900, 1400, 1000));
             ActionsQueue.Enqueue(delayAction(1500));
            ActionsQueue.Enqueue(switchAction(1050, 2200, 800));
             ActionsQueue.Enqueue(delayAction(200));
            ActionsQueue.Enqueue(switchAction(2200, 1050, 2000));
             ActionsQueue.Enqueue(delayAction(100));
            ActionsQueue.Enqueue(lidAction(1400, 900, 2000));
#if usingLidTicker
            lidTicker.Start();
#endif
        }
        public void Sequence2()
        {
            switchState = true;
            currentAction = null;
            ActionsQueue.Enqueue(delayAction(800));
            ActionsQueue.Enqueue(lidAction( 900, 1200, 3000));
            ActionsQueue.Enqueue(lidAction( 1200, 1300, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1300, 1200, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1200, 1300, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1300, 1200, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1200, 1300, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1300, 1200, 1));
            ActionsQueue.Enqueue(delayAction(120));
            ActionsQueue.Enqueue(lidAction( 1200, 900, 3000));
            ActionsQueue.Enqueue(lidAction( 900, 1200, 3000));
            ActionsQueue.Enqueue(delayAction(1000));
            ActionsQueue.Enqueue(lidAction(1200, 1400, 1000));
            ActionsQueue.Enqueue(switchAction(1050, 1800, 1800));
            ActionsQueue.Enqueue(switchAction(1800, 2200, 500));
            ActionsQueue.Enqueue(delayAction(100));
            ActionsQueue.Enqueue(switchAction(2200, 1050, 500));
             ActionsQueue.Enqueue(lidAction( 1400, 900, 500));
        }
        public void RunCompiledSequence(byte[] sequence)
        {
            switchState = true;
            currentAction = null;
            int index = 0;
            while (index < sequence.Length)
            {
                switch (sequence[index] & 0x0f)
                {
                    case (byte)SequenceAction.ActionType.DelayAction:
                        {
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            ActionsQueue.Enqueue(delayAction(duration));
                            System.Console.WriteLine("delayAction({0})", duration);
                            index++;
                        }
                        break;
                    case (byte)SequenceAction.ActionType.LidAction:
                        {
                            int fromPos = sequence[++index] * 10;
                            int toPos = sequence[++index] * 10;
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            ActionsQueue.Enqueue(lidAction(fromPos, toPos, duration));
                            System.Console.WriteLine("lidAction({0},{1},{2})",fromPos,toPos,duration);
                            index++;
                        }
                        break;
                    case (byte)SequenceAction.ActionType.SwitchAction:
                        {
                            int fromPos = sequence[++index] * 10;
                            int toPos = sequence[++index] * 10;
                            int duration = sequence[++index] | (sequence[++index] << 8);
                            ActionsQueue.Enqueue(switchAction(fromPos, toPos, duration));
                            System.Console.WriteLine("switchAction({0},{1},{2})", fromPos, toPos, duration);
                            index++;
                        }
                        break;
                }
            }
        }
        public void Ticker_Thread()
        {
            bool repaintNeeded = false;
            
            while (true)
            {
                Thread.Sleep(0);
                if (currentAction != null)
                {
                    if (currentAction.Action == SequenceAction.ActionType.LidAction)
                    {
                        if (lidCurrentPos == lidTargetPos && lidMoveDirection > 0)
                        {
                            currentAction = null;
                        }
                        else if (lidCurrentPos == lidTargetPos && lidMoveDirection < 0)
                        {
                            currentAction = null;
                        }
                        else
                        {
                            lidCurrentPos += lidMoveDirection;
                            if (lidMoveDirection > 0 && lidCurrentPos> lidTargetPos)
                                lidCurrentPos = lidTargetPos;
                            else if (lidMoveDirection < 0 && lidCurrentPos < lidTargetPos)
                                lidCurrentPos = lidTargetPos;
                        }
                    }
                    else if (currentAction.Action == SequenceAction.ActionType.SwitchAction)
                    {
                        if (switchCurrentPos == switchOnOffTargetPos && switchOnOffMoveDirection < 0)
                        {
                            currentAction = null;
                        }
                        else if (switchCurrentPos == switchOnOffTargetPos && switchOnOffMoveDirection > 0)
                        {
                            currentAction = null;
                        }
                        else
                        {
                            switchCurrentPos += switchOnOffMoveDirection ;
                            if (switchOnOffMoveDirection < 0 && switchCurrentPos < switchOnOffTargetPos)
                                switchCurrentPos = switchOnOffTargetPos;
                            else if (switchOnOffMoveDirection > 0 && switchCurrentPos > switchOnOffTargetPos)
                                switchCurrentPos = switchOnOffTargetPos;

                            if (switchCurrentPos > 2000)
                                switchState = false;
                        }
                    }
                    if (currentAction != null) 
                    μTimer.uSleep(currentAction.Duration);
                }
                else
                {
                    if (ActionsQueue.Count > 0)
                    {
                        currentAction = ActionsQueue.Dequeue();
                        //System.Console.WriteLine("Dequeing {0}", currentAction.Action);
                        if (currentAction.Action == SequenceAction.ActionType.LidAction)
                        {
                            lidStartPos = currentAction.StartMs;
                            lidEndPos = currentAction.EndMs;
                            if (lidStartPos < lidEndPos)
                                lidMoveDirection = 1;
                            else
                                lidMoveDirection = -1;
                            lidCurrentPos = lidStartPos;
                            lidTargetPos = lidEndPos;
                          }
                        else if (currentAction.Action == SequenceAction.ActionType.DelayAction)
                        {
                            //System.Console.Write("Delaying {0}", currentAction.Duration);
                            Thread.Sleep(currentAction.Duration);
                            //System.Console.WriteLine("done");
                            currentAction = null;
                        }
                        else if (currentAction.Action == SequenceAction.ActionType.SwitchAction)
                        {
                            switchOnOffStartPos = currentAction.StartMs;
                            switchOnOffEndPos = currentAction.EndMs;
                            switchCurrentPos = switchOnOffStartPos;
                            if (switchOnOffStartPos > switchOnOffEndPos)
                                switchOnOffMoveDirection = -1;
                            else
                                switchOnOffMoveDirection = 1;
                            switchOnOffTargetPos = switchOnOffEndPos;

                        }
                    }
                }
                repaintNeeded = currentAction != null;
                if (repaintNeeded)
                Refresh();
            }
        }
#if usingLidTicker
        public void lidTicker_Tick(object sender, ElapsedEventArgs e)
        {
            float resolution = 3f;
            if (currentAction != null)
            {
                if (currentAction.Action == SequenceAction.actionType.LidAction)
                {
                    if (lidRotateAngle == lidTargetAngle && lidMoveDirection > 0)
                    {
                        currentAction = null;
                    }
                    else if (lidRotateAngle == lidTargetAngle && lidMoveDirection < 0)
                    {
                        currentAction = null;
                    }
                    else
                    {
                        lidRotateAngle += (float)lidMoveDirection / resolution;
                        if (lidMoveDirection > 0 && lidRotateAngle > lidTargetAngle)
                            lidRotateAngle = lidTargetAngle;
                        else if (lidMoveDirection < 0 && lidRotateAngle < lidTargetAngle)
                            lidRotateAngle = lidTargetAngle;
                    }
                }
                else if (currentAction.Action == SequenceAction.actionType.DelayAction)
                {
                    currentAction = null;
                    return;
                }
                else if (currentAction.Action == SequenceAction.actionType.SwitchAction)
                {
                    if (switchRotateAngle == switchTargetAngle && switchOnOffMoveDirection < 0)
                    {
                        currentAction = null;
                    }
                    else if (switchRotateAngle == switchTargetAngle && switchOnOffMoveDirection > 0)
                    {
                        currentAction = null;
                    }
                    else
                    {
                        switchRotateAngle += (float)switchOnOffMoveDirection / resolution;
                        if (switchOnOffMoveDirection < 0 && switchRotateAngle < switchTargetAngle)
                            switchRotateAngle = switchTargetAngle;
                        else if (switchOnOffMoveDirection > 0 && switchRotateAngle > switchTargetAngle)
                            switchRotateAngle = switchTargetAngle;

                        if (switchRotateAngle < 7)
                            switchState = false;
                    }
                }
            }
            else
            {
                lidTicker.Stop();
                if (ActionsQueue.Count > 0)
                {
                    currentAction = ActionsQueue.Dequeue();
                    System.Console.WriteLine("Dequeing {0}", currentAction.Action);
                    if (currentAction.Action == SequenceAction.actionType.LidAction)
                    {
                        lidStartAngle = currentAction.StartMs;
                        lidEndAngle = currentAction.EndMs;
                        if (lidStartAngle < lidEndAngle)
                            lidMoveDirection = 1;
                        else
                            lidMoveDirection = -1;
                        lidRotateAngle = lidStartAngle;
                        lidTargetAngle = lidEndAngle;
                        var numSteps = Math.Abs((lidEndAngle - lidStartAngle) * resolution);
                        int tickInterval = (int)(currentAction.Duration / numSteps);
                        if (tickInterval == 0) tickInterval = 1;
                        lidTicker.Interval = tickInterval;
                    }
                    else if (currentAction.Action == SequenceAction.actionType.DelayAction)
                    {
                        lidTicker.Interval = currentAction.Duration;
                    }
                    else if (currentAction.Action == SequenceAction.actionType.SwitchAction)
                    {
                        switchOnOffStartAngle = currentAction.StartMs;
                        switchOnOffEndAngle = currentAction.EndMs;
                        switchRotateAngle = switchOnOffStartAngle;
                        if (switchOnOffStartAngle > switchOnOffEndAngle)
                            switchOnOffMoveDirection = -1;
                        else
                            switchOnOffMoveDirection = 1;
                        switchTargetAngle = switchOnOffEndAngle;
                        var numSteps = Math.Abs((switchOnOffEndAngle - switchOnOffStartAngle) * resolution);
                        int tickInterval = (int)(currentAction.Duration / numSteps);
                        if (tickInterval == 0) tickInterval = 1;

                        lidTicker.Interval = tickInterval;
                    }
                    System.Console.WriteLine("TickInterval {0}", lidTicker.Interval);
                    lidTicker.Start();
                }
            }


            Refresh();
        }
#endif
    }
}
