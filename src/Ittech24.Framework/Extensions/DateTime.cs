using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Framework.Extensions
{
    public static partial class DateTimeExtensions
    {
        public static long ToEpoch(this DateTime date)
        {
            TimeSpan ts = date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            string timestamp = ts.TotalSeconds.ToString();
            return long.TryParse(timestamp, out long time) ? time : 0;
        }

        public static long EpochNow(this DateTime date)
        {
            TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            string timestamp = ts.TotalSeconds.ToString();
            return long.TryParse(timestamp, out long time) ? time : 0;
        }
    }

    public class DateTimeHelpers
    {
        public static long GetEpochTime()
        {
            TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            string timestamp = ts.TotalSeconds.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));
            return long.TryParse(timestamp, out long time) ? time : -1;
        }

        public static DateTime FromEpoch(long date)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(date);
        }
    }
}
