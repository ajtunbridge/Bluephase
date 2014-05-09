using System.Windows.Forms;
using Bluephase.Common;
using Bluephase.Common.Views;
using Bluephase.Properties;

namespace Bluephase.Views
{
    public partial class ViewBase : UserControl, IView
    {
        private readonly IDialogService _dialogService;

        public ViewBase()
        {
            InitializeComponent();

            Font = Settings.Default.DefaultAppFont;
        }

        public ViewBase(IDialogService dialogService) : this()
        {
            _dialogService = dialogService;
        }

        public IDialogService DialogService
        {
            get { return _dialogService; }
        }
    }
}
