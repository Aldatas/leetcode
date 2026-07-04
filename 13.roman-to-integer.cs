/* 
 * @lc app=leetcode id=13 lang=csharp
 * 
 * [13] Roman to Integer
 */

// @lc code=start
public class Solution {
    private Dictionary<char, int> RomanNumDict = 
        new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

    public int RomanToInt(string s) {
        if (s.Length == 0) return 0;
        
        int result = 0;
        int prevValue = 0;
        int currentValue;

        for (int i = s.Length - 1; i >= 0; i--)
        {
            currentValue = RomanNumDict[s[i]];

            if (currentValue < prevValue)
            {
                result -= currentValue;
            }
            else
            {
                result += currentValue;
            }

            prevValue = currentValue;
        }

        return result;
    }
}
// @lc code=end