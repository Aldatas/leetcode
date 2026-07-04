/*
 * @lc app=leetcode id=14 lang=csharp
 *
 * [14] Longest Common Prefix
 */

// @lc code=start
// #define LOCAL
using System.Formats.Asn1;
using System.Text;

public class Solution {
    public bool HasSameLetterAt(int index, string[] strs)
    {
        char letter = strs[0][index];
        for (int i = 1; i < strs.Length; i++)
        {
            if (letter != strs[i][index])
                return false;
        }

        return true;
    }

    public int GetSmallestLengthInStringArray(string[] strs)
    {
        int length = strs[0].Length;
        for (int i = 1; i < strs.Length; i++)
        {
            if (length > strs[i].Length) 
                length = strs[i].Length;
        }
        return length;
    }

    public string LongestCommonPrefix(string[] strs) {
        StringBuilder sb = new();
        int smallestLength = GetSmallestLengthInStringArray(strs);
        
        if (smallestLength == 0) return "";
        
        for (int i = 0; i < smallestLength; i++)
        {
            if (HasSameLetterAt(i, strs))
            {
                sb.Append(strs[0][i]);
            }
            else break;
        }

        return sb.ToString();
    }
}

#if LOCAL
public class Program
{
    public static void Main()
    {
        var solution = new Solution();

        Test(solution, new[] { "flower", "flow", "flight" }, "fl");
        Test(solution, new[] { "dog", "racecar", "car" }, "");
        Test(solution, new[] { "internet", "internal", "interval" }, "inter");
        Test(solution, new[] { "" }, "");
        Test(solution, new[] { "abc" }, "abc");
        Test(solution, new[] { "", "b" }, "");
    }

    private static void Test(Solution solution, string[] input, string expected)
    {
        string actual = solution.LongestCommonPrefix(input);

        Console.WriteLine(
            actual == expected
                ? $"PASS: {string.Join(", ", input)} => \"{actual}\""
                : $"FAIL: {string.Join(", ", input)} => got \"{actual}\", expected \"{expected}\""
        );
    }
}
#endif

// @lc code=end

