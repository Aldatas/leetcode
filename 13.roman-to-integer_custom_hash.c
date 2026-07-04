/*
 * @lc app=leetcode id=13 lang=c
 *
 * [13] Roman to Integer
 */

// @lc code=start
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define TABLE_SIZE 7

typedef struct HashEntry {
    char *key;
    int value;
    struct HashEntry *next;
} HashEntry;

typedef struct HashTable {
    HashEntry **table;
} HashTable;

void initHashTable(HashTable *ht) {
    ht->table = (HashEntry **)malloc(TABLE_SIZE * sizeof(HashEntry *));
    for (int i = 0; i < TABLE_SIZE; i++) {
        ht->table[i] = NULL;
    }
}

int hashFunction(const char *key) {
    int hash = 0;
    for (int i = 0; key[i] != '\0'; i++) {
        hash = (hash * 31 + key[i]) % TABLE_SIZE;
    }
    return hash;
}

void insert(HashTable *ht, const char *key, int value) {
    int index = hashFunction(key);
    HashEntry *newEntry = (HashEntry *)malloc(sizeof(HashEntry));
    newEntry->key = strdup(key);
    newEntry->value = value;
    newEntry->next = ht->table[index];
    ht->table[index] = newEntry;
}

int search(HashTable *ht, const char *key) {
    int index = hashFunction(key);
    HashEntry *entry = ht->table[index];
    while (entry != NULL) {
        if (strcmp(entry->key, key) == 0) {
            return entry->value;
        }
        entry = entry->next;
    }
    return -1;
}

void freeHashTable(HashTable *ht) {
    for (int i = 0; i < TABLE_SIZE; i++) {
        HashEntry *entry = ht->table[i];
        while (entry != NULL) {
            HashEntry *next = entry->next;
            free(entry->key);
            free(entry);
            entry = next;
        }
    }
    free(ht->table);
}

void InitRomanNumHashTable(HashTable *ht) {
    initHashTable(ht);

    insert(ht, "I", 1);
    insert(ht, "V", 5);
    insert(ht, "X", 10);
    insert(ht, "L", 50);
    insert(ht, "C", 100);
    insert(ht, "D", 500);
    insert(ht, "M", 1000);
}

int romanToInt(char* s) {
    HashTable ht;
    InitRomanNumHashTable(&ht);

    int result = 0;
    int prevValue = 0;
    int currentValue = 0;

    for (int i = sizeof(s) / sizeof(char) - 1; i >= 0; i--) {
        currentValue = search(&ht, &s[i]);
        if (currentValue < prevValue) {
            result -= currentValue;
        }
        else {
            result += currentValue;
        }
        prevValue = currentValue;
    }

    freeHashTable(&ht);

    return result;
}
// @lc code=end

