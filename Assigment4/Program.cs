using System;
using System.Collections.Generic;

public class Node<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public Node(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}


public class HashTable<TKey, TValue>
{
    private int Capacity = 10;
    private List<Node<TKey, TValue>>[] items;

    public HashTable()
    {
        items = new List<Node<TKey, TValue>>[Capacity];
    }

    public HashTable(int capacity)
    {
        this.Capacity = capacity;
        items = new List<Node<TKey, TValue>>[Capacity];
    }


    public void Insert(TKey key, TValue value)
    {
        int hashIndex = GetHashIndex(key);

        if (items[hashIndex] == null)
        {
            items[hashIndex] = new List<Node<TKey, TValue>>();

        }

        //Check of key in the items, if exist replace it's value. 
        foreach (var pair in items[hashIndex])
        {
            if(pair != null)
            {
                if (pair.Key.Equals(key))
                {
                    pair.Value = value;
                    return;
                }
            }
            
        }

        // If key doesn't exist, Insert a new key-value pair to the bucket
        items[hashIndex].Add(new Node<TKey, TValue>(key, value));
    }

    public bool SearchKey(TKey key, ref TValue value)
    {
        int hashIndex = GetHashIndex(key);

        if (items[hashIndex] != null)
        {
            // Search for the key in the bucket
            foreach (var pair in items[hashIndex])
            {
                if (pair.Key.Equals(key))
                {
                    value = pair.Value;
                    return true;
                }
            }
        }

        return false;
    }

    private int GetHashIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % Capacity;
    }

    public void Remove(TKey key)
    {
        int bucketIndex = GetHashIndex(key);

        if (items[bucketIndex] != null)
        {
            for (int i = 0; i < items[bucketIndex].Count; i++)
            {
                var pair = items[bucketIndex][i];
                if (pair != null && pair.Key.Equals(key))
                {
                    items[bucketIndex].RemoveAt(i);
                    return;
                }
            }
        }
    }

}

class Program
{
    static void Main()
    {
        var hashTable = new HashTable<string, int>();

        hashTable.Insert("One", 1);
        hashTable.Insert("Two", 2);
        hashTable.Insert("Three", 3);

        int value = 0;
        if(hashTable.SearchKey("Two", ref value))
        {
            Console.WriteLine(value);
        } else
        {
            Console.WriteLine("Value against Two does not exit");
        }

        if (hashTable.SearchKey("Four", ref value))
        {
            Console.WriteLine("Value against Two: " + value);
        }
        else
        {
            Console.WriteLine("Value against Four does not exit");
        }
    }
}
