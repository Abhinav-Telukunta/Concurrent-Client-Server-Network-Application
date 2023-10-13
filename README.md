## The goal of the project is to create a concurrent client/server application that uses the TCP/IP protocol stack and socket communication.

## Server-Side Program:
The server-side program has been designed to operate continuously while it waits for new connections from clients. It must manage asynchronous operations to handle multiple customers' requests at once.
## Client-Side Program:
After the server has started running, clients are started on separate machines. The client application, which is written in F#, connects to the server and runs concurrently and asynchronously.
## Error Handling:
The server program is responsible for handling unexpected inputs and errors. Error codes (negative numbers) are used to represent various error scenarios, such as incorrect commands, insufficient or excessive 
inputs, or non-numeric inputs.
The server program generates appropriate error codes for incorrect commands and communicates these codes to the client. Clients print corresponding error messages when receiving error codes.
## Termination:
Both the server and the clients must terminate. There shouldn't be any lingering threads or processes after termination.
