#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
//#include <my_global.h>

MYSQL *conn;

pthread_mutex_t mutex=PTHREAD_MUTEX_INITIALIZER;
int i;
int sockets[100];

//Estructura definida para crear una persona conectada
//con su nombre y su socket
typedef struct {
	int socket;
	char nombre [20];
}Conectado;


//Estructura para crear una lista de conectados
//y el nmero de conectados
typedef struct {
	int num;
	Conectado conectados[100];
}ListaConectado;

char misconectados[200];
ListaConectado listaconect;


//Funcion que recibe nombre y socket como parametro para añadir en la lista de conectados.
int Agregar(ListaConectado *lista, char nombre[20], int socket)
{
	printf ("voy a poner conectado: %d \n", lista->num);
	printf ("el socket es %d\n", socket);
	printf ("el nombre es %s\n", nombre);
	if(lista->num == 100)
		return -1;
	else
	{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket= socket;
		
		lista->num ++;
		return 0;
	}
	
	   
}

//funcion que devuelve la posicion en la lista o -1 si no esta
int DamePosicion (ListaConectado *lista, char nombre[20]){
	
	int i=0, encontrado=0;
	
	while((i<lista->num)&& !encontrado){
		
		if(strcmp(lista->conectados[i].nombre, nombre) ==0)
			
			encontrado =1;
		
		if(!encontrado)
			
			i = i+1;
	}
	if(encontrado)
		
		return i;
	else
		return -1;
	
}

//Funcion implementada para eliminar a un jugador de la lista de conectados.
//Si todo va bien devolvemos un 0, en el caso contrario un -1
int EliminaConectado (ListaConectado *lista, char nombre[20]){ //0 si ok y -1 si no esta
	
	int pos = DamePosicion(lista, nombre);
	
	if(pos==-1)
		return -1;
	else{
		
		int i;
		for (i=pos;i<lista->num-1; i++){
			
			lista->conectados[i].socket = lista->conectados[i+1].socket;
			strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
		}
		lista->num--;
		return 0;
	}	
	
}

//Procedimiento que crea un vector de los jugadores conectados.
//Dentro del vector veremos los nombres de los usuarios conectados
void DameConectados (ListaConectado *lista, char conect[250]){	
		
	//pone en conectados los nombres de las personas conectadas separados por /.
	//primero pone el numero de conectados
			
		sprintf(conect, "%d", lista->num);
		int i;
		for (int i=0; i< lista->num; i++){
			
			sprintf(conect, "%s/%s", conect, lista->conectados[i].nombre);
		}
	}

	
	//Este procedimiento sirve para realizar la invitacion a la partida. A partir de esta funcion
	//vamos a obtener el nombre del jugador a partir de su socket
void obtenerNombrePorSocket(ListaConectado *lista,int socket,char nombre[30])
{	
	printf("Funcion obtener Nombre Por Socket\n");
	printf("Buscando el nombre con el socket %d ...\n",socket);
	int encontrado=0;
	int i=0;
	//Debemos de realizar una bÃºsqueda para encontrar el socket del jugador
	while((i<lista->num)&&(!encontrado))
	{
		printf("Usuario%d: %s Socket:%d\n",i,lista->conectados[i].nombre,lista->conectados[i].socket);
		
		if(lista->conectados[i].socket==socket)
			encontrado=1;
		else
			i++;
		
	}
	//Si no lo encontramos, quiere decir que ese socket no existe
	if(!encontrado)	 
	{
		printf("NO se ha encontrado el usuario por SOCKET\n");
		strcpy(nombre,"No encontrado");
	}
	//Si lo encontramos, devolveremos el nombre del jugador que tiene ese socket
	else
	{
		printf("SI se ha encontrado el usuario por SOCKET\n");
		strcpy(nombre,lista->conectados[i].nombre);
		printf("Nombre encontrado: %s\n",nombre);
	}
	printf("Funcion FINALIZADA: obtenerNombrePorSocket\n");
}

//Funcion para obtener el socket a partir del nombre.
//Es la funcion contraria a obtenerNombrePorSocket.
int obtenerSocketPorNombre(ListaConectado *lista, char nombre[64]) 
{
	
	printf("Funcion activada: obtenerSocketPorNombre\n");
	printf("Buscamos el socket con el nombre %s ...\n",nombre);
	printf ("socket del primero %d\n", lista->conectados[0].socket);
	printf("nombre del primero %s\n", lista->conectados[0].nombre);
	int encontrado=0;
	int i=0;
	while((i<lista->num)&&(!encontrado))
	{
		//printf("Usuario%d: %s Socket:%d\n",i,lista->conectados[i].nombre,lista->conectados[i].socket);
		
		if(strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado=1;
		else
			i++;
	}
	if(!encontrado)	 
	{
		printf("NO se ha encontrado el socket por NOMBRE\n");
		return -1;
	}
	
	else
	{
		printf("SI se ha encontrado el socket por NOMBRE\n");
		printf("Socket encontrado: %d\n",lista->conectados[i].socket);
		return lista->conectados[i].socket;
	}
	
}





int contador;
void *AtenderCliente(void *socket){
	//int err;
	// Estructura especial para almacenar resultados de consultas 
	//Creamos una conexion al servidor MYSQL 
	
	
	int terminar=0;
	
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int dur_partida, ret, err;
	char user[30];
	char consulta [200];
	char peticion[512];
	char respuesta[512];
	
	char conectados[250];
		
	char clear [20];
	// Ahora recibimos la petici?n
	while ( terminar == 0){
		char usuariologeado[20];
		usuariologeado[0]='\0';
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		
		peticion[ret]='\0';
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		
		// Ya tenemos el c?digo de petici?n
		
			
			 if (codigo ==1){ //Login del usuario
				 //para el login, el cliente nos pasarÃ¡ un usuario y una contraseña
				 //tenemos que mirar que exista y que la contraseña sea correcta
				 
				 p = strtok( NULL, "/");//obtenemos el usuario
				 char user[20];
				 strcpy (user, p);
				 
				 p = strtok( NULL, "/");//obtenemos la contraseña
				 char pas[20];
				 strcpy (pas, p);
				 
				 sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO) WHERE USUARIO.PASSWORD ='%s'", pas);
				 err=mysql_query (conn, consulta);
				 if (err != 0){
					 printf("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					 exit(1);
				 }
				 resultado = mysql_store_result (conn);
				 row = mysql_fetch_row (resultado);
				 
				 if(row==NULL) // usuario o contraseÃ±a incorrectas, pasara un 1
					 
					 strcpy(respuesta, "1/");
				 
				 else if(strcmp(user,row[0])==0){ //comprobar que el usuario que se corresponde con la contraseña es el usuario correcto
					 
					 strcpy(respuesta, "2/");
					 					 
					 
					 pthread_mutex_lock( &mutex);
					 int Agre = Agregar(&listaconect,user,sock_conn);
					 pthread_mutex_unlock( &mutex);
					 
					 if(Agre == -1 ){
						 printf("Errror al añadir jugador\n");
						 
						 close(sock_conn);
					 }else{
						 
						 printf("Jugador añadido \n");
						 
						 sprintf(respuesta, "%s%s", respuesta, user);
						 
						 
						 
						 pthread_mutex_lock( &mutex);
						 
						 
						 DameConectados(&listaconect,misconectados);
						 
						 pthread_mutex_unlock( &mutex);
						 
						 strcpy(usuariologeado, misconectados);
						 printf("Lista: %s\n",usuariologeado);
						 char notificacion[20];
						 
						 sprintf(notificacion, "5/%s/",usuariologeado);
						 int j;
						 for(j = 0;j < listaconect.num;j++)
						 {
							 write(sockets[j],notificacion,strlen(notificacion));
						 }
						 
					 }
				 }else
						 strcpy(respuesta, "1/");
				 
				 // Enviamos la respuesta
				 
				 write (sock_conn,respuesta, strlen(respuesta));
				
			}
			
			else if(codigo==2){ // Registro de usuario 
				 
				 
				 
				 //aqui necesitamos un nombre de usuario, una contraseña y la edad. 
				 //comprobamos que no existe un usuario con este nombre y cogemos el max id 
				 // y le asignamos ese id y le añadimos en la base de datos
				 char respuesta [30];
				 p = strtok( NULL, "/");//obtenemos el usuario
				 char user[20];
				 strcpy (user, p);
				 
				 p = strtok( NULL, "/");//obtenemos la contraseña
				 char pas[20];
				 strcpy (pas, p);
				 
				 p = strtok( NULL, "/");//obtenemos la edad
				 int edad = atoi(p);
				 
				 int idd;
				 
				 //comprobar que no existe dicho usuario 
				 
				 sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO) WHERE USUARIO.USERNAME ='%s'", user);
				 err=mysql_query (conn, consulta); 
				 if (err!=0) 
				 {
					 printf ("Error al consultar datos de la base %u %s\n",
							 mysql_errno(conn), mysql_error(conn));
					 exit (1);
				 }
				 
				 resultado = mysql_store_result (conn);
				 row = mysql_fetch_row (resultado);
				 
				 if(row== NULL)
				 { //no existe dicho usuario
					 printf ("no existe dicho usuario\n");
					 
					 
					 sprintf(consulta, "SELECT MAX(USUARIO.ID) FROM USUARIO");
					 err=mysql_query (conn, consulta); 
					 if (err!=0) 
					 {
						 printf ("Error al consultar datos de la base %u %s\n",
								 mysql_errno(conn), mysql_error(conn));
						 exit (1);
					 }
					 
					 
					 resultado = mysql_store_result (conn);
					 row = mysql_fetch_row (resultado);
					 
					 
					 printf("el max id es: %d\n", atoi (row[0]));
					 idd = atoi ( row[0]) + 1 ;
					 
					 
					 sprintf ( consulta, "INSERT INTO USUARIO VALUES(%d,'%s','%s',%d)",idd, user, pas, edad);
					 printf("consulta = %s\n",consulta);
					 
					 err=mysql_query (conn,consulta);
					 if (err!=0){
						 
						 printf ("Error al introducir datos en la base %u %s\n",
								 mysql_errno(conn), mysql_error(conn));
						 sprintf(respuesta, "3/no");
						 
					 }
					 else{
						 
						 sprintf(respuesta, "3/ok");						
						 
					 }
					 
				 }
				 else{
					 
					 sprintf(respuesta, "3/no");					
					 
				 }
				 
				 
				 
				 // Enviamos la respuesta
				 write (sock_conn,respuesta, strlen(respuesta));
				 
				
				
				
			}
			else if(codigo == 3){ //Darse de baja 
				
				//En el caso que nos llegue de codigo un 3, daremos de baja a un usuario, eliminandolo
				//de la base de datos como tambien de la lista de conectados.
				//Necesitaremos una notificación para que el cambio se vea reflejado en cada cliente.
				
				
				p = strtok( NULL, "/");
				char us[20];
				strcpy (us, p);
				
				strcpy(consulta,"DELETE FROM USUARIO WHERE USUARIO.USERNAME = '");
				strcat(consulta,us);
				strcat(consulta,"';");
				printf("consulta = %s\n",consulta);
				
				err=mysql_query (conn,consulta);
				if (err!=0) {
					printf ("Error al introducir datos en la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				
				
				pthread_mutex_lock( &mutex);
				int eliminado = EliminaConectado (&listaconect, us);
				pthread_mutex_unlock( &mutex);
				
				pthread_mutex_lock( &mutex);
				DameConectados(&listaconect, misconectados);
				pthread_mutex_unlock( &mutex);
				
				strcpy(usuariologeado, misconectados);
				printf("Lista: %s\n",usuariologeado);
				char notificacion[20];
				
				sprintf(notificacion, "5/%s/",usuariologeado);
				int j;
				for(j = 0;j < listaconect.num;j++)
				{
					write(sockets[j],notificacion,strlen(notificacion));
				}
				
				
				
				
				printf("El jugador con nombre %s ha sido eliminado.\n", us);
				
				// Enviamos la respuesta
				strcpy(respuesta, "4/ELIMINADO");
				write (sock_conn,respuesta, strlen(respuesta));
				
				
				
				
			}
			else if(codigo == 4){ //invitar
				//En el caso que nos llegue de codigo un 4, el servidor se encargará de realizar el intercambio de informacion
					//necesario para poder establecer la conexion entre los dos usuario. Utilizaremos las funciones de obtener el 
					//socket a partir del nombre y al contrario para poder guardarlos y que al enviar y recibir se pongan de acuerdo
					//para evitar posibles errores y implementar el protocolo de aplicacion correctamente.
				
				char emisor[30]; //invitador
				
				char mensaje[100];
				p = strtok(NULL, "/");
				strcpy(user,p);
				
				pthread_mutex_lock( &mutex);
				int notifi = obtenerSocketPorNombre(&listaconect,user);
				pthread_mutex_unlock( &mutex);
				printf("***********************************\n");
				
				printf (" ¿sale el socket que no deberia salir?  %d" , sock_conn);
				
				printf("***********************************\n");
				pthread_mutex_lock( &mutex);
				obtenerNombrePorSocket(&listaconect,sock_conn,emisor);
				pthread_mutex_unlock( &mutex);
				printf ("pon aqui el emisor %s\n", emisor);
				printf("***********************************\n");
				printf("Emisor: %s\n",emisor);
				printf("Receptor: %s\n",user);
				printf("***********************************\n");
				if(notifi==-1)
				{
					strcpy(mensaje,"6/Este jugador no se encuentra conectado\n");
					write(sock_conn,mensaje,strlen(mensaje));
				}
				else
				{
					
					printf("SocketRes: %d\n",notifi);
					sprintf(mensaje,"6/ok/%s",emisor);
					write(notifi,mensaje,strlen(mensaje));
				}
				
				
				
				}
			else if(codigo==5){ //aceptar invitacion
				//En el caso que nos llegue de codigo un 5, a partir del protocolo de invitación,
				//un usuario acepta la invitacion  o la rechaza, en este caso de codigo = 5 , el usuario habra
				//aceptado la invitacion
				
				char mensaje [20];
				p = strtok(NULL, "/");
				strcpy(user, p); 
				
				//printf("El usuario ha aceptado %s\n",user);
				pthread_mutex_lock( &mutex);				
				int notifi = obtenerSocketPorNombre(&listaconect, user);
				pthread_mutex_unlock( &mutex);
				//printf("aceptada\n");
				printf("Socket: %d\n",notifi);
				strcpy(mensaje,"7/aceptado");
				write(notifi, mensaje, strlen(mensaje));
				
				
				
				
				}
			
			
			
			else if (codigo ==6){//rechazar la invitacion
					//En el caso que nos llegue de codigo un 6, a partir del protocolo de invitación,
					// un usuario ha aceptado la invitacion o la rechaza, en este caso de codigo = 6 , el usuario habrá
					//rechazado.
				
				char mensaje [20];
				strcpy(user,p);
				p= strtok(NULL,"/");
				
				
				printf("El jugador ha rechazado la partida de %s\n",user);
				
				pthread_mutex_lock( &mutex);
				int notifi = obtenerSocketPorNombre(&listaconect, user);
				pthread_mutex_unlock( &mutex);
				
				printf("Partida rechazada\n");
				printf("Socket: %d\n",notifi);
				
				strcpy(mensaje,"8/rechazado");
				
				write(notifi,mensaje,strlen(mensaje));
				
				
			} 
			else if(codigo ==7){ 	//intercambio de mensajes
				//En el caso que nos llegue de codigo un 7, hara referencia al chat 
				//utilizado para que los dos usuarios hablen entre ellos.
				char chat [500];
				
				char mensaje[20];
				
				p = strtok(NULL, "/");
				strcpy(chat,p);
				p= strtok(NULL,"/");
				strcpy(user,p);
				
				printf("CHAT ACTIVO\n");
				printf("Mensaje para %s.",user);
				printf("CONTENIDO: %s\n",chat);
				
				
				pthread_mutex_lock( &mutex);
				int notifi = obtenerSocketPorNombre(&listaconect,user);
				printf (" socket : &d\n", notifi);
				pthread_mutex_unlock( &mutex);
				
				sprintf(mensaje,"9/%s",chat);
				
				write(notifi,mensaje,strlen(mensaje));
			} 
	
			
			else if(codigo==8){ //abandonar el chat
				//En el caso que nos llegue de codigo un 8, querra decir que algun jugador
				//ha abandonado el chat
				char mnj[200];
				char jugador[20];
				p= strtok(NULL,"/");
				strcpy(jugador,p);
				
				
				pthread_mutex_lock( &mutex);
				int SocketRes = obtenerSocketPorNombre(&listaconect,jugador);
				pthread_mutex_unlock( &mutex);
				
				sprintf(mnj,"10/");
				printf("Un usuario se ha desconectado\n");
				write(SocketRes,mnj,strlen(mnj));
				
			}
	else if (codigo == 0){ //desconexion
		//En el caso que nos llegue de codigo un 0, un usuario se desconectará
		//eliminandolo ­ de los conectados.
		char notificacion [30];
		char funcion [30];
		p = strtok(NULL,"/");
		strcpy(user,p);
		pthread_mutex_lock( &mutex);
		int resu=EliminaConectado(&listaconect,user);
		pthread_mutex_unlock( &mutex);
		
		if(resu==0)
		{
			printf("El usuario SI ha sido eliminado\n");
			pthread_mutex_lock( &mutex);
			DameConectados(&listaconect,notificacion);
			printf("Lista %s",notificacion);
			
			sprintf(funcion,"5/%s/",notificacion);
			pthread_mutex_unlock( &mutex);
			
			
			int j = 0;
			for(j = 0;j < listaconect.num;j++)
			{
				write(sockets[j],funcion,strlen(funcion));
			}
			//strcpy(funcion,clear);
			
		}
		else
		{
			printf("El usuario NO ha sido eliminado\n");
		}
		terminar=1;
		 //desconectamos de la lista
		
		}
	}	
	//codigo 0;
	// Se acabo el servicio para este cliente (sacar del bucle)
	close(sock_conn); 
	terminar = 0;
}




int main(int argc, char *argv[]){
	
	listaconect.num=0;
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9640);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	contador=0;
	
	
	//listaconect.num = 0;
	pthread_t thread;
	i=0;
	// bucle infinito
	for (;;){
		
		// Estructura especial para almacenar resultados de consultas 
		//Creamos una conexion al servidor MYSQL
		conn = mysql_init(NULL);
		if (conn==NULL) {
			printf ("Error al crear la conexion: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		//inicializar la conexion
		
		conn = mysql_real_connect (conn, "localhost","root", "mysql", "bd",0, NULL, 0);
		//conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql", "tg14",0, NULL, 0);
		
		if (conn==NULL) {
			printf ("Error al inicializar la conexion: %u %s\n", 
					mysql_errno(conn), mysql_error(conn));
			exit (1);
		}
		
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");		
		sockets[i] =sock_conn;			
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
		
	}
	
	
	
	
	//mysql_close (conn);
	//exit(0);		
}		


