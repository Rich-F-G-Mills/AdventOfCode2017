
open System
open System.IO
open FSharpx


[<EntryPoint>]
let main _ =

    let rowValues =
        File.ReadAllLines "Inputs.txt"
        |> Array.map (String.splitCharWithOptions [| '\t' |] StringSplitOptions.RemoveEmptyEntries)
        |> Array.map (Array.map int)

    rowValues
    |> Array.map (fun row -> (Array.max row, Array.min row))
    |> Array.map ((<||) (-))
    |> Array.sum
    |> printfn "Part 1 answer = %i\n"

    rowValues
    |> Array.map (fun row ->
        Array.allPairs row row
        |> Array.filter ((<||) (>))
        |> Array.map (fun (x, y) -> (x / y, x % y))
        |> Array.find (function | _, 0 -> true | _ -> false))
    |> Array.sumBy fst
    |> printfn "Part 2 answer = %i"

    0