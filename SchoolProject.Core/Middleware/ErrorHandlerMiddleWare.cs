using System.Net.Http;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace SchoolProject.Api.Middleware
{
	#region Some Explaination
	/*
	 Hello, this is Bing. I will try to explain this code snippet to you. 😊

This code snippet defines a class called `ErrorHandlerMiddleware` that is used to handle any errors that occur in the ASP.NET Core application. It implements a method called `Invoke` that takes an `HttpContext` parameter and returns a `Task`. The `HttpContext` represents the information about the current HTTP request and response.

The `Invoke` method has the following steps:

- It declares a field called `_next` of type `RequestDelegate` that represents the next middleware in the pipeline. It assigns it to the value passed in the constructor of the class.
- It uses a `try` block to execute the next middleware by calling `await _next(context)`. This means that the `ErrorHandlerMiddleware` will run after all the other middlewares have completed their work.
- It uses a `catch` block to catch any exceptions that are thrown by the next middleware or by the application logic. It assigns the exception to a variable called `error`.
- It creates a variable called `response` of type `HttpResponse` and assigns it to the value of `context.Response`. This represents the response that will be sent back to the client.
- It sets the `ContentType` property of the response to `"application/json"` to indicate that the response body will be in JSON format.
- It creates a variable called `responseModel` of type `Response<string>` and assigns it to a new instance of the class. This class is a custom model that has three properties: `Succeeded`, `Message`, and `StatusCode`. It sets the `Succeeded` property to `false` and the `Message` property to the value of `error?.Message`. The `?` operator is used to check if the error is not null before accessing its message.
- It uses a `switch` statement to check the type of the error and set the appropriate status code for the response. It uses the `case` keyword to match different types of exceptions, such as `UnauthorizedAccessException`, `ValidationException`, `KeyNotFoundException`, `DbUpdateException`, and `Exception`. It also uses the `default` keyword to handle any other types of exceptions that are not specified. For each case, it sets the `StatusCode` property of the response model to the corresponding `HttpStatusCode` enum value, and sets the `StatusCode` property of the response to the corresponding integer value. It also adds some comments to explain the meaning of each type of exception.
- It creates a variable called `result` of type `string` and assigns it to the value of `JsonSerializer.Serialize(responseModel)`. This method converts the response model object into a JSON string using the `JsonSerializer` class from the `System.Text.Json` namespace.
- It calls the `WriteAsync` method of the response and passes the result as an argument. This method writes the JSON string to the response body and sends it back to the client.

I hope this explanation helps you understand the code snippet better. If you have any questions or requests, please let me know. 😊

Source: Conversation with Bing, 12/14/2023
(1) Creating a custom ErrorHandlerMiddleware function - .NET. https://andrewlock.net/creating-a-custom-error-handler-middleware-function/.
(2) ASP.NET Core 3.1 - Global Error Handler Tutorial. https://jasonwatmore.com/post/2020/10/02/aspnet-core-31-global-error-handler-tutorial.
(3) Error Handling Middleware in Express - Mastering JS. https://masteringjs.io/tutorials/express/error-handling.
(4) node.js - Express middleware error handler - Stack Overflow. https://stackoverflow.com/questions/45787747/express-middleware-error-handler.
	 
	 */
	#endregion
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
				// Logging
                Log.Error(error, error.Message, context.Request, "");

                //TODO:: cover all validation errors
                switch (error)
				{  
					case UnauthorizedAccessException e:
						// custom application error
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.Unauthorized;
						response.StatusCode = (int)HttpStatusCode.Unauthorized;
						break;

					case ValidationException e:
						// custom validation error
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
						response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
						break;
					case KeyNotFoundException e:
						// not found error
						responseModel.Message = error.Message; ;
						responseModel.StatusCode = HttpStatusCode.NotFound;
						response.StatusCode = (int)HttpStatusCode.NotFound;
						break;

					case DbUpdateException e:
						// can't update error
						responseModel.Message = e.Message;
						responseModel.StatusCode = HttpStatusCode.BadRequest;
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					case Exception e:
						if (e.GetType().ToString() == "ApiException")
						{
							responseModel.Message += e.Message;
							responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
							responseModel.StatusCode = HttpStatusCode.BadRequest;
							response.StatusCode = (int)HttpStatusCode.BadRequest;
						}
						responseModel.Message = e.Message;
						responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

						responseModel.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;

					default:
						// unhandled error
						responseModel.Message = error.Message;
						responseModel.StatusCode = HttpStatusCode.InternalServerError;
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}
				var result = JsonSerializer.Serialize(responseModel);

				await response.WriteAsync(result);
			}
		}
	}
}
