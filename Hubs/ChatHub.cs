using Microsoft.AspNetCore.SignalR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
       public async Task SendMessage(string user, string message)
        {
            Models.mensaje mensajjje = new Models.mensaje();
            // Inicialmente se utilizaba como log el propio envío de mensajes
            mensajjje.tipomensaje = "Bienvenida";
            mensajjje.timestamp=(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            mensajjje.nota = message;
            await Clients.All.SendAsync("ReceiveMessage", user, mensajjje);
            
            string connectionString = "datasource=192.168.0.94;port=3306;username=apppruebas;password=Capeluam209173$$_;database=test;";
           
            // Prepara la conexión
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            
           
            //MySqlDataReader reader;
            // A consultar !

            try
            {
                
              //  await Clients.All.SendAsync("ReceiveMessage", user, mensajje);

                // Abre la base de datos
                databaseConnection.Open();
                // esccribir el log
                string queryIntroducirLogMensajes = "INSERT INTO Logmensajes(Mensaje) VALUES('" + " USUARIO: " + user + " , MENSAJE: "+ message + "') ";
                MySqlCommand commandDatabaseintroducirLogMensajes = new MySqlCommand(queryIntroducirLogMensajes, databaseConnection);
                commandDatabaseintroducirLogMensajes.ExecuteNonQuery();
                databaseConnection.Close();
                
             
            }

            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                
            }
            
        }
    }
}