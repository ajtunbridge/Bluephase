using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluephase.Common;
using Bluephase.Common.Views;
using Bluephase.Views;
using Ninject.Modules;

namespace Bluephase.NinjectModules
{
    internal sealed class BluephaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDialogService>().To<StandardDialogService>();

            Bind<ICustomerManagementView>().To<CustomerManagementView>();
        }
    }
}
