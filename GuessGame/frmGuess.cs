/*
 * Copyright (c) 2017 Matthew Wright.
 * Licensed under MIT License. See LICENSE.txt for further details.
 * 
 * This software should be distributed with a LICENSE.TXT file in the solution root.
 * Alternatively  you can find a copy of the license in the github repository:
 * https://github.com/wiganlatics/temperature-converter.
 * The MIT License text is also available at: https://choosealicense.com/licenses/mit/.
 */

using System;
using System.Windows.Forms;

namespace GuessGame
{
    public partial class frmGuess : Form
    {
        /// <summary>
        /// The engine to run the guessing game.
        /// </summary>
        private GuessEngine guessEngine;
        /// <summary>
        /// The minimum value that the guess engine can generate.
        /// </summary>
        private const int minAnswer = 1;
        /// <summary>
        /// The maximum value that the guess engine can generate.
        /// </summary>
        private const int maxAnswer = 100;
        /// <summary>
        /// The maximum number of guesses.
        /// </summary>
        private const byte maxGuesses = 20;

        /// <summary>
        /// The constructor for form.
        /// </summary>
        /// <param name="rand">The number generator to use.</param>
        public frmGuess(Random rand)
        {
            try
            {
                InitializeComponent();
                guessEngine = new GuessEngine(rand, maxGuesses, minAnswer, maxAnswer);
            }
            catch (Exception ex)
            {
                ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Event handler for the start button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            guessEngine.SetNumberToGuess();
            btnStart.Enabled = false;
            btnGuess.Enabled = true;
            txtAnswer.Enabled = true;
            SetFocusOnTxtAnswer();
        }

        /// <summary>
        /// Event handler for the guess button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The arguments.</param>
        private void btnGuess_Click(object sender, EventArgs e)
        {
            switch (guessEngine.SubmitGuess(txtAnswer.Text))
            {
                case GuessResult.Correct:
                    MessageBox.Show(string.Format(Properties.Resources.WinMessage, guessEngine.GetNumberOfGuesses()), Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    InitialiseForm();
                    break;
                case GuessResult.GreaterThan:
                    MessageBox.Show(Properties.Resources.GuessTooLarge, Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFocusOnTxtAnswer();
                    break;
                case GuessResult.LessThan:
                    MessageBox.Show(Properties.Resources.GuessTooSmall, Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetFocusOnTxtAnswer();
                    break;
                case GuessResult.AboveMaximum:
                    MessageBox.Show(string.Format(Properties.Resources.GuessAboveMaximum, maxAnswer), Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFocusOnTxtAnswer();
                    break;
                case GuessResult.BelowMinimum:
                    MessageBox.Show(string.Format(Properties.Resources.GuessBelowMinimum, minAnswer), Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFocusOnTxtAnswer();
                    break;
                case GuessResult.NotANumber:
                    MessageBox.Show(Properties.Resources.GuessNotANumber, Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFocusOnTxtAnswer();
                    break;
                case GuessResult.TooManyGuesses:
                    MessageBox.Show(Properties.Resources.LoseMessage, Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    InitialiseForm();
                    break;
                default:
                    ErrorHandler(new Exception(Properties.Resources.UnknownGuessResult));
                    break;
            }
        }

        /// <summary>
        /// Sets focus to answer text box.
        /// </summary>
        private void SetFocusOnTxtAnswer()
        {
            if (!txtAnswer.Focused) txtAnswer.Focus();
            txtAnswer.SelectAll();
        }

        /// <summary>
        /// Reset form components to initial state.
        /// </summary>
        private void InitialiseForm()
        {
            btnStart.Enabled = true;
            btnGuess.Enabled = false;
        }

        /// <summary>
        /// Handle errors and disable form elements since state is invalid.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        private void ErrorHandler(Exception ex)
        {
            MessageBox.Show(string.Format(Properties.Resources.ErrorDisplayingForm, ex.Message), Properties.Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            btnStart.Enabled = false;
            btnGuess.Enabled = false;
            txtAnswer.Enabled = false;
        }
    }
}
