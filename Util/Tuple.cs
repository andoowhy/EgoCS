using System.Collections;

public class Tuple<T, U>
{
    public readonly T first;
    public readonly U second;

    public Tuple( T first, U second )
    {
        this.first = first;
        this.second = second;
    }
}
