/*
 * Licensed under MIT License. Copyright (c) 2017 Matthew Wright. See LICENSE.txt in solution root for further details.
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
