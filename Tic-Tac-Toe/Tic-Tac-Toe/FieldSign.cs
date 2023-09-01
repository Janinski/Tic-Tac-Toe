using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Current value of a cell in the game
    /// </summary>
    public enum FieldSign
    {
        /// <summary>
        /// Cell hasn't been clicked yet
        /// </summary>
        Free,
        /// <summary>
        /// Cell is marked with a cross
        /// </summary>
        Cross,
        /// <summary>
        /// Cell is marked with a circle
        /// </summary>
        Circle        
    }
}
