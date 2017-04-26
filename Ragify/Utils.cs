using System;

namespace Ragify
{
	public static class Utils
	{
		public static int GetCurrentTimestamp()
		{
			long num = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
			return int.Parse((num / 10000000L).ToString());
		}

		public static int GetTimeStamp(DateTime date)
		{
			long num = date.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
			return int.Parse((num / 10000000L).ToString());
		}
	}
}
