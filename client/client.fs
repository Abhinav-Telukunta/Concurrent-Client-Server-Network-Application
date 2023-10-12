open System
open System.Net.Sockets
open System.IO

let serverAddress = "127.0.0.1"
let serverPort = 12345

let connectToServer () =
    try
        // establishing new TCP client, obtain network stream and create readers and writers to read and write from stream
        let client = new TcpClient(serverAddress, serverPort)
        let stream = client.GetStream()
        let reader = new StreamReader(stream)
        let writer = new StreamWriter(stream)
        writer.AutoFlush <- true

        printfn "Received from server: %s" (reader.ReadLine()) // Read and print the initial "Hello" message

        let mutable shouldExit = false

        try
            while not shouldExit do
                printf "Sending command: "
                let userInput = Console.ReadLine()
                writer.WriteLine(userInput) // sends user input to server
                let response = reader.ReadLine() // receives server response
                match response with
                | "-1" -> 
                    printfn "Server response: incorrect operation command"
                | "-2" ->
                    printfn "Server response: number of inputs is less than two"
                | "-3" ->
                    printfn "Server response: number of inputs is more than four"
                | "-4" ->
                    printfn "Server response: one or more of the inputs contain(s) non-number(s)"
                | response ->
                    if response = "-5" then
                        if userInput = "bye" then 
                            printfn "exit"
                        shouldExit <- true
                    else 
                        printfn "Server response: %s" response
            
        with ex ->
            if not client.Connected then // if client socket is closed, then this would execute
                printfn "Client socket closed!!"

    with
    | ex ->
        printfn "%s" ex.Message

[<EntryPoint>]
let main argv =
    connectToServer()
    0 