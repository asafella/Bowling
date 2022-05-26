using System;
namespace BowlingScoring
{
    public class ThrowEventArgs: EventArgs
    {
        public ThrowTypeEnum throwTypeEnum { get; set; }
        public short throwScore { get; set; }

        public ThrowEventArgs(ThrowTypeEnum throwTypeEnum,short throwScore)
        {
            this.throwTypeEnum = throwTypeEnum;
            this.throwScore = throwScore;
        }
    }
}
