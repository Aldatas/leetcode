/*
 * @lc app=leetcode id=20 lang=csharp
 *
 * [20] Valid Parentheses
 */

// @lc code=start
public class Solution {
    public bool IsValid(string s) {
        char p = s.ElementAt(0);
        bool opening = false;
        for (int i = 1; i < s.Length; i++)
        {
            if (opening)
            {
                p = s.ElementAt(i);
                opening = false;
            }
            else
            {
                char closeSymbol;
                opening = true;
                switch (p)
                {
                    case '(':
                        {
                            closeSymbol = ')';
                            break;
                        }
                    case '{':
                        {
                            closeSymbol = '}';
                            break;
                        }
                    case '[':
                        {
                            closeSymbol = ']';
                            break;
                        }
                    default:
                        {
                            return false;
                        }
                }
                if (s.ElementAt(i) != closeSymbol)
                    return false;
            }
        }

        return true;
    }
}
// @lc code=end

