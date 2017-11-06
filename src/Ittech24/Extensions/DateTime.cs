using System;
using System.Collections.Generic;
using System.Text;

namespace Ittech24.Extensions
{
    public static partial class DateTimeExtensions
    {
        public static long Timestamp(this DateTime date)
        {
            TimeSpan ts = date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            string timestamp = ts.TotalSeconds.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));
            return long.TryParse(timestamp, out long time) ? time : -1;
        }
    }

    public class DateTimeUtils
    {
        public static long Timestamp()
        {
            TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            string timestamp = ts.TotalSeconds.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));
            return long.TryParse(timestamp, out long time) ? time : -1;
        }
    }

    public class StringUtils
    {
        public static string Nonce() => Guid.NewGuid().ToString("N");
    }
}
