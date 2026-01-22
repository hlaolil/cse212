using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Items are added in order and internal list should preserve insertion order
    // Expected Result: ToString() shows items exactly in the order they were enqueued
    // Defect(s) Found: None (Enqueue appears to work correctly)
    public void TestPriorityQueue_Enqueue_PreservesInsertionOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("apple", 5);
        priorityQueue.Enqueue("banana", 10);
        priorityQueue.Enqueue("cherry", 3);
        priorityQueue.Enqueue("date", 7);

        string expected = "[apple (Pri:5), banana (Pri:10), cherry (Pri:3), date (Pri:7)]";
        Assert.AreEqual(expected, priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeue from queue with different priorities → should return highest priority
    // Expected Result: Returns "banana" (priority 10), then "date" (7), then "apple" (5), then "cherry" (3)
    // Defect(s) Found: 
    // 1. Item is not removed from the queue
    // 2. When priorities are equal it returns the LAST matching item instead of the FIRST
    // 3. Loop does not check the last item (goes only to Count-2)
    public void TestPriorityQueue_Dequeue_ReturnsHighestPriorityAndRemovesIt()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("apple", 5);
        priorityQueue.Enqueue("banana", 10);
        priorityQueue.Enqueue("cherry", 3);
        priorityQueue.Enqueue("date", 7);

        Assert.AreEqual("banana", priorityQueue.Dequeue(), "Should return highest priority item");
        Assert.AreEqual("[apple (Pri:5), cherry (Pri:3), date (Pri:7)]", priorityQueue.ToString());

        Assert.AreEqual("date", priorityQueue.Dequeue(), "Next highest");
        Assert.AreEqual("[apple (Pri:5), cherry (Pri:3)]", priorityQueue.ToString());

        Assert.AreEqual("apple", priorityQueue.Dequeue());
        Assert.AreEqual("[cherry (Pri:3)]", priorityQueue.ToString());

        Assert.AreEqual("cherry", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Multiple items with the same highest priority → should return the earliest one (FIFO)
    // Expected Result: Returns first → third → fourth (all priority 100), then lower priorities
    // Defect(s) Found: 
    // 1. Uses >= instead of > → prefers later items when priorities are equal (breaks FIFO)
    // 2. Does not remove the item
    public void TestPriorityQueue_Dequeue_UsesFIFOForEqualPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 100);
        priorityQueue.Enqueue("second", 20);
        priorityQueue.Enqueue("third", 100);
        priorityQueue.Enqueue("fourth", 100);
        priorityQueue.Enqueue("fifth", 50);

        Assert.AreEqual("first", priorityQueue.Dequeue(), "Should return earliest item with priority 100");
        Assert.AreEqual("third", priorityQueue.Dequeue(), "Next item with priority 100");
        Assert.AreEqual("fourth", priorityQueue.Dequeue(), "Last item with priority 100");

        Assert.AreEqual("fifth", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());

        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from empty queue
    // Expected Result: Throws InvalidOperationException with correct message
    // Defect(s) Found: None (empty check and exception message are correct)
    public void TestPriorityQueue_Dequeue_EmptyQueue_ThrowsCorrectException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Single item – enqueue then dequeue
    // Expected Result: Returns the only item and queue becomes empty
    // Defect(s) Found: 
    // 1. Item not removed (would fail on second dequeue if not fixed)
    public void TestPriorityQueue_SingleItem_DequeueWorks()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only-one", 42);

        Assert.AreEqual("only-one", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());

        // Second dequeue should throw
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should throw after dequeuing the only item");
        }
        catch (InvalidOperationException) { }
    }

    [TestMethod]
    // Scenario: Mix of priorities, multiple dequeues, check order and removal
    // Expected Result: Correct priority order + FIFO for ties + all items removed
    public void TestPriorityQueue_MixedOperations_CorrectOrderAndRemoval()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 8);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 10);
        priorityQueue.Enqueue("E", 8);
        priorityQueue.Enqueue("F", 8);

        Assert.AreEqual("D", priorityQueue.Dequeue());  // 10
        Assert.AreEqual("B", priorityQueue.Dequeue());  // 8 (earlier than E and F)
        Assert.AreEqual("E", priorityQueue.Dequeue());  // 8
        Assert.AreEqual("F", priorityQueue.Dequeue());  // 8
        Assert.AreEqual("A", priorityQueue.Dequeue());  // 3 (earlier than C)
        Assert.AreEqual("C", priorityQueue.Dequeue());  // 3

        Assert.AreEqual("[]", priorityQueue.ToString());
    }
}