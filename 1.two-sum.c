/*
 * @lc app=leetcode id=1 lang=c
 *
 * [1] Two Sum
 */

// @lc code=start
/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
//#include "lib/int_hash_map.h"

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>
#include <stdlib.h>

typedef struct {
    int64_t key;
    int value;
    bool occupied;
} IntHashMapEntry;

typedef struct {
    IntHashMapEntry *entries;
    size_t capacity;
    size_t size;
} IntHashMap;

static size_t next_power_of_two(size_t value) {
    size_t result = 16;

    while (result < value && result <= SIZE_MAX / 2) {
        result *= 2;
    }

    return result;
}

static size_t hash_key(int64_t key, size_t capacity) {
    uint64_t x = (uint64_t)key;

    // SplitMix64-style mixing prevents bad clustering for nearby integer keys.
    x ^= x >> 30;
    x *= UINT64_C(0xbf58476d1ce4e5b9);
    x ^= x >> 27;
    x *= UINT64_C(0x94d049bb133111eb);
    x ^= x >> 31;

    return (size_t)x & (capacity - 1);
}

bool int_hash_map_init(IntHashMap *map, size_t initial_capacity) {
    if (map == NULL) {
        return false;
    }

    map->capacity = next_power_of_two(initial_capacity);
    map->size = 0;
    map->entries = calloc(map->capacity, sizeof(*map->entries));
    return map->entries != NULL;
}

void int_hash_map_destroy(IntHashMap *map) {
    if (map == NULL) {
        return;
    }

    free(map->entries);
    map->entries = NULL;
    map->capacity = 0;
    map->size = 0;
}

static bool insert_entry(IntHashMap *map, int64_t key, int value) {
    size_t index = hash_key(key, map->capacity);

    while (map->entries[index].occupied) {
        if (map->entries[index].key == key) {
            map->entries[index].value = value;
            return true;
        }
        index = (index + 1) & (map->capacity - 1);
    }

    map->entries[index] = (IntHashMapEntry){
        .key = key,
        .value = value,
        .occupied = true,
    };
    map->size++;
    return true;
}

static bool resize_map(IntHashMap *map) {
    IntHashMapEntry *old_entries = map->entries;
    size_t old_capacity = map->capacity;
    size_t new_capacity = old_capacity * 2;
    IntHashMapEntry *new_entries = calloc(new_capacity, sizeof(*new_entries));

    if (new_entries == NULL) {
        return false;
    }

    map->entries = new_entries;
    map->capacity = new_capacity;
    map->size = 0;

    for (size_t i = 0; i < old_capacity; i++) {
        if (old_entries[i].occupied) {
            insert_entry(map, old_entries[i].key, old_entries[i].value);
        }
    }

    free(old_entries);
    return true;
}

bool int_hash_map_put(IntHashMap *map, int64_t key, int value) {
    if (map == NULL || map->entries == NULL) {
        return false;
    }

    if ((map->size + 1) * 10 > map->capacity * 7 && !resize_map(map)) {
        return false;
    }

    return insert_entry(map, key, value);
}

bool int_hash_map_get(const IntHashMap *map, int64_t key, int *value) {
    if (map == NULL || map->entries == NULL || value == NULL) {
        return false;
    }

    size_t index = hash_key(key, map->capacity);
    while (map->entries[index].occupied) {
        if (map->entries[index].key == key) {
            *value = map->entries[index].value;
            return true;
        }
        index = (index + 1) & (map->capacity - 1);
    }

    return false;
}


int *twoSum(int *nums, int numsSize, int target, int *returnSize) {
    *returnSize = 0;

    IntHashMap map;
    if (nums == NULL || numsSize < 2 || !int_hash_map_init(&map, (size_t)numsSize * 2)) {
        return NULL;
    }

    for (int i = 0; i < numsSize; i++) {
        int previous_index;
        int64_t complement = (int64_t)target - nums[i];

        if (int_hash_map_get(&map, complement, &previous_index)) {
            int *indices = malloc(2 * sizeof(*indices));
            if (indices != NULL) {
                indices[0] = previous_index;
                indices[1] = i;
                *returnSize = 2;
            }
            int_hash_map_destroy(&map);
            return indices;
        }

        if (!int_hash_map_put(&map, nums[i], i)) {
            int_hash_map_destroy(&map);
            return NULL;
        }
    }

    int_hash_map_destroy(&map);
    return NULL;
}
// @lc code=end
