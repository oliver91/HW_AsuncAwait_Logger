using System;
using System.IO;

namespace GreenHouse.WebUI
{
    public enum MsgType
    {
        Warn,
        Error,
        Info
    }

    public class Logger
    {
        private static readonly Lazy<Logger> _instance = 
            new Lazy<Logger>(() => new Logger(), true);

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void Write(Exception e, MsgType msgType = MsgType.Info)
        {
            WriteToFile(String.Format("{0}. {1}: {2} [{3}]", 
                msgType, e.GetType(), e.Message, DateTime.Now));
        }

        public void Write(Exception e, string message, MsgType msgType = MsgType.Info)
        {
            WriteToFile(String.Format("{0}. {1}. {2}: {3} [{4}]", 
                msgType, message, e.GetType(), e.Message, DateTime.Now));
        }

        public void Write(string message, MsgType msgType = MsgType.Info)
        {
            WriteToFile(String.Format("{0}. {1} [{2}]", msgType, message, DateTime.Now));
        }

        private void WriteToFile(string str)
        {
            string filePath = @"E:\log.txt";
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(str);
            writer.Close();
        }
    }
}