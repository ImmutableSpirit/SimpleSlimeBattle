using System.Text;

namespace Demo1
{
    public static class StringExtensions
{
    public static string Repeat(this string s, int n)
    {
        return new StringBuilder(s.Length * n)
                        .AppendJoin(s, new string[n+1])
                        .ToString();
    }
}
}