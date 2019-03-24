using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Models
{
    public class ChatFormater
    {
        public static string Format(string sender, string Message)
        {
            return $"[{DateTime.Now}] {sender}: {Message} \n";
        }
        public static string FormatToAll(string sender, string Message)
        {
            return $"[{DateTime.Now}] {sender}: {Message} \n";
        }
    }
}
