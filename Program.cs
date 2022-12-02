using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_IO
{
    internal class Program
    {

        const int MAX = 100;

        class Coche
        {
            public int id;
            public string marca;
            public string modelo;
            public string tipo_vehiculo;
            public int autonomia;
            public decimal precio;
            public int puertas;
            public int [] telefono = new int[MAX];
            public int num_telefonos;
        }
        class Lista_coches
        {
            public int numero; //Ahora mismo nos dice el numero de lineas que tiene el txt
            public int id_max; //Nos dice el numero de id mas alto, la gracia del ID es que sea unico
            public Coche[] coches =new Coche[MAX];
        }

        static int Leer_fichero(Lista_coches lista)
        {
            //DIEGO
            
            StreamReader dato_fichero;
            try
            {
                dato_fichero = new StreamReader("coches.txt");
            }
            catch (FileNotFoundException)
            {
                return -1;
            }

            int i = 0;
            int j; //para recorrer numeros de telefono
            string linea = dato_fichero.ReadLine();
            while (linea != null)
            {
                try
                {
                    Coche c = new Coche();
                    string [] columna = linea.Split(' ');
                    c.id = Convert.ToInt32(columna[0]);
                    c.marca = columna[1];
                    c.modelo = columna[2];//meter telefonos
                    c.tipo_vehiculo = columna[3];
                    c.autonomia = Convert.ToInt32(columna[4]);
                    c.precio = Convert.ToDecimal(columna[5]);
                    c.puertas = Convert.ToInt32(columna[6]);
                    if (columna.Length > 7)
                    {
                        string[] telefonos= columna[7].Split(',');
                        j = 0;
                        while(j < telefonos.Length)
                        {
                            c.telefono[j] = Convert.ToInt32(telefonos[j]);
                            j++;
                        }
                        c.num_telefonos = j;
                    }

                    lista.coches[i] = c;
                    i++;
                    linea = dato_fichero.ReadLine();    
                }
                catch (FormatException)
                {
                    return -2;
                }
            }
            lista.numero = i;
            lista.id_max = lista.coches[i - 1].id;
            //si desordenan los coches, donde los id no estan en orden, hay que hacer una funcion para buscar el id mayor
            dato_fichero.Close();
            return 0;

        }
        static void Main(string[] args)
        {
            //Diego
            //Este sera el menu
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            //SoundPlayer admite audio en .wav, poneis la musica dentro del proyecto bin/debug y ya
            //SoundPlayer player = new SoundPlayer("Just_The_Two_Of_Us.wav");
            //player.PlayLooping();

            Lista_coches autos = new Lista_coches();
            int res =Leer_fichero(autos);
            if (res == -1)
            {
                Console.WriteLine("No se ha encontrado el fichero");
                Console.ReadKey();
                return;
            }
            else if(res == -2)
            {
                Console.WriteLine("Formato incorrecto");
                Console.ReadKey();
                return;
            }
            else
            {
                menu(autos);
                guardar_datos(autos); //al ponerlo abajo nos aseguramos que los nuevos datos se guarden aunque el usuario se haya equivocado
                Console.WriteLine("");
                Console.WriteLine("Adios");
                Console.ReadKey();
            }
        }

        static void menu(Lista_coches autos)
        {
            int opcion = 2;
            while (opcion != 0)
            {
                Console.Clear();//limpiamos la pantalla de la consola de contenido
                Console.WriteLine("Opciones:");
                Console.WriteLine("0: Cerrar y guardar datos");
                Console.WriteLine("1: Mostrar Todo");
                Console.WriteLine("2: Insertar coche");
                Console.WriteLine("3: Eliminar coche");
                Console.WriteLine("4: Modificar atributo");
                Console.WriteLine("5: Buscar modelo");
                Console.WriteLine("6: Insertar telefono");
                Console.WriteLine("7: Eliminar telefono");
                Console.WriteLine("8: Coches entre un rango de precio");
                Console.WriteLine("Elige una opcion");

                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("No has introducido un numero");
                    Console.WriteLine("Pulsa una tecla para volver");
                    Console.ReadKey();
                    menu(autos);
                    break;
                }

                switch (opcion)
                {
                    case 0: //salimos del switch
                        break;
                    case 1:
                        mostrar_todo(autos);
                        break;
                    case 2:
                        add_coche(autos);
                        break;
                    case 3:
                        eliminar_coche(autos);
                        break;
                    case 4:
                        modificar_atributo(autos);
                        break;
                    case 5:
                        busqueda_modelo(autos);
                        break;
                    case 6:
                        add_telefono(autos);
                        break;
                    case 7:
                        eliminar_telefono(autos);
                        break;
                    case 8:
                        mostrar_entre_rango_precios(autos);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("La opcion es incorrecta");
                        Console.WriteLine("Pulsa una tecla para volver");
                        Console.ReadKey();
                        break;

                }
            }
        }

        static void mostrar_todo(Lista_coches autos)
        {
            //alex
            int i = 0;
            int j;
            Console.Clear();
            while (i < autos.numero)
            {
               
                Console.WriteLine("ID: " + autos.coches[i].id);
                Console.WriteLine("Marca: " + autos.coches[i].marca);
                Console.WriteLine("Modelo: " + autos.coches[i].modelo);
                Console.WriteLine("Tipo de vehiculo: " + autos.coches[i].tipo_vehiculo);
                Console.WriteLine("Autonomia: " + autos.coches[i].autonomia);
                Console.WriteLine("Numero de puertas: " + autos.coches[i].puertas);
                Console.WriteLine("Precio: " + autos.coches[i].precio);
                if (autos.coches[i].num_telefonos != 0)
                {
                    j = 0;
                    Console.Write("Telefonos de interesados: ");
                    while (j < autos.coches[i].num_telefonos)
                    {
                        Console.Write(autos.coches[i].telefono[j] + " ");
                        j++;
                    }
                    Console.WriteLine(" ");
                }
                Console.WriteLine("-----------");
                i++;
            }
            Console.WriteLine("Pulsa una tecla para volver");
            Console.WriteLine("");
            Console.ReadKey();
        }

        static void mostrar_entre_rango_precios(Lista_coches autos)
        {
            int i = 0;
            int j;
            decimal minimo;
            decimal maximo;
            Console.Clear();
            Console.WriteLine("Introduce el precio minimo y maximo de busqueda de coche");
            Console.Write("Minimo: ");
            try
            {
                minimo = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            Console.Write("Maximo: ");
            try
            {
                maximo = Convert.ToDecimal(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            while (i < autos.numero)
            {
                if ((autos.coches[i].precio>=minimo) && (autos.coches[i].precio<=maximo))
                {
                    Console.WriteLine("ID: " + autos.coches[i].id);
                    Console.WriteLine("Marca: " + autos.coches[i].marca);
                    Console.WriteLine("Modelo: " + autos.coches[i].modelo);
                    Console.WriteLine("Tipo de vehiculo: " + autos.coches[i].tipo_vehiculo);
                    Console.WriteLine("Autonomia: " + autos.coches[i].autonomia);
                    Console.WriteLine("Numero de puertas: " + autos.coches[i].puertas);
                    Console.WriteLine("Precio: " + autos.coches[i].precio);
                    if (autos.coches[i].num_telefonos != 0)
                    {
                        j = 0;
                        Console.Write("Telefonos de interesados: ");
                        while (j < autos.coches[i].num_telefonos)
                        {
                            Console.Write(autos.coches[i].telefono[j] + " ");
                            j++;
                        }
                        Console.WriteLine(" ");
                    }
                    Console.WriteLine("-----------");
                }
                i++;
            }
            Console.WriteLine("Pulsa una tecla para volver");
            Console.WriteLine("");
            Console.ReadKey();
        }

        static void add_coche(Lista_coches autos)
        {
            //julia

            Console.Clear();
            Console.WriteLine("Introduce los datos");
            try
            {
                Coche c = new Coche();
                //El ID Mejor que se meta automaticamente
                //Console.Write("ID: ");
                //c.id = Convert.ToInt32(Console.ReadLine());
                c.id = (autos.id_max + 1);
                Console.Write("Marca: ");
                c.marca = Console.ReadLine();
                Console.Write("Modelo: ");
                c.modelo = Console.ReadLine();
                Console.Write("Tipo_vehiculo: ");
                c.tipo_vehiculo = Console.ReadLine();
                Console.Write("Autonomia: ");
                c.autonomia = Convert.ToInt32(Console.ReadLine());
                Console.Write("Precio: ");
                c.precio = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Nº puertas: ");
                c.puertas = Convert.ToInt32(Console.ReadLine());
                autos.coches[autos.numero] = c;
                autos.id_max++;
                autos.numero++;
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("");
            Console.WriteLine("Coche insertado con exito");
            Console.WriteLine("Pulsa una tecla para volver");
            Console.WriteLine("");
            Console.ReadKey();


        }

        static void add_telefono(Lista_coches autos)
        {
            Console.Clear();
            int id;
            Console.WriteLine("Introduce el ID del vehiculo que deseas ponerle el n de telefono: ");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            bool encontrado = false;
            int i = 0;
            while ((i < autos.numero) && (encontrado != true))
            {
                if (id == autos.coches[i].id)
                {
                    encontrado = true;
                }
                else
                {
                    i++;
                }

            }
            if (encontrado)
            {
                Console.Write("Introduce el n de telefono: ");
                try
                {
                    autos.coches[i].telefono[autos.coches[i].num_telefonos] = Convert.ToInt32(Console.ReadLine());
                    autos.coches[i].num_telefonos++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("No has introducido un numero");
                    Console.WriteLine("Pulsa una tecla para volver");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Numero de telefono insertado con exito");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("El ID introducido no existe.");
                Console.ReadKey();
            }
        }

        static void eliminar_coche(Lista_coches autos)
        {
            // julia

            Console.Clear();
            int id;
            Console.WriteLine("Introduce el ID del vehiculo que desea eliminar: ");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            bool encontrado = false;
            int i = 0;
            while ((i < autos.numero) && (encontrado != true))
            {
                if (id == autos.coches[i].id)
                {
                    encontrado = true;
                }
                else
                {
                    i++;
                }

            }

            if (encontrado)
            {
                while (i < autos.numero-1)
                {
                    autos.coches[i] = autos.coches[i + 1];
                    i++;
                }
                autos.coches[i] = null;
                autos.numero--;
                Console.WriteLine("Vehiculo eliminado con exito");
            }
            else
            {
                Console.WriteLine("El ID introducido no existe.");
            }

            Console.WriteLine("Pulsa una tecla para volver");
            Console.WriteLine("");
            Console.ReadKey();

        }

        static void eliminar_telefono(Lista_coches autos)
        {
            Console.Clear();
            int id;
            int id_numero;
            Console.WriteLine("Introduce el ID del vehiculo que desea eliminar el n de telefono: ");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            bool encontrado = false;
            int i = 0;
            int j;
            while ((i < autos.numero) && (encontrado != true))
            {
                if (id == autos.coches[i].id)
                {
                    encontrado = true;
                }
                else
                {
                    i++;
                }

            }

            if (encontrado)
            {
                if (autos.coches[i].num_telefonos != 0)
                {
                    j = 0;
                    Console.WriteLine("Selecciona el numero de telefono que desea eliminar");
                    while (j < autos.coches[i].num_telefonos)
                    {
                        Console.WriteLine(j + ": " + autos.coches[i].telefono[j]);
                        j++;
                    }
                    Console.Write("Numero a eliminar: ");
                    try
                    {
                        id_numero = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("No has introducido un numero");
                        Console.WriteLine("Pulsa una tecla para volver");
                        Console.ReadKey();
                        return;
                    }
                    while(id_numero < autos.coches[i].num_telefonos-1)
                    {
                        autos.coches[i].telefono[id_numero] = autos.coches[i].telefono[id_numero + 1];
                        id_numero++;
                    }
                    autos.coches[i].telefono[autos.coches[i].num_telefonos-1] = 0;
                    autos.coches[i].num_telefonos--;
                    Console.WriteLine("Numero de telefono eliminado con exito");

                }
                else
                {
                    Console.WriteLine("No hay numeros de telefonos asociados a este modelo de vehiculo");
                }
            }
            else
            {
                Console.WriteLine("El ID introducido no existe.");
            }

            Console.WriteLine("Pulsa una tecla para volver");
            Console.WriteLine("");
            Console.ReadKey();
        }

        static void modificar_atributo(Lista_coches autos)
        {
            //alex
            Console.Clear();
            int id;
            Console.WriteLine("Introduce el ID del vehiculo que desea modificar: ");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un numero");
                Console.WriteLine("Pulsa una tecla para volver");
                Console.ReadKey();
                return;
            }
            bool encontrado = false;
            int i = 0;
            int j;
            while ((i < autos.numero) && (encontrado != true))
            {
                if (id == autos.coches[i].id)
                {
                    encontrado = true;
                }
                else
                {
                    i++;
                }

            }
            if (encontrado)
            {
                int opcion;
                Console.WriteLine("Que elemento desea modificar?");
                Console.WriteLine("1. Marca                    " + autos.coches[i].marca);
                Console.WriteLine("2. Modelo                   " + autos.coches[i].modelo);
                Console.WriteLine("3. Tipo de vehiculo         " + autos.coches[i].tipo_vehiculo);
                Console.WriteLine("4. Autonomia                " + autos.coches[i].autonomia);
                Console.WriteLine("5. Puertas                  " + autos.coches[i].puertas);
                Console.WriteLine("6. Precio                   " + autos.coches[i].precio);
                j = 0;
                Console.Write("7. Telefonos ");
                while (j < autos.coches[i].num_telefonos)
                {
                    Console.Write(autos.coches[i].telefono[j] + " ");
                    j++;
                }
                Console.WriteLine(" ");
                Console.WriteLine("0. Volver al menu");
                try
                {
                    opcion = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("No has introducido un numero");
                    Console.WriteLine("Pulsa una tecla para volver");
                    Console.ReadKey();
                    return;
                }
                switch (opcion)
                {
                    case 0: //salimos del switch
                        break;
                    case 1:
                        Console.Write("Introduce la nueva marca: ");
                        autos.coches[i].marca = Console.ReadLine();
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Write("Introduce el nuevo modelo: ");
                        autos.coches[i].modelo = Console.ReadLine();
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Write("Introduce el tipo de vehiculo: ");
                        autos.coches[i].tipo_vehiculo = Console.ReadLine();
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Write("Introduce la autonomia del vehiculo: ");
                        try
                        {
                            autos.coches[i].autonomia = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("No has introducido un numero");
                            Console.WriteLine("Pulsa una tecla para volver");
                            Console.ReadKey();
                            return;
                        }
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Write("Introduce el numero de puertas: ");
                        try
                        {
                            autos.coches[i].puertas = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("No has introducido un numero");
                            Console.WriteLine("Pulsa una tecla para volver");
                            Console.ReadKey();
                            return;
                        }
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Write("Introduce el precio del vehiculo: ");
                        try
                        {
                            autos.coches[i].precio = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("No has introducido un numero");
                            Console.WriteLine("Pulsa una tecla para volver");
                            Console.ReadKey();
                            return;
                        }
                        Console.WriteLine("Cambio realizado con exito");
                        Console.ReadKey();
                        break;
                    case 7:
                        modificar_telefono(autos, i);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("La opcion es incorrecta");
                        Console.WriteLine("Pulsa una tecla para volver");
                        Console.ReadKey();
                        break;

                }
            }
            else
            {
                Console.WriteLine("El ID introducido no existe.");
                Console.ReadKey();
            }
        }

        static void modificar_telefono(Lista_coches autos, int i)
        {
            Console.Clear();
            int j;
            int id_numero;
            if (autos.coches[i].num_telefonos != 0)
            {
                j = 0;
                Console.WriteLine("Selecciona el numero de telefono que desea modificar");
                while (j < autos.coches[i].num_telefonos)
                {
                    Console.WriteLine(j + ": " + autos.coches[i].telefono[j]);
                    j++;
                }
                Console.Write("Numero a modificar: ");
                try
                {
                    id_numero = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("No has introducido un numero");
                    Console.WriteLine("Pulsa una tecla para volver");
                    Console.ReadKey();
                    return;
                }
                Console.Write("Introduce el n de telefono rectificado: ");
                try
                {
                    autos.coches[i].telefono[id_numero] = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("No has introducido un numero");
                    Console.WriteLine("Pulsa una tecla para volver");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Numero de telefono cambiado con exito");

            }
            else
            {
                Console.WriteLine("No hay numeros de telefonos asociados a este modelo de vehiculo");
            }
            Console.ReadKey();
        }

        static void busqueda_modelo(Lista_coches autos)
        {
            // arslan
            string modelobuscado;
            bool encontrado = false;
            int i = 0;
            int j;
            Console.Clear();
            Console.Write("Escriba el modelo que desee: ");
            modelobuscado = Console.ReadLine();
            while ((i<autos.numero)&&(encontrado == false))
            {
                if (modelobuscado == autos.coches[i].modelo)
                {
                    encontrado = true;
                }
                else
                {
                    i++;
                }
        
            }
            //encontrado==true
            if (encontrado)
            {
                Console.WriteLine("Los modelos que coinciden con su busqueda son:");
                Console.WriteLine("ID: " + autos.coches[i].id);
                Console.WriteLine("Marca: " + autos.coches[i].marca);
                Console.WriteLine("Modelo: " + autos.coches[i].modelo);
                Console.WriteLine("Tipo de vehiculo: " + autos.coches[i].tipo_vehiculo);
                Console.WriteLine("Autonomia: " + autos.coches[i].autonomia);
                Console.WriteLine("Numero de puertas: " + autos.coches[i].puertas);
                Console.WriteLine("Precio: " + autos.coches[i].precio);
                if (autos.coches[i].num_telefonos != 0)
                {
                    j = 0;
                    Console.Write("Telefonos de interesados: ");
                    while (j < autos.coches[i].num_telefonos)
                    {
                        Console.Write(autos.coches[i].telefono[j] + " ");
                        j++;
                    }
                    Console.WriteLine(" ");
                }
            }
            else
            {
                Console.WriteLine("No se ha encontrado un modelo con esas caracterísicas.");
            }
            Console.WriteLine("");
            Console.WriteLine("Pulsa una tecla para volver");
            Console.ReadKey();
        
        }

        static void guardar_datos(Lista_coches lista)
        {
            //Diego
            //La idea es que al finalizar el programa se guarde en un txt los datos actuales
            StreamWriter res;
            res = new StreamWriter("datos.txt");
            int i = 0;
            int j;
            while (i < lista.numero)
            {
                if (lista.coches[i].num_telefonos != 0)
                {
                    res.Write(
                    lista.coches[i].id + " "
                    + lista.coches[i].marca + " "
                    + lista.coches[i].modelo + " "
                    + lista.coches[i].tipo_vehiculo + " "
                    + lista.coches[i].autonomia + " "
                    + lista.coches[i].precio + " "
                    + lista.coches[i].puertas + " "
                    );
                    j = 0;
                    while (j < lista.coches[i].num_telefonos-1)
                    {
                        res.Write(lista.coches[i].telefono[j] + ",");
                        j++;
                    }
                    res.Write(lista.coches[i].telefono[j]);
                    res.WriteLine("");
                }
                else
                {
                    res.WriteLine(
                    lista.coches[i].id + " "
                    + lista.coches[i].marca + " "
                    + lista.coches[i].modelo + " "
                    + lista.coches[i].tipo_vehiculo + " "
                    + lista.coches[i].autonomia + " "
                    + lista.coches[i].precio + " "
                    + lista.coches[i].puertas
                    );
                }
                i++;
            }
            res.Close();
        }
        
    }
}
