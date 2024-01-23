using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

// Using tuples for simplicity
var books = new List<(string Id, string Title)>();
var borrowings = new List<(string Id, string BookId, string ReaderId)>();
var readers = new List<(string Id, string Name)>();

// API Endpoints
// Books
//app.MapGet("/books", () => books);
//app.MapPost("/books", (string id, string title) =>
//{
//    books.Add((id, title));
//    return Results.Created($"/books/{id}", new { Id = id, Title = title });
//});
//app.MapPut("/books/{id}", (string id, string newTitle) =>
//{
//    var bookIndex = books.FindIndex(b => b.Id == id);
//    if (bookIndex != -1)
//    {
//        books[bookIndex] = (id, newTitle);
//        return Results.NoContent();
//    }
//    return Results.NotFound();
//});
//app.MapDelete("/books/{id}", (string id) =>
//{
//    var bookIndex = books.FindIndex(b => b.Id == id);
//    if (bookIndex != -1)
//    {
//        books.RemoveAt(bookIndex);
//        return Results.NoContent();
//    }
//    return Results.NotFound();
//});


// Http request method - GET, POST, PUT, PATCH, DELETE
app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    if (path.StartsWith("/login"))
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("This is the login page.");
    }
    else if (path.StartsWith("/books"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;

            if (context.Request.Query.ContainsKey("id") && context.Request.Query.ContainsKey("name"))
            {
                string id = context.Request.Query["id"];
                string name = context.Request.Query["name"];
                await context.Response.WriteAsync("Book ID is:" + id + " Book Name is:" + name);
                return;
            }

            await context.Response.WriteAsync("This is the books library page");

        }
        else if (method == "POST")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "id:", "book:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "id: 002 book: The TV"
            if (parts.Length == 2)
            {
                var id = parts[0].Trim();
                var name = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {id}, Book Name: {name} has been added to the database.");
            }
        }
        else if (method == "PUT")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "id:", "book:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "id: 002 book: The TV"
            if (parts.Length == 2)
            {
                var id = parts[0].Trim();
                var name = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {id}, Book Name: {name} has been updated in the database.");
            }
        }
        else if (method == "DELETE")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "id:", "book:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "id: 002 book: The TV"
            if (parts.Length == 2)
            {
                var id = parts[0].Trim();
                var name = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {id}, Book Name: {name} has been successfully deleted.");
            }
        }
    }
    else if (path.StartsWith("/readers"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;

            if (context.Request.Query.ContainsKey("rId") && context.Request.Query.ContainsKey("rName"))
            {
                string ReaderId = context.Request.Query["rId"];
                string ReaderName = context.Request.Query["rName"];
                await context.Response.WriteAsync("Reader ID is:" + ReaderId + " Reader Name is:" + ReaderName);
                return;
            }

            await context.Response.WriteAsync("This is the readers management page");

        }
        else if (method == "POST")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "rId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "rId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var ReaderId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Reader ID: {ReaderId}, Reader Name: {ReaderName} has been added to the readers database.");
            }
        }
        else if (method == "PUT")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "rId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "rId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var ReaderId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Reader ID: {ReaderId}, Reader Name: {ReaderName} has been updated in the readers database.");
            }
        }
        else if (method == "DELETE")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();
            var parts = data.Split(new[] { "rId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "rId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var ReaderId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Reader ID: {ReaderId}, Reader Name: {ReaderName} has been successfully deleted.");
            }
        }
    }
    //if (path == "/login")
    //{
    //    context.Response.StatusCode = 200;
    //    await context.Response.WriteAsync("This is the LOGIN PAGE");
    //}

    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("This page is not found!");
    }
});


app.Run();
