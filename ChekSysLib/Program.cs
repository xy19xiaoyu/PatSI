using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChekSysLib
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(RegUtility.GetCpuNumber());

                RegLib.reginfo reg = new RegLib.reginfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
