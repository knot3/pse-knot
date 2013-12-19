/// <summary>
/// Eine Wertesammlung der möglichen Richtungen in einem dreidimensionalen Raum.
/// Wird benutzt, damit keine ungültigen Kantenrichtungen angegeben werden können.
/// </summary>
public enum Direction
{
    /// <summary>
    /// Links.
    /// </summary>
    Left=1,
    /// <summary>
    /// Rechts.
    /// </summary>
    Right=2,
    /// <summary>
    /// Hoch.
    /// </summary>
    Up=3,
    /// <summary>
    /// Runter.
    /// </summary>
    Down=4,
    /// <summary>
    /// Vorwärts.
    /// </summary>
    Forward=5,
    /// <summary>
    /// Rückwärts.
    /// </summary>
    Backward=6,
    /// <summary>
    /// Keine Richtung.
    /// </summary>
    Zero=0,
}

