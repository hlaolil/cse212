using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    // Problem 1: Sum of squares 1² + 2² + ... + n²
    public static int SumSquaresRecursive(int n)
    {
        // Base cases
        if (n <= 0) return 0;
        if (n == 1) return 1;

        // Recursive case: n² + sum of squares up to (n-1)
        return n * n + SumSquaresRecursive(n - 1);
    }

    // Problem 2: Generate all permutations of exactly 'size' letters from the given string
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: we have built a permutation of the desired length
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Try each remaining letter
        for (int i = 0; i < letters.Length; i++)
        {
            char next = letters[i];
            string remaining = letters.Substring(0, i) + letters.Substring(i + 1);
            PermutationsChoose(results, remaining, size, word + next);
        }
    }

    // Problem 3: Number of ways to climb s stairs (1, 2, or 3 steps at a time) with memoization
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize memoization dictionary on first call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base cases
        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // Check if already computed
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Recursive case with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    // Problem 4: Generate all binary strings matching the pattern with * wildcards
    public static void WildcardBinary(string pattern, List<string> results, string current = "")
    {
        // Base case: we've processed the entire pattern
        if (current.Length == pattern.Length)
        {
            results.Add(current);
            return;
        }

        // Find the next character in the pattern
        char ch = pattern[current.Length];

        if (ch == '*')
        {
            // Try both 0 and 1
            WildcardBinary(pattern, results, current + "0");
            WildcardBinary(pattern, results, current + "1");
        }
        else
        {
            // Fixed character — must use it
            WildcardBinary(pattern, results, current + ch);
        }
    }

    // Wrapper for cleaner call (matches the signature in tests)
    public static void WildcardBinary(string pattern, List<string> results)
    {
        WildcardBinary(pattern, results, "");
    }

    // Problem 5: Find ALL paths from (0,0) to the end (2) in the maze
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize path on first call
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add current position to path
        currPath.Add((x, y));

        // Check if we've reached the end
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            // Backtrack — remove current position before returning
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Try moving in four directions: down, right, up, left
        (int dx, int dy)[] directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };

        foreach (var (dx, dy) in directions)
        {
            int nextX = x + dx;
            int nextY = y + dy;

            if (maze.IsValidMove(currPath, nextX, nextY))
            {
                SolveMaze(results, maze, nextX, nextY, currPath);
            }
        }

        // Backtrack: remove current position after exploring all possibilities
        currPath.RemoveAt(currPath.Count - 1);
    }
}