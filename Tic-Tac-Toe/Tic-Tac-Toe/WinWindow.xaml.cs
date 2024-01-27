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
    /// Interaktionslogik für WinWindow.xaml
    /// </summary>
    public partial class WinWindow : Window
    {
        #region Variables
        /// <summary>
        /// Contains recently played mode
        /// </summary>
        private string mode;

        /// <summary>
        /// Contains if player has won or lost the game
        /// </summary>
        private bool hasWon;
        #endregion

        #region Constructor
        public WinWindow(string mode, bool hasWon)
        {
            InitializeComponent();
            this.mode = mode;
            this.hasWon = hasWon;
            if(mode == "two")
            {
                if (hasWon){ this.TitleText.Text = "Congratulations \n Player X!"; }else{ this.TitleText.Text = "Congratulations \n Player O!"; }

            }
            else
            {
                if (hasWon){this.TitleText.Text = "Congratulations!";}else{this.TitleText.Text = "Maybe next time...";}
            }
            
        }
        #endregion

        #region Methods
        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (mode == "single")
            {
                SinglePlayerModeWindow newSinglePlayerModeWindow = new SinglePlayerModeWindow();
                newSinglePlayerModeWindow.Show();
            }
            else
            {
                TwoPlayerModeWindow newTwoPlayerGame = new TwoPlayerModeWindow();
                newTwoPlayerGame.Show();
            }

            this.Close();
        }

        private void back_home_Click(object sender, RoutedEventArgs e)
        {
            StartWindow newStartWIndow = new StartWindow();
            newStartWIndow.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
