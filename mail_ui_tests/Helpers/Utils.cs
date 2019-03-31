using System;

namespace mail_ui_tests.Helpers
{
    class Utils
    {
        public static void sleep(int time)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(time));
        }
    }
}
