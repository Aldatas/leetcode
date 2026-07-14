#include "int_hash_map.h"

#include <stdlib.h>

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
