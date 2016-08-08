#r "System.Runtime.Serialization.dll"

open System.IO
open System.Runtime.Serialization.Formatters.Binary
open System.Runtime.Serialization.Json

let writeValue outputStream x =
    let formatter = new BinaryFormatter()
    formatter.Serialize(outputStream, box x)

let readValue inputStream =
    let formatter = new BinaryFormatter()
    let res = formatter.Deserialize(inputStream)
    unbox res

let jsonWrite (outputStream: Stream) x =
    let formatter = new DataContractJsonSerializer(typeof<Map<string, string>>)
    formatter.WriteObject(outputStream, box x)

let jsonRead (inputStream: Stream) =
    let formatter = new DataContractJsonSerializer(typeof<Map<string, string>>)
    let res = formatter.ReadObject(inputStream)
    unbox res

let addresses =
    Map.ofList ["Jeff", "123 Main Street, Redmond, WA 98052"
                "Fred", "987 Pine Road, Phila., PA 19116"
                "Mary", "PO Box 112233, Palo Alto, CA 94301"]

printfn "Type of addresses is %A" typeof<Map<string, string>>


let fsOut = new FileStream("Data.dat", FileMode.Create)
jsonWrite fsOut addresses
fsOut.Close()

let fsIn = new FileStream("Data.dat", FileMode.Open)
let res: Map<string, string> = jsonRead fsIn
fsIn.Close()

