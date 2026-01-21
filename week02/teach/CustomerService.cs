/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1: Size of Queue
        // Scenario: input invalid size (0 or negative)
        // Expected Result: must defualt to 10
        var cs1 = new CustomerService(-5);
        Console.WriteLine(cs1);

        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Test 2: Add New Customer
        // Scenario: Add customers until queue is full, then try to add one more
        // Expected Result: After adding maxSize customers → shows "Maximum Number of Customers in Queue."
        var cs3 = new CustomerService(3);
        
        cs3.AddNewCustomer(); // 1
        cs3.AddNewCustomer(); // 2
        cs3.AddNewCustomer(); // 3
        Console.WriteLine(cs3); // size should be 3

        cs3.AddNewCustomer(); // should show error message
        Console.WriteLine(cs3); // size should still be 3

        // Defect(s) Found: continues to add customers beyond max size

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
        //Test 3: Serve Customer
        // Scenario: Add customers and serve them one by one
        // Expected Result: Customers are served in the order they were added (FIFO)
        var cs4 = new CustomerService(3);
        cs4.AddNewCustomer(); // 1
        cs4.AddNewCustomer(); // 2
        cs4.AddNewCustomer(); // 3
        Console.WriteLine(cs4); // size should be 3
        cs4.ServeCustomer(); // serves 1
        Console.WriteLine(cs4); // size should be 2

        cs4.ServeCustomer(); // serves 2
        cs4.ServeCustomer(); // serves 3
        Console.WriteLine(cs4); // size should be 0

        cs4.ServeCustomer(); // should show error message
        // Defect(s) Found: does not handle serving from an empty queue

        Console.WriteLine(cs4);

    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count > _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        _queue.RemoveAt(0);
        var customer = _queue[0];
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}