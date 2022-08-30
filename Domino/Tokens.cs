namespace Domino.Tokens;

/// <summary>
///     Base Domino Token implementation
/// </summary>
public class DominoToken : ITokenGenerator {
    public int Left { get; }
    public int Right { get; }
    public bool IsDouble {
        get {
            return this.Left == this.Right;
        }
    }

    public DominoToken() {
        this.Left = -1;
        this.Right = -1;
    }

    /// <param name="left">Left value of a given token</param>
    /// <param name="right">Right value of a given token</param>
    /// <remarks>
    ///     There's no such thing as side concept for now, maybe this could change 
    ///     to a collection in the future
    /// </remarks>
    public DominoToken(int left, int right) {
        this.Left = left;
        this.Right = right;
    }

    /// <summary>
    ///     Numerical concept related to a token, where can be assigned an integer
    ///     to a token for comparation, filter, etc
    /// </summary>
    public virtual int Value() {
        return this.Left + this.Right;
    }

    public override string ToString()
    {
        if (this.Left == -1 || this.Right == -1)
            return "Domino Token";

        return $"({Left} | {Right})";
    }

    /// <summary>
    ///     This method generates the initial set of tokens to deal to each player given a max value
    ///     and using the current class instance.
    /// </summary>
    public virtual IList<DominoToken> GenerateTokens(int tokenValues) { 
        List<int[]> tokenValuesList = new List<int[]>();
        List<DominoToken> tokens = new List<DominoToken>();

        Utils.Utils.GenerateTokenValues(tokenValues, tokenValuesList);

        foreach(int[] values in tokenValuesList) {
            tokens.Add(new DominoToken(values[0], values[1]));
        }

        return tokens;
    }
}

    /// <summary>
    ///     Token value implementation where the number 6 doesn't represent a value
    ///     to compute for the total
    /// </summary>
public class SixUnvaluableDominoToken : DominoToken {
    public SixUnvaluableDominoToken() : base() {}
    public SixUnvaluableDominoToken(int left, int right) : base(left, right) {}

    public override int Value()
    {
        int value = 0;

        if (this.Right != 6)
            value += this.Right;
        if (this.Left != 6)
            value += this.Left;

        return value;
    }

    public override string ToString() {
        if (this.Left == -1 || this.Right == -1)
            return "Six Unvaluable Domino Token";
            
        return base.ToString();
    }
    
    /// <summary>
    ///     This method generates the initial set of tokens to deal to each player given a max value
    ///     and using the current class instance.
    /// </summary>
    public override IList<DominoToken> GenerateTokens(int tokenValues) {
        List<int[]> tokenValuesList = new List<int[]>();
        List<DominoToken> tokens = new List<DominoToken>();

        Utils.Utils.GenerateTokenValues(tokenValues, tokenValuesList);

        foreach(int[] values in tokenValuesList) {
            tokens.Add(new SixUnvaluableDominoToken(values[0], values[1]));
        }

        return tokens;
    }
}

    /// <summary>
    ///     Token value implementation where the basic value implementation is doubled
    /// </summary>
public class DoubledValueDominoToken : DominoToken {
    public DoubledValueDominoToken() : base() {}    
    public DoubledValueDominoToken(int left, int right) : base(left, right) {}

    public override int Value()
    {
        return  2 * base.Value();
    }

    public override string ToString() {
        if (this.Left == -1 || this.Right == -1)
            return "Doubled Value Domino Token";

        return base.ToString();
    }

    /// <summary>
    ///     This method generates the initial set of tokens to deal to each player given a max value
    ///     and using the current class instance.
    /// </summary>
    public override IList<DominoToken> GenerateTokens(int tokenValues)
    {
        List<int[]> tokenValuesList = new List<int[]>();
        List<DominoToken> tokens = new List<DominoToken>();

        Utils.Utils.GenerateTokenValues(tokenValues, tokenValuesList);

        foreach(int[] values in tokenValuesList) {
            tokens.Add(new DoubledValueDominoToken(values[0], values[1]));
        }

        return tokens;
    }
}

/// <summary>
///     Token generator interface. This contains an abstraction to create a collection 
///     of token objects from a given maxNumber. E.g: 9-based Domino, 7-based Domino
/// </summary>
public interface ITokenGenerator {
    public IList<DominoToken> GenerateTokens(int tokenValues);
}
