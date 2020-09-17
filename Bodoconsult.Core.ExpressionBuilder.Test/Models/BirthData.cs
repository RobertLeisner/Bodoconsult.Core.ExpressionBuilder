using System;

namespace Bodoconsult.Core.ExpressionBuilder.Test.Models
{
    public class BirthData
    {
        public DateTime? Date { get; set; }
        public string Country { get; set; }
        public DateTimeOffset? DateOffset
        {
            get
            {
                return Date.HasValue ? new DateTimeOffset?(Date.Value) : new DateTimeOffset?();
            }
        }

        public override string ToString()
        {
            return string.Format("Born at {0} in {1}", Date.Value.ToShortDateString(), Country);
        }

    }
}