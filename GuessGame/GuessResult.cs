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
