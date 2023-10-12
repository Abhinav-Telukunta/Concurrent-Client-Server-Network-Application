open System
open System.Net
open System.Net.Sockets
open System.IO
open System.Threading

let port = 12345
let mutable clientCounter=0 // counter to keep track of number of clients
let mutable clientSockets = new System.Collections.Concurrent.ConcurrentBag<TcpClient>() // array of all client sockets connected to server

let handleClient (clientSocket: TcpClient) (listener: TcpListener) =
    try
        // obtain client network stream and create readers and writers to read and write from stream
        let stream = clientSocket.GetStream()
        let reader = new StreamReader(stream)
        let writer = new StreamWriter(stream)
        writer.AutoFlush <- true

        writer.WriteLine("Hello!") //sending "Hello!" response to clients
        let mutable continueReading = true
        let clientNum=Interlocked.Increment(&clientCounter) // setting appropriate client number

        while continueReading do
            try
                let request = reader.ReadLine() // reads client request from here
                printfn "Received: %s" request
                match request with
                | null -> continueReading <- false // Connection closed by the client
                | "bye" ->
                    writer.WriteLine("-5")
                    printfn "Responding to client %d with result: %d" clientNum -5
                    continueReading <- false
                | "terminate" ->
                    writer.WriteLine("-5")
                    printfn "Responding to client %d with result: %d" clientNum -5
                    continueReading <- false
                    clientSockets |> Seq.iter (fun client -> client.Close()) // close all client sockets
                | request ->
                    let parts = request.Split(' ')
                    let mutable errorCode = 0
                    match parts.Length with
                    | length when length >= 3 && length <= 5 && (parts.[0] = "add" || parts.[0] = "subtract" || parts.[0] = "multiply") ->
                        // Validate and parse the operands
                        let operands = parts.[1..] |> Array.map (fun str ->
                            match Int32.TryParse(str) with
                            | (true, num) -> (true, num)
                            | _ -> (false, 0))

                        // Check if parsing was successful for all operands
                        if Array.forall fst operands then
                            // Ensure there are at least 2 and at most 4 operands
                            let result =
                                match parts.[0] with
                                | "add" -> Array.sum (Array.map snd operands)
                                | "subtract" -> Array.reduce (-) (Array.map snd operands)
                                | "multiply" -> Array.reduce (*) (Array.map snd operands)
                                | _ -> 0

                            writer.WriteLine(sprintf "%d" result)
                            printfn "Responding to client %d with result: %d" clientNum result
                        else
                            writer.WriteLine("-4")
                            printfn "Responding to client %d with result: %d" clientNum -4

                    | len ->
                        if (parts.[0] = "add" || parts.[0] = "subtract" || parts.[0] = "multiply") then
                            if len < 3 then // operation is valid but number of operands is less than two
                                writer.WriteLine("-2")
                                printfn "Responding to client %d with result: %d" clientNum -2
                            else // operation is valid but number of operands is more than four
                                writer.WriteLine("-3")
                                printfn "Responding to client %d with result: %d" clientNum -3
                        else
                            // Invalid operation
                            writer.WriteLine("-1")
                            printfn "Responding to client %d with result: %d" clientNum -1
            with
            | ex -> 
                continueReading <- false

        if not clientSocket.Connected then // if client sockets are closed, then stop the TCP listener
            listener.Stop() 
        
    with
    | ex -> printfn "%s" ex.Message

    clientSocket.Close()

let startServer () =
    let ipAddress = IPAddress.Parse("127.0.0.1")
    let listener = TcpListener(ipAddress, port) // create TCP listener
    let mutable terminateFlag = false
    listener.Start()

    printfn "Server is running and listening on port %d." port

    while not terminateFlag do
        try 
            let client = listener.AcceptTcpClient() // looks for incoming client connections and accepts
            clientSockets.Add(client) // add new client to clientSockets array
            System.Threading.Tasks.Task.Run(fun () -> handleClient client listener) |> ignore //create async task to handle each client
        with
        | ex ->
            terminateFlag <- true // when listener stops, then it can't accept client connections, so then terminateFlag would be true



[<EntryPoint>]
let main argv =
    startServer()
    0