using System;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Shared
{
    public class MyMessage
    {
        public string Text { get; set; }
    }

    public class MCalss : IOptionsMonitor<ConsoleLoggerOptions>
    {
        public ConsoleLoggerOptions Get(string name)
        {
            return new ConsoleLoggerOptions();
        }

        public IDisposable OnChange(Action<ConsoleLoggerOptions, string> listener)
        {
            return null;
        }

        public ConsoleLoggerOptions CurrentValue => Get("");
    }

}
