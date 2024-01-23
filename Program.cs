using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

// Using tuples for simplicity
var books = new List<(string Id, string Title)>();
var borrowings = new List<(string Id, string BookId, string ReaderId)>();
var readers = new List<(string Id, string Name)>();

// API Endpoints
// Books
app.MapGet("/books", () => books);
app.MapPost("/books", async (HttpContext context) =>
{
    // Read the request body
    StreamReader reader = new StreamReader(context.Request.Body);
    string data = await reader.ReadToEndAsync();

    // Deserialize the JSON data to a Dictionary
    var bookData = JsonSerializer.Deserialize<Dictionary<string, string>>(data);

    if (bookData != null && bookData.ContainsKey("Id") && bookData.ContainsKey("Title"))
    {
        // Extract the book details
        var id = bookData["Id"];
        var title = bookData["Title"];

        // Add the new book
        books.Add((id, title));

        // Respond with the added book details
        await context.Response.WriteAsJsonAsync(new { Id = id, Title = title });
        context.Response.StatusCode = StatusCodes.Status201Created;
    }
    else
    {
        // Respond with a simple message for invalid data
        await context.Response.WriteAsync("Invalid book data.");
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
});

app.MapPut("/books/{id}", (string id, string newTitle) =>
{
    var bookIndex = books.FindIndex(b => b.Id == id);
    if (bookIndex != -1)
    {
        books[bookIndex] = (id, newTitle);
        return Results.NoContent();
    }
    return Results.NotFound();
});
app.MapDelete("/books/{id}", (string id) =>
{
    var bookIndex = books.FindIndex(b => b.Id == id);
    if (bookIndex != -1)
    {
        books.RemoveAt(bookIndex);
        return Results.NoContent();
    }
    return Results.NotFound();
});

// ... Similar setup for borrowings and readers ...

app.Run();
