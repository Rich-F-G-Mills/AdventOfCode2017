
open System
open System.IO
open FSharpx


let hasDuplicates arr =
    arr
    |> Array.distinct
    |> Array.length <> arr.Length

[<EntryPoint>]
let main _ =
    let words =
        File.ReadAllLines "Inputs.txt"
        |> Array.map (String.splitChar [| ' ' |])

    words
    |> Array.filter (not << hasDuplicates)
    |> Array.length
    |> printfn "Part 1 answer = %i\n"

    words
    |> Array.map (Array.map (Seq.sort >> Array.ofSeq >> String))
    |> Array.filter (not << hasDuplicates)
    |> Array.length
    |> printfn "Part 2 answer = %i"

    0