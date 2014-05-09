using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluephase.NinjectModules;
using Ninject;
using Ninject.Modules;

namespace Bluephase
{
    internal static class Session
    {
        private static StandardKernel _standardKernel;

        internal static void Initialize()
        {
            _standardKernel = new StandardKernel(new INinjectModule[] {
                new BluephaseModule()
            });
        }
    }
}