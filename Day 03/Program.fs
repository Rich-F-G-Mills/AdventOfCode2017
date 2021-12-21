
open System

let [<Literal>] PuzzleInput = 325489


type Cell =
    { CellIdx: int
      LayerIdx: int
      X: int
      Y: int }


[<EntryPoint>]
let main _ =

    let spiralCells =        
        let rec genCoordsForLayer layerIdx =         
            seq {
                let layerDim = 1 + 2 * layerIdx
                let layerHalfDim = (layerDim - 1) / 2
                
                for y = 1 - layerIdx to layerHalfDim do (layerIdx, layerIdx, y)
                for x = layerHalfDim - 1 downto -layerHalfDim do (layerIdx, x, layerIdx)
                for y = layerHalfDim - 1 downto -layerHalfDim do (layerIdx, -layerIdx, y)
                for x = -layerHalfDim + 1 to layerHalfDim do (layerIdx, x, -layerIdx)

                yield! genCoordsForLayer (layerIdx + 1)
            }

        seq {
            yield (0, 0, 0)
            yield! genCoordsForLayer 1
        }
        |> Seq.indexed
        |> Seq.map (fun (cellIdx, (layerIdx, x, y)) ->
            { CellIdx = cellIdx; LayerIdx = layerIdx; X = x; Y = y })
                      
    
    spiralCells
    |> Seq.find (fun { CellIdx = cellIdx } -> cellIdx + 1 = PuzzleInput)
    |> function | { X = x; Y = y } -> Math.Abs(x) + Math.Abs(y)
    |> printfn "Part 1 answer = %i\n"


    spiralCells
    |> Seq.skip 1
    |> Seq.scan (fun prior cell ->
        let newValue =
            prior
            |> Seq.takeWhile (fun (_, cell') ->
                cell'.LayerIdx >= cell.LayerIdx - 1)
            |> Seq.filter (fun (_, cell') ->
                Math.Abs (cell'.X - cell.X) <= 1 && Math.Abs (cell'.Y - cell.Y) <= 1)
            |> Seq.sumBy fst

        (newValue, cell) :: prior) [(1, spiralCells |> Seq.head)]
    |> Seq.pick (function
        | (value, _) :: _ when value > PuzzleInput -> Some value
        | _ -> None)
    |> printfn "Part 2 answer = %i"

    0