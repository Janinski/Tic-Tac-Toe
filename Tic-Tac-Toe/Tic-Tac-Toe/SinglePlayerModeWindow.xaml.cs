using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaktionslogik für SinglePlayerModeWindow.xaml
    /// </summary>
    public partial class SinglePlayerModeWindow : Window
    {
        #region Variables
        /// <summary>
        /// Contains current state of every cell during the active game
        /// </summary>
        private FieldSign[] currentGameState;

        /// <summary>
        /// Contains random number to set Circle on the field with this index
        /// </summary>
        private int indexForCircle;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public SinglePlayerModeWindow()
        {
            InitializeComponent();

            StartNewGame();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts a new game and resets all values to default
        /// </summary>
        void StartNewGame()
        {
            // Create a new blanc array of free cells
            currentGameState = new FieldSign[9];
            for (int i = 0; i < currentGameState.Length; i++)
            {
                currentGameState[i] = FieldSign.Free;
            }

            // Iterate every cell (button) of the grid
            playground.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Clear every cell and set default design of field
                button.Content = string.Empty;
                button.Background = Brushes.White;
            });

            // Make sure game starts and isn't finished yet
            gameEnded = false;
        }

        /// <summary>
        /// Handles a button (cell) click event
        /// </summary>
        /// <param name="sender"> The clicked button (cell) </param>
        /// <param name="e"> The events of the click </param>
        protected async void btn_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (gameEnded)
            {
                StartNewGame();
                return;
            }

            // Cast sender to a button
            Button button = (Button)sender;

            // Find the button's position in field 
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);

            int index = column + (row * 3);

            // Don't do anything if the cell has already a sign in it
            if (currentGameState[index] != FieldSign.Free)
            {
                return;
            }

            // Set the cell's sign
            currentGameState[index] = FieldSign.Cross;

            // Set button text and color
            button.Content = "X";
            button.Foreground = Brushes.LightGreen;

            // Checks field for a winner
            CheckForAWinner();

            if(!gameEnded)
            {
                // Bot is player2 and sets a random circle after 1s
                await Task.Delay(1000);
                SetCircle();
            }
        }

        /// <summary>
        /// Bot is player2 and sets a random cirlce
        /// </summary>
        private void SetCircle()
        {
            // List of all fields
            List<Button> fields = new List<Button>
                {
                    btn00, btn10, btn20, btn01, btn11, btn21, btn02, btn12, btn22
                };

            // Generate random index to set a circle on this field
            do
            {
                Random rnd = new Random();
                indexForCircle = rnd.Next(9);
            } while (currentGameState[indexForCircle] != FieldSign.Free);

            // Set Circle on the random free field after 
            if (currentGameState[indexForCircle] == FieldSign.Free)
            {
                currentGameState[indexForCircle] = FieldSign.Circle;
                fields[indexForCircle].Content = "O";
                fields[indexForCircle].Foreground = Brushes.LightPink;
            }

            // Checks field for a winner
            CheckForAWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a three line straight
        /// </summary>
        private void CheckForAWinner()
        {
            #region Horizontal Wins
            // Checks for hotizontal wins
            // Row 0
            if (currentGameState[0] != FieldSign.Free && (currentGameState[0] & currentGameState[1] & currentGameState[2]) == currentGameState[0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn00.Background = btn10.Background = btn20.Background = Brushes.DarkGreen;
                return;
            }

            // Row 2
            if (currentGameState[3] != FieldSign.Free && (currentGameState[3] & currentGameState[4] & currentGameState[5]) == currentGameState[3])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn01.Background = btn11.Background = btn21.Background = Brushes.DarkGreen;
                return;
            }

            // Row 3
            if (currentGameState[6] != FieldSign.Free && (currentGameState[6] & currentGameState[7] & currentGameState[8]) == currentGameState[6])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn02.Background = btn12.Background = btn22.Background = Brushes.DarkGreen;
                return;
            }
            #endregion

            #region Vertical Wins
            // Checks for vertical wins
            // Column 0
            if (currentGameState[0] != FieldSign.Free && (currentGameState[0] & currentGameState[3] & currentGameState[6]) == currentGameState[0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn00.Background = btn01.Background = btn02.Background = Brushes.DarkGreen;
                return;
            }

            // Column 1
            if (currentGameState[1] != FieldSign.Free && (currentGameState[1] & currentGameState[4] & currentGameState[7]) == currentGameState[1])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn10.Background = btn11.Background = btn12.Background = Brushes.DarkGreen;
                return;
            }

            // Column 2
            if (currentGameState[2] != FieldSign.Free && (currentGameState[2] & currentGameState[5] & currentGameState[8]) == currentGameState[2])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn20.Background = btn21.Background = btn22.Background = Brushes.DarkGreen;
                return;
            }
            #endregion

            #region Across Wins
            // Checks for across wins
            // upper left to bottom right
            if (currentGameState[0] != FieldSign.Free && (currentGameState[0] & currentGameState[4] & currentGameState[8]) == currentGameState[0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn00.Background = btn11.Background = btn22.Background = Brushes.DarkGreen;
                return;
            }
            // bottom left to upper right
            if (currentGameState[2] != FieldSign.Free && (currentGameState[2] & currentGameState[4] & currentGameState[6]) == currentGameState[2])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells
                btn02.Background = btn11.Background = btn20.Background = Brushes.DarkGreen;
                return;
            }
            #endregion

            // check for no winner and full board
            if (!currentGameState.Any(field => field == FieldSign.Free))
            {
                // Game ends
                gameEnded = true;

                // Turn all cells grey
                playground.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Gray;
                });
            }
        }
        #endregion
    }
}
