
open System
open System.IO
open FSharpx


[<EntryPoint>]
let main _ =

    let digits =
        File.ReadAllText "Inputs.txt"
        |> List.ofSeq
        |> List.map (Convert.ToString >> int)        
            
    0