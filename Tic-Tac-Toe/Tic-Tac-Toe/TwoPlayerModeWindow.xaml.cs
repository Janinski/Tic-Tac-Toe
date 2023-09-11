using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TwoPlayerModeWindow : Window
    {
        #region Variables
        /// <summary>
        /// Contains current state of every cell during the active game
        /// </summary>
        private FieldSign[] currentGameState;

        /// <summary>
        /// True if it's player1's turn (x)
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool gameEnded;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public TwoPlayerModeWindow()
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
            for(int i = 0; i < currentGameState.Length; i++)
            {
                currentGameState[i] = FieldSign.Free;
            }

            // Player1 starts the game
            player1Turn = true;

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
        private void btn_Click(object sender, RoutedEventArgs e)
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
            if(currentGameState[index] != FieldSign.Free)
            {
                return;
            }

            // Set the cell's sign based on which players turn it is
            currentGameState[index] = player1Turn? FieldSign.Cross : FieldSign.Circle;

            // Set button text
            button.Content = player1Turn ? "X" : "O";

            // Change circle fields to rosa and cross fields to blue
            if (!player1Turn)
            {
                button.Foreground = Brushes.LightPink;
            }
            else
            {
                button.Foreground = Brushes.LightGreen;
            }

            // Toggle the players turns
            player1Turn ^= true;

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
            if (currentGameState[0] != FieldSign.Free && (currentGameState[0] & currentGameState[1] & currentGameState[2]) == currentGameState[0]){
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
