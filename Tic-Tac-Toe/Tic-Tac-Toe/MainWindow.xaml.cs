using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        public MainWindow()
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
                button.Foreground = Brushes.Red;
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

            // Toggle the players turns
            player1Turn ^= true;
        }
        #endregion
    }
}
