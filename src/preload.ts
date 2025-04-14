import { contextBridge } from "electron";
import * as fn from '../functions/fileSystem'

async function detectDrives() {
    return fn.detectDrives()
}

contextBridge.exposeInMainWorld("api",{
    detectDrives: detectDrives
})