using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier
{
    public static class Constants
    {
        public static string ConnectionString { get; } =
            "Server=tcp:zg8hk2j3i5.database.windows.net,1433;Database=typingcAFGz4D1Xe;User ID=classifier@zg8hk2j3i5;Password=M3talt0ad;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
    }
}