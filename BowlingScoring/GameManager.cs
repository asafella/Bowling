using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoring
{
    public class GameManager
    {
        public event EventHandler GameEndedEvent;
        private Frame[] frames;
        private int currentFrameIndex=-1;

        public GameManager()
        {
            frames = new Frame[11];
        }

        public void newThrow(short throwScore)
        {
            Frame frame;

            if (currentFrameIndex==-1)
            {
                //First Throw Ever
                currentFrameIndex = 0;
                frame = new Frame();
            }
            else
            {
                if (frames[(short)currentFrameIndex].isFrameFilled)
                {
                    currentFrameIndex++;
                    frame = new Frame();
                }
                else
                {
                    frame = frames[(short)currentFrameIndex];
                }
            }

            frame.setThrowScore(throwScore);
            frames[(short)currentFrameIndex] = frame;
            makeScoreAdjustments();
            if ((currentFrameIndex == 9 && frame.throwTypeEnum == ThrowTypeEnum.PartialKnockDown && frame.isFrameFilled) ||
                (currentFrameIndex == 10 && !frame.isFrameFilled))
            {
                GameEndedEvent?.Invoke(this, null);
            }

        }
        private void makeScoreAdjustments()
        {
            int previousScore = 0;
            for(int i=0; i<= currentFrameIndex; i++)
            {
                switch (frames[i].throwTypeEnum)
                {
                    case ThrowTypeEnum.PartialKnockDown:
                        {
                            frames[i].frameScore = (int)(frames[i].firstThrow + frames[i].secondThrow??0);
                            break;
                        }
                    case ThrowTypeEnum.Spare:
                        {
                            if (i<currentFrameIndex)
                            {
                                frames[i].frameScore = 10 + (int)(frames[i + 1].throwTypeEnum == ThrowTypeEnum.Strike ? 10 : frames[i + 1].firstThrow);
                            }
                            else
                            {
                                frames[i].frameScore = 10;
                            }
                            break;
                        }
                    case ThrowTypeEnum.Strike:
                        {
                            if (i<currentFrameIndex)
                            {
                                if (frames[i + 1].throwTypeEnum != ThrowTypeEnum.Strike)
                                    frames[i].frameScore = 10 + frames[i + 1].firstThrow + frames[i+1].secondThrow??0;
                                else
                                {
                                    if (i+1<currentFrameIndex)
                                    {
                                        if (frames[i + 2].throwTypeEnum == ThrowTypeEnum.Strike)
                                            frames[i].frameScore = 30;
                                        else
                                            frames[i].frameScore = (int)(20 + frames[i + 2].firstThrow);
                                    }
                                }
                            }
                            else
                            {
                                frames[i].frameScore = 10;
                            }
                            break;
                        }
                }
                frames[i].frameScore += previousScore;
                previousScore = frames[i].frameScore;
            }
            
        }
        public Frame[] getFrames()
        {
            if (currentFrameIndex!=-1)
            {
                return frames.ToList().Where(x => x != null).ToArray();
            }
            else
                return null;
        }
    }
}
