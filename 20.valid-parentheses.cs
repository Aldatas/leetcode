/*
 * @lc app=leetcode id=20 lang=csharp
 *
 * [20] Valid Parentheses
 */

// @lc code=start
public class Solution {
    public bool IsValid(string s) {
        Stack<char> stack = new();
        Dictionary<char, char> pDict = new Dictionary<char, char>()
        {
            {')', '('},
            {'}', '{'},
            {']', '['}
        };
        bool completedParenthesis = false;
        for (int i = 0; i < s.Length; i++)
        {
            char openingChar;
            switch (s.ElementAt(i))
            {
                case '(':
                case '{':
                case '[':  
                    stack.Push(s.ElementAt(i));
                    completedParenthesis = false;
                    break;
                case ')':
                case '}':
                case ']':
                    stack.TryPop(out openingChar);
                    if (openingChar != pDict[s.ElementAt(i)]) return false;
                    completedParenthesis = true;
                    break;
                default:
                        return false;
            }
        }
        if (!completedParenthesis || stack.Count > 0) return false;
        return true;
    }
}
// @lc code=end

