using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L45SpecialCharWinEnhance
{
    class UnbreakableSendKey
    {
        public static void Send(string toSend)
        {
            try
            {
               SendKeys.SendWait(toSend);
            } catch (Exception e)
            {
                Debug.WriteLine("Error sending key {0}: {1}", toSend, e);
            }
        }

    }
}
