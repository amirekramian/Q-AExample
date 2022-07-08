using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class LogHelper
    {
		public static LogEventInfo LogFullData(this MongoLog mongoLog, LogLevel? logLevel = null)
		{
			var theEvent = new LogEventInfo(logLevel ?? LogLevel.Info, mongoLog.ControllerName, $"{mongoLog.ControllerName} . {mongoLog.ActionName} Called !");

			theEvent.Properties.Add("CustomData", mongoLog);

			return theEvent;
		}
	}
}
