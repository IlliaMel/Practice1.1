using System.Windows.Controls;
using Practice1._1.ViewModels;
namespace Practice1._1.Views
{
    public partial class DateControlView : UserControl
    {
        public DateControlView()
        {
            InitializeComponent();
            DataContext = new DataControlViewModel();
        }
    }
}
