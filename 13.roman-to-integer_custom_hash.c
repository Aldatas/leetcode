/*
 * @lc app=leetcode id=13 lang=c
 *
 * [13] Roman to Integer
 */

// @lc code=start
#include "lib/int_hash_map.h"

#include <stddef.h>
#include <stdint.h>
#include <string.h>

static bool init_roman_map(IntHashMap *map) {
    if (!int_hash_map_init(map, 7)) {
        return false;
    }

    int_hash_map_put(map, 'I', 1);
    int_hash_map_put(map, 'V', 5);
    int_hash_map_put(map, 'X', 10);
    int_hash_map_put(map, 'L', 50);
    int_hash_map_put(map, 'C', 100);
    int_hash_map_put(map, 'D', 500);
    int_hash_map_put(map, 'M', 1000);
    return true;
}

int romanToInt(char *s) {
    IntHashMap map;
    if (s == NULL || !init_roman_map(&map)) {
        return 0;
    }

    int result = 0;
    int previous_value = 0;
    size_t length = strlen(s);

    for (size_t i = length; i > 0; i--) {
        int current_value;
        int_hash_map_get(&map, (unsigned char)s[i - 1], &current_value);
        result += current_value < previous_value ? -current_value : current_value;
        previous_value = current_value;
    }

    int_hash_map_destroy(&map);
    return result;
}
// @lc code=end
