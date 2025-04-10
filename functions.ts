import * as fs from '@std/fs';

export async function detectDrives(): Promise<string[]>{
    const all = [
        "A", "B", "C", "D", "E", "F",
        "G", "H", "I", "J", "K", "L",
        "M", "N", "O", "P", "Q", "R",
        "S", "T", "U", "V", "W", "X",
        "Y", "Z"
    ]
    const filtered = Promise.all(
        all.map(async item => {
        if (await fs.exists(`${item}:\\`)){
            return item;
        }
    }))
    

    return (await filtered).filter(item => item != undefined) ;
}