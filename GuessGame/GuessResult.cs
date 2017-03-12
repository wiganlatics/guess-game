/*
 * Copyright (c) 2017 Matthew Wright.
 * Licensed under MIT License. See LICENSE.txt for further details.
 * 
 * This software should be distributed with a LICENSE.TXT file in the solution root.
 * Alternatively  you can find a copy of the license in the github repository:
 * https://github.com/wiganlatics/guess-game.
 * The MIT License text is also available at: https://choosealicense.com/licenses/mit/.
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
