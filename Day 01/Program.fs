
open System
open System.IO


[<EntryPoint>]
let main _ =

    let digits =
        File.ReadAllText "Inputs.txt"
        |> Array.ofSeq
        |> Array.map (Convert.ToString >> int)
        
    Array.append digits [| digits.[0] |]
    |> Array.pairwise
    |> Array.sumBy (function | x, y when x = y -> x | _ -> 0)
    |> printfn "Part 1 answer = %i\n"

    digits
    |> Array.mapi (fun idx d -> (d, digits.[(idx + digits.Length / 2) % digits.Length]))
    |> Array.sumBy (function | x, y when x = y -> x | _ -> 0)
    |> printfn "Part 2 answer = %i"
      
    0