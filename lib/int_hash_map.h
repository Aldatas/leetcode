#ifndef INT_HASH_MAP_H
#define INT_HASH_MAP_H

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>

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

bool int_hash_map_init(IntHashMap *map, size_t initial_capacity);
void int_hash_map_destroy(IntHashMap *map);
bool int_hash_map_put(IntHashMap *map, int64_t key, int value);
bool int_hash_map_get(const IntHashMap *map, int64_t key, int *value);

#endif
