namespace SimplePainterServer;

public static class Config
{
    public const int    WordMaxDrawCount                 = 10;
    public const int    ImageMaxGuessCount               = 10;
    public const double ImageMaxGuessCorrectlyProportion = 0.8;
    public const double CreateWordAddPersonalProgress    = 0.0025;
    public const double GuessWordAddPersonalProgress     = 0.005;
}