import React, { useEffect, useState } from 'react'
import { Button, Select, message } from 'antd';

declare global {
    interface Window {
        api? : any
    }
}

interface selectOptions{
    value: string,
    label: string
}

const App: React.FC = () => {

    const [disable, setDisable] = useState<boolean>(true)
    const [started, setStarted] = useState<boolean>(false)
    const [messageApi, contextHolder] = message.useMessage()

    const [aviableDrives, setAviableDrives] = useState<selectOptions[]>([])
    const [origin, setOrigin] = useState<string>()
    const [destiny, setDestiny] = useState<string>()

    useEffect(() => {
        if(started){
            if(origin == destiny){
                setDisable(true)
                messageApi.open({
                    type: 'error',
                    content: 'El destino no puede ser igual al origen',
                    duration: 10
                })
            }else{
                setDisable(false)
            }
        }      
    }, [origin, destiny])

    useEffect(() => {
        getAviableDrives()
    }, [])

    const getAviableDrives = async() => {
        const res = await window.api.detectDrives()
        const treated: selectOptions[] = res.map((item: string) => ({value: item, label: `Disco ${item}:\\`}))
        setAviableDrives(treated)
    }

    return(
        <div className="App">
            {contextHolder}
            <h1>Backup Maker</h1>
            <div className='SelectionArea'>
                <h5>Copiar de: </h5>
                <Select
                    placeholder="Origen"
                    className='Selector'
                    options={aviableDrives}
                    onChange={(e) => {setOrigin(e); setStarted(true)}}
                />
                <h5>a: </h5>
                <Select
                    placeholder="Destino"
                    className='Selector'
                    options={aviableDrives}
                    onChange={(e) => {setDestiny(e); setStarted(true)}}
                />
            </div>

            <Button variant='solid' color='blue' disabled={disable}>Empezar respaldo</Button>
        </div>
    )
}

export default App;