using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        #region Constructor
        public StartWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void two_player_Click(object sender, RoutedEventArgs e)
        {
            TwoPlayerModeWindow newTwoPlayerGame = new TwoPlayerModeWindow();
            newTwoPlayerGame.Show();
            this.Close();
        }

        private void single_player_Click(object sender, RoutedEventArgs e)
        {
           SinglePlayerModeWindow newSinglePlayerGame = new SinglePlayerModeWindow();
           newSinglePlayerGame.Show();
           this.Close();
        }
        #endregion
    }
}
