The goal of the project is to create a concurrent client/server application that uses the TCP/IP protocol stack and socket communication.

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

## Example Server side Responses:
![image](https://github.com/Abhinav-Telukunta/Concurrent-Client-Server-Network-Application/assets/62216101/9fa77d3e-32c4-4577-8e70-f5496f351099)

## Example Client Responses:
![image](https://github.com/Abhinav-Telukunta/Concurrent-Client-Server-Network-Application/assets/62216101/2f6dabd5-8aa3-404f-b628-ae362dff7faa)

![image](https://github.com/Abhinav-Telukunta/Concurrent-Client-Server-Network-Application/assets/62216101/5c307dd3-8797-4c1a-9e0e-0776aa870c7a)

![image](https://github.com/Abhinav-Telukunta/Concurrent-Client-Server-Network-Application/assets/62216101/fea957b8-3290-4529-b093-a9d00b6b83fa)

![image](https://github.com/Abhinav-Telukunta/Concurrent-Client-Server-Network-Application/assets/62216101/993795b8-ea6b-449c-88a3-86ad684928ad)





