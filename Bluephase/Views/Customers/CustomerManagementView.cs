using Bluephase.Common;
using Bluephase.Common.Views;

namespace Bluephase.Views
{
    public partial class CustomerManagementView : ViewBase, ICustomerManagementView
    {
        public CustomerManagementView(IDialogService dialogService) : base(dialogService)
        {
            InitializeComponent();
        }
    }
}