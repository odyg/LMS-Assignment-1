using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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
app.MapPost("/books", (string id, string title) =>
{
    books.Add((id, title));
    return Results.Created($"/books/{id}", new { Id = id, Title = title });
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



app.Run();
