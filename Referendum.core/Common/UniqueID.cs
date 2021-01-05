using System;

namespace Referendum.core.Common
{
{
	public class UniqueID
	{
		public static string GetUniqueID()
		{
			//System.Guid guid = System.Guid.NewGuid();
			//return guid.ToString();
			return DateTime.Now.Ticks.ToString("x");
		}
	}
}
