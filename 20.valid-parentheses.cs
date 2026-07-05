/*
 * @lc app=leetcode id=20 lang=csharp
 *
 * [20] Valid Parentheses
 */

// @lc code=start
public class Solution {
    public bool IsValid(string s) {
        Stack<char> stack = new();

        foreach (char c in s) 
        {
            switch (c)
            {
                case '(':
                case '{':
                case '[':
                    stack.Push(c);
                    break;

                case ')':
                    if (stack.Count == 0 || stack.Pop() != '(') return false;
                    break;

                case '}':
                    if (stack.Count == 0 || stack.Pop() != '{') return false;
                    break;

                case ']':
                    if (stack.Count == 0 || stack.Pop() != '[') return false;
                    break;
            }
        }

        return stack.Count == 0;
    }
}
// @lc code=end

