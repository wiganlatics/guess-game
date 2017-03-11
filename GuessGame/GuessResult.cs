/*
 * Licensed under MIT License. Copyright (c) 2017 Matthew Wright. See LICENSE.txt in solution root for further details.
 */

namespace GuessGame
{
    /// <summary>
    /// Enumerates the guess results.
    /// </summary>
    public enum GuessResult
    {
        Correct,

        GreaterThan,

        LessThan,

        AboveMaximum,

        BelowMinimum,

        NotANumber,

        TooManyGuesses
    }
}
