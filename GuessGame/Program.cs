/*
 * Copyright (c) 2017 Matthew Wright.
 * Licensed under MIT License. See LICENSE.txt for further details.
 * 
 * This software should be distributed with a LICENSE.TXT file in the solution root.
 * Alternatively  you can find a copy of the license in the github repository:
 * https://github.com/wiganlatics/guess-game.
 * The MIT License text is also available at: https://choosealicense.com/licenses/mit/.
 */

using System;
using System.Windows.Forms;

namespace GuessGame
{
    static class Program
    {
        /// <summary>
        /// Random number generator to use.
        /// Define this once globally to avoid similar 
        /// sequences of randomly generated numbers.
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGuess(rand));
        }
    }
}
