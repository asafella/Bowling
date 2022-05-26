using System;
namespace BowlingScoring
{
    public class Frame
    {

        public short? firstThrow { get; set; }
        public short? secondThrow { get; set; }
        internal int frameScore { get; set; } = 0;
        private ThrowTypeEnum _throwTypeEnum;

        public bool isFrameFilled
        {
            get
            {
                if (firstThrow.HasValue && secondThrow.HasValue)
                    return true;
                return false;
            }
            
        }
        public ThrowTypeEnum throwTypeEnum
        {
            get
            {
                return _throwTypeEnum;
            }
        }

        public Frame()
        {
            
        }
        public void setThrowScore(short score)
        {
            if (isFrameFilled)
            {
                throw new Exception("This frame has already been set.");
            }
            if (score >= 0 && score <= 10)
            {
                if (!firstThrow.HasValue) // Meaning it's the first throw for this frame
                {
                    if (score == 10) // 10 on the first means strike.
                    {
                        firstThrow = 0;
                        secondThrow = score;
                        frameScore = 10;
                        _throwTypeEnum = ThrowTypeEnum.Strike;
                    }
                    else
                        firstThrow = score;
                }
                else // second throw for that frame
                {
                    if (score == 10)
                        score = (short)(10 - firstThrow);
                    if (firstThrow + score > 10)
                    {
                        throw new Exception("The total values entered for this frame exceeds the number of pins");
                    }
                    else
                    {

                        secondThrow = score;
                        frameScore = (int)(firstThrow + secondThrow);
                        if (firstThrow + score == 10)
                            _throwTypeEnum = ThrowTypeEnum.Spare;
                        else
                        {
                            _throwTypeEnum = ThrowTypeEnum.PartialKnockDown;
                        }
                    }

                }
            }
            else
            {
                throw new Exception("A wrong score for a throw.");
            }
        }
    }
}
