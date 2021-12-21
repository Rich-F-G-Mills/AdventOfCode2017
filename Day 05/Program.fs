
open System.IO


let part1Processor (offsets: int array) offset _ =
    let newOffset = offset + offsets.[offset]
    
    offsets.[offset] <- offsets.[offset] + 1
    
    newOffset

let part2Processor (offsets: int array) offset _ =
    let newOffset = offset + offsets.[offset]
    
    offsets.[offset] <-
        offsets.[offset] + (if offsets.[offset] < 3 then 1 else -1)
    
    newOffset


let runProcessor proc (offsets: int array) =
    Seq.initInfinite id
    |> Seq.scan (proc offsets) 0
    |> Seq.indexed
    |> Seq.find (fun (_, offset) -> offset < 0 || offset >= offsets.Length)
    |> fst


[<EntryPoint>]
let main _ =

    let offsets =
        File.ReadAllLines "Inputs.txt"
        |> Array.map int

    let offsets' = offsets |> Array.copy in
        runProcessor part1Processor offsets'
        |> printfn "Part 1 answer = %i\n"

    let offsets' = offsets |> Array.copy in
        runProcessor part2Processor offsets'
        |> printfn "Part 2 answer = %i"

    0