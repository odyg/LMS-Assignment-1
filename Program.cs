var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

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
    else if (path.StartsWith("/dashboard"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("This is the Dashboard.");
        }
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
                //?rId=012&rName=John Doe ---- This is the format for entering reader
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
            //this is the format for entering reader "rId: 002 rName: John Doe"
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
    else if (path.StartsWith("/borrowings"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;

            if (context.Request.Query.ContainsKey("BookId") && context.Request.Query.ContainsKey("rName"))
            {
                //?BookId=012&rName=John Doe ---- This is the format for entering reader
                string BookId = context.Request.Query["BookId"];
                string ReaderName = context.Request.Query["rName"];
                await context.Response.WriteAsync("Book ID: " + BookId + " is borrowed by: " + ReaderName);
                return;
            }

            await context.Response.WriteAsync("This is the readers management page");

        }
        else if (method == "POST")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "BookId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "BookId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var BookId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {BookId}, has been borrowed by: {ReaderName}.");
            }
        }
        else if (method == "PUT")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();

            var parts = data.Split(new[] { "BookId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "BookId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var BookId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {BookId}, borrowed by: {ReaderName} has been updated in the database.");
            }
        }
        else if (method == "DELETE")
        {
            StreamReader reader = new StreamReader(context.Request.Body);
            string data = await reader.ReadToEndAsync();
            var parts = data.Split(new[] { "BookId:", "rName:" }, StringSplitOptions.RemoveEmptyEntries);
            //this is the format for entering book "BookId: 002 rName: John Doe"
            if (parts.Length == 2)
            {
                var BookId = parts[0].Trim();
                var ReaderName = parts[1].Trim();

                await context.Response.WriteAsync($"Book ID: {BookId}, has been returned by: {ReaderName}.");
            }
        }
    }
    else if (path.StartsWith("/report"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("This is the Report page. You will be able to generate \nreports based on various criteria, such as borrowing dates,\nbook types, or reader demographics.");
        }
    }
    else if (path.StartsWith("/overdue"))
    {
        if (method == "GET")
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("This is the Overdue Page. You will be able to generate \na list of books that are overdue with outstanding charges in this page.");
        }
    }
     else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("This page is not found!");
    }
});


app.Run();
