open System
open System.IO
open System.Text

// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

let readFileContent fileName =
    File.ReadAllText(fileName).Split [|' '|]

let histogram =
    Seq.fold (fun acc key ->
          if Map.containsKey key acc
          then Map.add key (acc.[key] + 1) acc
          else Map.add key 1 acc
    ) Map.empty
    >> Seq.sortBy (fun kvp -> -kvp.Value)

[<EntryPoint>]
let main argv = 
    printfn "Processing files in %A..." argv
    let texts = Array.map readFileContent (Directory.GetFiles(argv.[0]))
    let wordCount = histogram texts.[0]
    printfn "%A" wordCount
//    let foo = Array.map (fun f -> printfn "%A words" (Array.length f ) ) texts
    printfn "Press any key to exit..."
    let exit = Console.ReadKey();
    0 // return an integer exit code


