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
            await Clients.All.SendAsync("ReceiveMessage", "difusion_jueces", mensajjje);
            
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
                /*
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count>0) // Si el juez existe y está activo, inserta la puntuación
                {
                    // se añaden las puntuaciones a la tabla de puntuaciones
                    commandDatabaseintroducirpuntuaciones.ExecuteNonQuery();
                    // se añaden las puntuaciones a la tabla scores, teniendo en cuenta el rol del juez, por lo que hay que localizarlo en la tabla roles jueces
                
                    ad.SelectCommand = commandConsularRolJuez;
                    DataSet dsjv = new DataSet();
                    ad.Fill(dsjv);
                    if (dsjv.Tables[0].Rows.Count>0)
                    {
                        
                        DataRow row;
                        row = dsjv.Tables[0].Rows[0];
                        roljuez = row[0].ToString();
                        paneljuez= row[1].ToString();
                        // sólo si se localiza se añade la puntuación al score 
                        queryIntroducirpuntuacionesscore = "UPDATE SCORE SET " + roljuez + "='" + puntuacion + "' WHERE panel='" + paneljuez + "' and dorsal='" + dorsal + "' and enablescore='1'";
                        MySqlCommand commandDatabaseintroducirpuntuacionesscore = new MySqlCommand(queryIntroducirpuntuacionesscore, databaseConnection);
                        commandDatabaseintroducirpuntuacionesscore.ExecuteNonQuery();
                    }

                    // aqui hacemos lo que tengamos que hacer 
                    await Clients.All.SendAsync("ReceiveMessage", "arosales2", "Perfe");
                }
                else
                {
                    await Clients.All.SendAsync("ReceiveMessage", "arosales3", "Error");
                }
                //ad
               //commandDatabaseintroducirpuntuaciones.ExecuteNonQuery();
             
                databaseConnection.Close();

    */
            }

            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                
            }
            
        }
    }
}