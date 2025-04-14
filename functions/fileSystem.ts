import { access, constants } from 'fs/promises'
import fs from 'fs'

async function testRoute(route:string) {
    try{
        await access(route, constants.F_OK);
        return true
    }catch{
        return false
    }
}

export const detectDrives = async() => {
    const abc: string[] = []
    // const drives: string[] = []

    for(let i = 65; i <= 90; i++){
        abc.push(String.fromCharCode(i))
    }

    const drives = await Promise.all(
        abc.map(async item => {
            const exist = await testRoute(`${item}:\\`)
            if(exist){
                return item
            }else{
                return null
            }
        })
    )

    return drives.filter(item => item != null)
}