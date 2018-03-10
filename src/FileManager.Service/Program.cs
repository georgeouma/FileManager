using FileManager.Service.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            WindsorActivator.PreStart();
        }
    }
}
