#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>

int main(int argc, char **argv)
{
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int dur_partida;
	char us[15];
	char consulta [200];
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
	
	int sock_conn, sock_listen, ret;
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
			
			sprintf(consulta, "SELECT USUARIO.USERNAME FROM (USUARIO, PARTIDA, PARTICIPACION) WHERE USUARIO.ID = PARTICIPACION.ID_U AND PARTICIPACION.PUNTOS = '%d' AND PARTICIPACION.ID_U = USUARIO.ID AND USUARIO.EDAD > '%d' AND USUARIO.EDAD < '%d' AND USUARIO.ID = PARTICIPACION.ID_U AND PARTICIPACION.ID_P = PARTIDA.ID AND PARTIDA.DURACION > '%d'", puntos, edad_min, edad_max, minutos);
		}
		else if (codigo ==2)// consulta Marc
			//faltaria hacer en el forms la pantalla con los textos
			//solo haria falta un usuario y un tiempo
			
			/* Dame el ID de las partidas de más de X minutos jugadas por el usuario Y */
		{	
			p = strtok( NULL, "/");
			char us[20];
			strcpy (us, p);
			
			p=strtok (NULL, "/");
			int dur_partida = atoi(p);
			
			sprintf(consulta, "SELECT PARTIDA.ID FROM (USUARIO, PARTIDA, PARTICIPACION) WHERE USUARIO.USERNAME = '%s' AND USUARIO.ID = PARTICIPACION.ID_U AND PARTICIPACION.ID_P = PARTIDA.ID AND PARTIDA.DURACION > '%d'", us, dur_partida);
			
			
		}
		else if(codigo ==3) //Log In
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
			
		}
		else //codigo 4, por tanto Registro
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
		
			///////////////////////////////////////////////////
			//comprobar que no existe dicho usuario (otra consulta?)
			///////////////////////////////////////////////////
			
			
			//Confirmamos contraseñas
			if (strcmp(pas, pas_conf)!=0)
				
				sprintf(respuesta, "Las contraseñas no coinciden\n");
			
			//generamos el id para el nuevo usuario
			int id = rand () % (9999 + 1);   // Este estÃ¡ entre 1 y 9999
			
			/////////////////////////////////////////////////////
			//como hacemos el INSERT desde el servidor?????
			/////////////////////////////////////////////////////
		}
	}
			
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
			printf("No se han obtenido datos en la consulta\n");
		else
			while (row != NULL){
				
				//COMO CONVERTIMOS
				//EL RESULTADO DE LA consulta
				//A STRING
				
				
				
			
			printf ("Respuesta: %s\n", respuesta);
			// Enviamos la respuesta
			write (sock_conn,respuesta, strlen(respuesta));
			
			// Se acabo el servicio para este cliente
			close(sock_conn); 
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
			
	}
		
		mysql_close (conn);
		exit(0);
		
}
