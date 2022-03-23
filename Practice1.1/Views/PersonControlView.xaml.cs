using System.Windows.Controls;
using Practice1._1.ViewModels;
namespace Practice1._1.Views
{
    public partial class PersonControlView : UserControl
    {
        public PersonControlView()
        {
            InitializeComponent();
            DataContext = new PersonControlViewModel();
           
        }
    }
}
