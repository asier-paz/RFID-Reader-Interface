using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Cronomur_WRI
{
    class Win32
    {
        [DllImport("kernel32.dll")]
        private static extern bool Beep(int freq, int duration);

        public enum BeepType
        {
            Success,
            Failure
        }

        public static void Beep(BeepType _beepType)
        {
            switch (_beepType)
            {
                case BeepType.Success:
                    Beep(1000, 10);
                    break;
                case BeepType.Failure:
                    Beep(3500, 10);
                    break;
            }
        }

    }
}
