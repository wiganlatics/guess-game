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

namespace GuessGame
{
    public class GuessEngine
    {
        /// <summary>
        /// Random number generator to use.
        /// Define this once globally to avoid similar sequences of randomly generated numbers.
        /// </summary>
        private Random rand;
        /// <summary>
        /// The randomly generated number to guess.
        /// </summary>
        private int numberToGuess;
        /// <summary>
        /// The number of guesses so far.
        /// </summary>
        private byte guesses;
        /// <summary>
        /// The minimum number to guess that engine can generate.
        /// </summary>
        private readonly int minAnswer;
        /// <summary>
        /// The maximum number to guess that engine can generate.
        /// </summary>
        private readonly int maxAnswer;
        /// <summary>
        /// The maximum number of guesses.
        /// </summary>
        private readonly byte maxGuesses;

        /// <summary>
        /// The constructor for guess engine.
        /// </summary>
        /// <param name="rd">The random number generator to use.</param>
        /// <param name="maxGuesses">The maximum number of guesses for a question.</param>
        /// <param name="minAnswer">The inclusive lower bound of random number to guess.</param>
        /// <param name="maxAnswer">The inclusive upper bound of random number to guess.</param>
        /// <exception cref="System.ArgumentException">Thrown if the random number generator is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the max guesses is not above 0.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the min answer is not less than max answer.</exception>
        public GuessEngine(Random rd, byte maxGuesses, int minAnswer, int maxAnswer)
        {
            // Argument validation
            if (rd == null) throw new ArgumentException(Properties.Resources.NumberGeneratorMustNotBeNull);
            if (maxGuesses < 1) throw new ArgumentException(Properties.Resources.MaxGuessesMustBeAboveZero);
            if (minAnswer >= maxAnswer) throw new ArgumentException(string.Format(Properties.Resources.MinMustBeLessThanMax, minAnswer.ToString(), maxAnswer.ToString()));
            
            this.rand = rd;
            this.maxGuesses = maxGuesses;
            this.minAnswer = minAnswer;
            this.maxAnswer = maxAnswer;
        }

        /// <summary>
        /// Sets the number to guess and initialise guess count.
        /// </summary>
        public void SetNumberToGuess()
        {
            InitialiseGuesses();
            numberToGuess = rand.Next(minAnswer, maxAnswer + minAnswer);
        }

        /// <summary>
        /// Calculate the result of the guess.
        /// </summary>
        /// <param name="answer">The guess string.</param>
        /// <returns>The guess result.</returns>
        public GuessResult SubmitGuess(string answer)
        {
            GuessResult result;

            IncrementGuesses();

            int answerNum;
            if(Int32.TryParse(answer, out answerNum))
            {
                if (answerNum == numberToGuess)
                {
                    result = GuessResult.Correct;
                }
                else if (answerNum > numberToGuess)
                {
                    result = (answerNum <= maxAnswer) ? GuessResult.GreaterThan : GuessResult.AboveMaximum;
                }
                else
                {
                    result = (answerNum >= minAnswer) ? GuessResult.LessThan : GuessResult.BelowMinimum;
                }
            }
            else
            {
                result = GuessResult.NotANumber;
            }

            return (guesses < maxGuesses) ? result : GuessResult.TooManyGuesses;
        }

        /// <summary>
        /// Accessor method for the number of guesses.
        /// </summary>
        /// <returns>Byte - the number of guesses.</returns>
        public byte GetNumberOfGuesses()
        {
            return guesses;
        }

        /// <summary>
        /// Increment the guess count.
        /// </summary>
        /// <exception cref="System.Exception">Thrown if the maximum number of guesses has already been taken.</exception>
        /// <exception cref="System.Exception">Thrown if the type of the guesses variable is not byte.</exception>
        /// <exception cref="System.OverflowException">Thrown if incrementing the guess count would cause an overflow.</exception>
        private void IncrementGuesses()
        {
            if (guesses < maxGuesses)
            {
                if (guesses.GetType().Equals(typeof(byte)))
                {
                    if (guesses < byte.MaxValue)
                    {

                        guesses++;
                    }
                    else
                    {
                        throw new OverflowException(Properties.Resources.GuessesOverflowError);
                    }
                }
                else
                {
                    throw new Exception(string.Format(Properties.Resources.WrongTypeError, typeof(byte).Name, guesses.GetType().Name));
                }
            }
            else
            {
                throw new Exception(string.Format(Properties.Resources.MaximumGuessesError, guesses.ToString(), maxGuesses.ToString()));
            }
        }

        /// <summary>
        /// Initialise the guess count.
        /// </summary>
        private void InitialiseGuesses()
        {
            guesses = 0;
        }
    }
}
