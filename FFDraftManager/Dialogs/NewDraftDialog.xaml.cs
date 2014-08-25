using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FFDraftManager.Dialogs {
    /// <summary>
    /// Interaction logic for NewDraftDialog.xaml
    /// </summary>
    public partial class NewDraftDialog : Window {
        public NewDraftDialog() {
            InitializeComponent();
            DataContext = new NewDraftDialogController(this);
        }
    }
}