open System
open System.IO
open System.Text

// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type textFileData = {fileName:string; tokens:string[]; count:int }

let readFileContent fileName = 
    let tokens = File.ReadAllText(fileName).Split [|' '|]  
    { fileName = fileName; tokens = tokens; count = tokens.Length }

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
    let wordCounts = Array.map histogram (Array.map (fun t -> t.tokens) texts)
    printfn "%A" wordCounts
//    let foo = Array.map (fun f -> printfn "%A words" (Array.length f ) ) texts
    printfn "Press any key to exit..."
    let exit = Console.ReadKey();
    0 // return an integer exit code


