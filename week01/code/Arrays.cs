public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create a new array of doubles with the specified length.
        // Step 2: Use a loop to populate the array, where each index i gets the value (i + 1) * number.
        // Step 3: Return the populated array.

        double[] result = new double[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (i + 1) * number;
        }
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Calculate the starting index for the elements that will be moved to the front.
        // Step 2: Create a temporary list to hold the last 'amount' elements of the data list.
        // Step 3: Loop from the starting index to the end and add to the temp list.
        // Step 4: Remove the last 'amount' elements from the original data list using RemoveRange.
        // Step 5: Insert the temporary list at the beginning of the data list using InsertRange.

        int startIndex = data.Count - amount;
        List<int> temp = new List<int>();
        for (int i = startIndex; i < data.Count; i++)
        {
            temp.Add(data[i]);
        }
        data.RemoveRange(startIndex, amount);
        data.InsertRange(0, temp);
    }
}
