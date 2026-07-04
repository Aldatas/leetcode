/*
 * @lc app=leetcode id=14 lang=csharp
 *
 * [14] Longest Common Prefix
 */

// @lc code=start
// #define LOCAL
using System.Formats.Asn1;

public class Solution {
    public bool ContainSameLetterAt(int index, string[] strs)
    {
        char letter = ' ';
        for (int i = 0; i < strs.Length; i++)
        {
            if (letter == ' ') 
            {
                letter = strs[i][index];
            }
            else if (letter != strs[i][index])
            {
                return false;
            }
        }

        return true;
    }

    public int GetSmallestLengthInStringArray(string[] strs)
    {
        int length = 0;
        for (int i = 0; i < strs.Length; i++)
        {
            if (i == 0) length = strs[i].Length;
            if (length > strs[i].Length) length = strs[i].Length;
        }
        return length;
    }

    public string LongestCommonPrefix(string[] strs) {
        int smallestLength = GetSmallestLengthInStringArray(strs);
        string prefix = "";
        
        if (smallestLength == 0) return prefix;
        
        for (int i = 0; i < smallestLength; i++)
        {
            if (ContainSameLetterAt(i, strs))
            {
                prefix += strs[0][i];
            }
            else break;
        }

        return prefix;
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

