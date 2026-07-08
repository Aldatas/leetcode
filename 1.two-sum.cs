/*
 * @lc app=leetcode id=1 lang=csharp
 *
 * [1] Two Sum
 */

// @lc code=start
public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        Dictionary<int, int> numToIndex = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (numToIndex.ContainsKey(complement))
                return new int[] { numToIndex[complement], i };

            numToIndex[nums[i]] = i;
        }
        return new int[] {};
    }
}
// @lc code=end

