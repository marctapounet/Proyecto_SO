#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>

MYSQL *conn;
pthread_mutex_t mutex=PTHREAD_MUTEX_INITIALIZER;

typedef struct {
	char nombre [20];
	
}Conectado;

typedef struct {
	int num;
	Conectado conectados[100];
}ListaConectado;

ListaConectado lista;

int DameConectados (ListaConectado *con, char conect[250]){
	char temporal[250];
	int i;
	conect[0]= '\0';
	temporal[0]= '\0';

	for(i=0; i < con->num; i++){
		
		sprintf(temporal, "%s/%s", temporal, con->conectados[i].nombre);
		
	}
	
	sprintf(conect, "%d%s", i, temporal);

	if(strlen(conect)==0)
		return -1; //no hay conectados
	else{

		conect[strlen(conect)-1]= '\0'; //para quitar la última barra '/'
		return 0;

	}
}

void *AtenderCliente(void *socket){

	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;

	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int dur_partida, ret, err;
	char us[15];
	char consulta [200];
	char peticion[512];
	char respuesta[512];
	char conectados[250];

	// Ahora recibimos la petici?n

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
		
	if(codigo!=0){ //0 seria desconexion
		
		if (codigo ==1){ //consulta Rahman
			p=strtok (NULL, "/");
			int puntos = atoi(p);
			
			p=strtok (NULL, "/");
			int edad_min = atoi(p);
			
			p=strtok (NULL, "/");
			int edad_max = atoi(p);
			
			p=strtok (NULL, "/");
			int minutos = atoi(p);
			
			sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO, PARTIDA, PARTICIPACION) WHERE USUARIO.ID = 				PARTICIPACION.ID_U AND PARTICIPACION.PUNTOS = '%d' AND PARTICIPACION.ID_U = USUARIO.ID AND USUARIO.EDAD > '%d' AND 				USUARIO.EDAD < '%d' AND USUARIO.ID = PARTICIPACION.ID_U AND PARTICIPACION.ID_P = PARTIDA.ID AND PARTIDA.DURACION > 				'%d'", puntos, edad_min, edad_max, minutos);

			err=mysql_query (conn, consulta);
			if (err != 0){
				printf("Error al consultar datos de la base %u %s\n",
					   mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			//recogemos el resultado de la consulta
		
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			if( row == NULL)
				strcpy(respuesta, "5/"); //El mensaje empieza por 5/ si no hay nadie
			else 
				strcpy(respuesta, "6"); //El mensaje empieza por 6/ si hay alguien

			while(row != NULL){
					
				sprintf(respuesta,"%s/%s",respuesta, row[0]);
				row = mysql_fetch_row (resultado);
			}
				
			printf ("Respuesta: %s\n", respuesta);

			// Enviamos la respuesta

			write (sock_conn,respuesta, strlen(respuesta));
		
		} else if (codigo ==2)// consulta Marc
				//faltaria hacer en el forms la pantalla con los textos
				//solo haria falta un usuario y un tiempo
			
				/* Dame el ID de las partidas de más de X minutos jugadas por el usuario Y */
		{	
			p = strtok( NULL, "/");
			char us[20];
			strcpy (us, p);
		
			p=strtok (NULL, "/");
			int dur_partida = atoi(p);
			
			sprintf(consulta, "SELECT PARTIDA.ID FROM (USUARIO, PARTIDA, PARTICIPACION) WHERE USUARIO.USERNAME = '%s' AND 				USUARIO.ID = PARTICIPACION.ID_U AND PARTICIPACION.ID_P = PARTIDA.ID AND PARTIDA.DURACION > '%d'", us, dur_partida);
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);

			if( row == NULL)
				strcpy(respuesta, "7/"); //El mensaje empieza por 0/ si no hay nadie
			else
				strcpy(respuesta, "8"); //El mensaje empieza por 1/ si hay alguien
								//seguido de los ID de las partidas

			while(row != NULL){
					
				sprintf(respuesta,"%s/%s",respuesta, row[0]);
				row = mysql_fetch_row (resultado);
			}
				
			 printf ("Respuesta: %s\n", respuesta);

			// Enviamos la respuesta

			write (sock_conn,respuesta, strlen(respuesta));
			
		} else if(codigo ==3) 	//Log In
					//para el login, el cliente nos pasará un usuario y una contraseña
				  	//tenemos que mirar que exista y que la contraseña sea correcta
		{
			p = strtok( NULL, "/");//obtenemos el usuario
			char user[20];
			strcpy (user, p);
		
			p = strtok( NULL, "/");//obtenemos la contraseña
			char pas[20];
			strcpy (pas, p);
			
			sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO) WHERE USUARIO.PASSWORD ='%s'", pas);
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
		
			if(row==NULL) // usuario o contraseña incorrectas, pasar un 7/

				strcpy(respuesta, "9/");

			else if(strcmp(user,row[0])==0){ //comprobar que el usuario que se corresponde con la contraseña es el usuario correcto

				strcpy(respuesta, "10");
				sprintf(respuesta, "%s/%s", respuesta, user);

			}else
				strcpy(respuesta, "9/");
			
			// Enviamos la respuesta

			write (sock_conn,respuesta, strlen(respuesta));
		} else 	if(codigo==4)	//codigo 4, por tanto Registro
				//aqui necesitamos un nombre de usuario y una contraseña (confirmar contraseña)
		{	
			p = strtok( NULL, "/");//obtenemos el usuario
			char user[20];
			strcpy (user, p);
		
			p = strtok( NULL, "/");//obtenemos la contraseña
			char pas[20];
			strcpy (pas, p);
		
			p = strtok( NULL, "/");//obtenemos la contraseña confirmada
			char pas_conf[20];
			strcpy (pas_conf, p);

			p = strtok( NULL, "/");//obtenemos la contraseña confirmada
			int edad = atoi(p);
			
			int id;
			
			//comprobar que no existe dicho usuario 
			
			sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO) WHERE USUARIO.USERNAME ='%s'", user);
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if(row== NULL){ //no existe dicho usuario
			
				//Confirmamos contraseñas
				if (strcmp(pas, pas_conf)!=0){
				
					strcpy(respuesta, "11/"); //las contraseñas no son iguales
			
					//generamos el id para el nuevo usuario
				}else{
					row[0]="Frase aleatoria";

					while(row!=NULL){
						id = rand () % (9999 + 1);   // Este esta entre 1 y 9999

						//comprobar que no existe dicho id
						strcpy(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO) WHERE USUARIO:ID = 'id'");
						resultado = mysql_store_result (conn);
						row = mysql_fetch_row (resultado);
					}

					//Ya tenemos el id (int id), la contraseña(char pas) y el nombre (char user)
					//Ahora hay que registrarlo

			
					strcpy(respuesta, "13");
					sprintf(respuesta, "%s/%s/%d/%s", respuesta, user, id, pas);
				}
					
			
			}else{ //ya existe un usuario con ese nombre

				strcpy(respuesta, "12/");
				sprintf(respuesta, "%s%s", respuesta, user);
			
			}
			
			// Enviamos la respuesta

			write (sock_conn,respuesta, strlen(respuesta));

		}else if (codigo == 5) //codigo 5 nos da la lista de conectados
		{

			int resp;
			
			resp = DameConectados (&lista, conectados);
			

			if(resp == -1){
				strcpy(respuesta, "14/"); //El mensaje empieza por 14/ si no hay nadie
			}else{ 
				strcpy(respuesta, "15/");//El mensaje empieza por 15/ si hay alguien
				sprintf(respuesta, "%s%s", respuesta, conectados);
			}
			// Enviamos la respuesta

			write (sock_conn,respuesta, strlen(respuesta));
		}else if( (codigo ==1)||(codigo==2)||(codigo==3)||(codigo ==4)||(codigo ==5)){
		
			
			pthread_mutex_lock( &mutex ); //no me desconectes ahora
				
			pthread_mutex_unlock( &mutex ); //ya me puedes desconectar
		}
		

	}else //codigo 0;
		   
		// Se acabo el servicio para este cliente (sacar del bucle)
		close(sock_conn); 
}




int main(int argc, char **argv){

	int err;
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
		
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
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
	serv_adr.sin_port = htons(9020);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");

	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");

	int i;
	// bucle infinito
	for (;;){
		printf ("Escuchando\n");
	
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
	
	}
	
	mysql_close (conn);
	exit(0);		
}		
		

