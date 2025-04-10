import * as fs from "@std/fs";
// import * as cli from "@std/cli"
import { promptMultipleSelect } from "@std/cli/unstable-prompt-multiple-select";
import { detectDrives } from "./functions.ts";

const drives: string[] = await detectDrives()
let usersPathExist = false
let origin: string[] | null = []
let destiny: string[] | null = []


while(usersPathExist == false || (origin && origin.length == 0) || (destiny && destiny.length == 0)) {
    while(origin && origin.length == 0){
        origin = promptMultipleSelect("Seleccione un disco de origen", drives, {clear: false})
        if(origin && origin.length == 0){
            console.clear()
            console.log("No ha seleccionado ningun disco de origen")
        }
    }
    console.clear()
    console.log(`Ah seleccionado el disco ${origin[0]} como origen del respaldo`)

    while(destiny && destiny.length == 0){
        destiny = promptMultipleSelect("Seleccione un disco de destino", drives, {clear: false})
        if(destiny && destiny.length == 0){
            console.clear()
            console.log("No ha seleccionado ningun disco de destino")
        }
    }
    console.clear()
    console.log(`Se procedera a copiar del disco ${origin[0]} al disco ${destiny[0]}`)
    usersPathExist = await fs.exists(`${origin[0]}:\\Users`)
    if(usersPathExist == false){
        origin = []
        destiny = []
        console.clear()
        console.log("El directorio de origen no contiene un sistema operativo windows.")
    }else if(destiny && origin && destiny[0] == origin[0]){
        destiny = []
            origin = []
            console.clear()
            console.log("Ah seleccionado el mismo disco de origen como destino")
    }
}

const usersPath = await Array.fromAsync(fs.walk(`${origin[0]}:\\Users`, { includeFiles: false, maxDepth: 1, includeSymlinks: false }))
console.log(usersPath.filter(item => !item.name.includes("Default")))