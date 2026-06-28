---

title: 2. Repository Pattern Implementation
author: Marcus Ackre Medina
type: lecture
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/3_oop_adv/lectures/05_mysql_integration/2_example.md"
description: "// Example 1: Order Management System using Repository Pattern"
tags: ["csharp", "implementation", "oop", "pattern", "repository", "visual-studio"]
week_fit: []
---

# 2. Repository Pattern Implementation

🟢


```csharp
// Example 1: Order Management System using Repository Pattern
// Handles orders, customers and inventory with transaction support

public interface IRepository<T, TId>
{
    Task<T?> FindByIdAsync(TId id);
    Task<IEnumerable<T>> FindAllAsync();
    Task<T> SaveAsync(T entity);
    Task DeleteAsync(TId id);
    Task<bool> ExistsAsync(TId id);
}

public class Order
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    public Order(long id, long customerId, DateTime orderDate, 
                OrderStatus status, decimal totalAmount)
    {
        Id = id;
        CustomerId = customerId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = totalAmount;
    }
}

public class OrderRepository : AbstractRepository<Order, long>
{
    public OrderRepository(IDbConnection connection) 
        : base(connection, "orders")
    {
    }

    protected override Order MapDataReaderToEntity(IDataReader reader)
    {
        return new Order(
            reader.GetInt64(reader.GetOrdinal("Id")),
            reader.GetInt64(reader.GetOrdinal("CustomerId")),
            reader.GetDateTime(reader.GetOrdinal("OrderDate")),
            Enum.Parse<OrderStatus>(reader.GetString(reader.GetOrdinal("Status"))),
            reader.GetDecimal(reader.GetOrdinal("TotalAmount"))
        );
    }

    public async Task<IEnumerable<Order>> FindByCustomerIdAsync(long customerId)
    {
        const string sql = "SELECT * FROM orders WHERE CustomerId = @CustomerId";
        var results = new List<Order>();

        using var command = Connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new SqlParameter("@CustomerId", customerId));

        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(MapDataReaderToEntity(reader));
            }
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error finding orders", ex);
        }

        return results;
    }

    public async Task UpdateStatusAsync(long orderId, OrderStatus status)
    {
        const string sql = "UPDATE orders SET Status = @Status WHERE Id = @Id";

        using var command = Connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new SqlParameter("@Status", status.ToString()));
        command.Parameters.Add(new SqlParameter("@Id", orderId));

        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error updating order status", ex);
        }
    }
}
```

```csharp
// Example 2: Library Management System using Repository Pattern
// Manages books, members and loans with transaction support

public class Book
{
    public long Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Copies { get; set; }
    public int AvailableCopies { get; set; }

    public Book(long id, string isbn, string title, string author, 
                int copies, int availableCopies)
    {
        Id = id;
        Isbn = isbn;
        Title = title;
        Author = author;
        Copies = copies;
        AvailableCopies = availableCopies;
    }
}

public class BookRepository : AbstractRepository<Book, long>
{
    public BookRepository(IDbConnection connection) 
        : base(connection, "books")
    {
    }

    protected override Book MapDataReaderToEntity(IDataReader reader)
    {
        return new Book(
            reader.GetInt64(reader.GetOrdinal("Id")),
            reader.GetString(reader.GetOrdinal("Isbn")),
            reader.GetString(reader.GetOrdinal("Title")),
            reader.GetString(reader.GetOrdinal("Author")),
            reader.GetInt32(reader.GetOrdinal("Copies")),
            reader.GetInt32(reader.GetOrdinal("AvailableCopies"))
        );
    }

    public async Task<IEnumerable<Book>> FindByAuthorAsync(string author)
    {
        const string sql = "SELECT * FROM books WHERE Author LIKE @Author";
        var results = new List<Book>();

        using var command = Connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new SqlParameter("@Author", $"%{author}%"));

        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(MapDataReaderToEntity(reader));
            }
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error finding books", ex);
        }

        return results;
    }

    public async Task<bool> UpdateAvailabilityAsync(long bookId, int change)
    {
        const string sql = @"UPDATE books 
                          SET AvailableCopies = AvailableCopies + @Change 
                          WHERE Id = @Id 
                          AND AvailableCopies + @Change >= 0";

        using var command = Connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.Add(new SqlParameter("@Change", change));
        command.Parameters.Add(new SqlParameter("@Id", bookId));

        try
        {
            return await command.ExecuteNonQueryAsync() > 0;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Error updating book availability", ex);
        }
    }
}
```

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
