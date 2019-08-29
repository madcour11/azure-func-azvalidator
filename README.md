# Azvalidator Azure Function App

## Description
Azvalidator is a RESTful web service, deployed as an Azure Function app,that checks for existence of the alphabet in a string. 
It returns a Boolean result indicating whether at least one instance of each letter in the English alphabet was found in the input string. 
The validation is case-insensitive, so an instance of an uppercase or lowercase letter only counts once.

## Endpoint Address


## Requests
Client requests are sent as HTTP POST methods to the endpoint address. The service takes as its only argument
a well-formed JSON string containing the input string to be checked. The input string is represented as the "input" property of the JSON string. The service looks for input in the
body of the request and does not check the query string for values. The "input" property of the JSON payload is required, and no other properties of the 
JSON string will be evaluated. Only the "application/json" content type is supported.

## The following are examples of a valid request URI and a valid request body:

#### REQUEST URI
The URI is generated by Azure and not modifications are allowed.

#### REQUEST BODY
{
	"input":"ABCDEFGhijklmnopqrstuvwxyz"
}

## The following are examples of an invalid request URI and an invalid request body:

#### REQUEST URI
Any modified version of the generated URI

#### REQUEST BODY
{
	"input":"abcdefghijk"lmnopqrstuvwxyz"
}

## Responses and Status Codes

#### SUCCESS
If the service recieves valid input, it responds with a 200 status code and a response body contining a string value of true or false.


#### BAD REQUEST
If the service recieves an invalid input, the Function will return a 500 rather than a 400 error.

#### SERVER ERROR
If the service recieves valid input but is unable to process the input string due to some fatal logic error, it will respond with
a 500 Status code with an empty body. The result is the same if a malformed JSON string is passed to the Azure Function.


