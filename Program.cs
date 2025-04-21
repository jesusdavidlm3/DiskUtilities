using System;
using System.Configuration.Assemblies;
using System.IO;
using System.Linq;

char[] abc = new char[26];  //Lista de todas las posibles unidades
List<char> drives = [];     //Lista de las unidades detectadas
char origin;                //origen seleccionado
char destiny;               //Destino seleccionado
int listNumber = 1;         //Contador para listas
bool soFound = false;       //Variable que dice si existe un SO en el disco origen

void getDrives(){
    drives = [];
    foreach(char item in abc)   //Detectamos que unidades existen y las guardamos en la lista de unidades detectadas.
    {
        if(Directory.Exists($"{item}:\\"))
        {
            drives.Add(item);
        }
    }
}

for(int i = 0; i < 26;i++)  //Llenamos la lista con todas las letras de posibles unidades
{
    abc[i] = (char)('A'+i);
}
do{             //Este bloque se ejecuta mientras el origen y el destino sean el mismo disco
    getDrives();
    Console.WriteLine("Seleccione una unidad de origen:");
    foreach(char item in drives)    //Imprimimos las opciones
    {
        Console.WriteLine($"{listNumber}. Disco {item}");
        listNumber++ ;
    }
    listNumber = 1;             
    origin = char.ToUpper(char.Parse(Console.ReadLine()));  //Leemos la respuesta del usuario
    soFound = Directory.Exists($"{origin}:\\Users");


    while((!drives.Contains(origin)) || (soFound == false)) //Si la respuesta no es valida repetimos el proceso hasta que lo sea
    {
        Console.Clear();
        if(!drives.Contains(origin))
        {
            Console.WriteLine($"La unidad {origin} no se encuentra en la lista");
        }else if(soFound == false)
        {
            Console.WriteLine("Este disco no contiene un sistema operativo Windows");
        }

        getDrives();
        Console.WriteLine("Intente con una de las siguientes opciones:");
        foreach(char item in drives)
        {
            Console.WriteLine($"{listNumber}. Disco {item}");
            listNumber++;
        }
        listNumber = 1;
        origin = char.ToUpper(char.Parse(Console.ReadLine()));
        soFound = Directory.Exists($"{origin}:\\Users");
    }
    Console.Clear();        

    Console.WriteLine($"Ah seleccionado el disco {origin} como disco de origen");
    Console.WriteLine("Seleccione un disco de destino");

    getDrives();
    foreach(char item in drives)
    {
        Console.WriteLine($"{listNumber}. Disco {item}");
    }
    listNumber = 1;
    destiny = char.ToUpper(char.Parse(Console.ReadLine()));     //Leemos la respuesa de destino del usuario

    while(!drives.Contains(destiny))    //Si el destino no es valido se reasigna hasta que lo sea
    {
        Console.Clear();
        Console.WriteLine($"La unidad {destiny} no se encuentra en la lista");
        Console.WriteLine("Intente con una de las siguientes opciones:");
        getDrives();
        foreach(char item in drives)
        {
            Console.WriteLine($"{listNumber}. Disco {item}");
        }
        listNumber = 1;
        destiny = char.ToUpper(char.Parse(Console.ReadLine()));
    }
    Console.Clear();
    if(destiny == origin)
    {
        Console.WriteLine("Ah Seleccionado el mismo disco como origen y destino");
        Console.WriteLine("Por favor seleccione discos distintos");
    }
}while(destiny == origin);

Console.WriteLine($"Ah seleccionado el disco {origin} como origen y {destiny} como destino");

Console.WriteLine("De un nombre al respaldo");
string backupName = Console.ReadLine();

Console.WriteLine("Escriba las extensiones de archivo que desea excluir separados por espacios");
string[] excludedFiles = Console.ReadLine().Split(' ');

List<string> users = new List<string>(Directory.GetDirectories($"{origin}:\\Users"));   //Obtenemos las capetas de usuario

string[] excludedDirectories = {    //carpetas predeterminadas de windows que no se respaldaran
    $"{origin}:\\Users\\All Users",
    $"{origin}:\\Users\\Default",
    $"{origin}:\\Users\\Public",
    $"{origin}:\\Users\\Default User"
};

users = users.Where(i => !excludedDirectories.Contains(i)).ToList();

string[] includedDirectories = {    //Carpetas que se copiaran desde todos los usuarios
    "Documents",
    "Desktop",
    "Downloads"
};

string destinyDirectory = $"{destiny}:\\Respaldo de {backupName}";  //Carpeta que guaradara el respaldo
Directory.CreateDirectory(destinyDirectory);     //Creamos el direcotio destino del respaldo


foreach(string user in users)
{
    foreach(string directory in includedDirectories)
    {
        foreach(string file in Directory.GetFiles($"{user}\\{directory}"))
        {
            string extension = Path.GetExtension(file);
            if(excludedFiles.Contains(extension))
            {
                Console.WriteLine($"Se copio: {file}");
            }
            else
            {
                //Aqui va la linea que copiara el archivo
                Console.WriteLine($"No se copio: {file}");
            }
        }
    }
}