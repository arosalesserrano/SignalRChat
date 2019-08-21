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
            /*mensajjje.tipomensaje = "Bienvenida";
            mensajjje.timestamp=(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            mensajjje.nota = "intentamos acceder a la base de datos";
            await Clients.All.SendAsync("ReceiveMessage", "arosales1", mensajjje);
            */
            string juez ="";
            string dorsal="";
            string puntuacion="";
            string roljuez="";
            string paneljuez="";
                        String[] TagIds = message.Split(",");
            juez = "arosales";// TagIds[0];
            puntuacion = "0.00";// TagIds[1];
            dorsal = "sdsd";// TagIds[2];
            string connectionString = "datasource=192.168.0.94;port=3306;username=apppruebas;password=Capeluam209173$$_;database=test;";
            // Tu consulta en SQL
            //string query = "UPDATE SCORE SET P = 4 WHERE id = 5";
            
            string queryIntroducirpuntuaciones = "INSERT INTO Puntuaciones(Juez, Puntuacion, Dorsal) VALUES('"+juez+"','"+puntuacion+"','"+dorsal+"') ";
            string queryConsularJuezValido = "SELECT * FROM Jueces WHERE ACTIVO=1 AND ID='" + juez+"'";
            string queryConsularRolJuez = "SELECT rol,panel FROM RolesJueces WHERE IDJUEZ='" + juez + "'";
            string queryIntroducirpuntuacionesscore;
            // Prepara la conexión
            MySqlDataAdapter ad = new MySqlDataAdapter();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            
            MySqlCommand commandDatabaseintroducirpuntuaciones = new MySqlCommand(queryIntroducirpuntuaciones, databaseConnection);
            MySqlCommand commandConsularJuezValido = new MySqlCommand(queryConsularJuezValido, databaseConnection);
            MySqlCommand commandConsularRolJuez = new MySqlCommand(queryConsularRolJuez, databaseConnection);
            ad.SelectCommand = commandConsularJuezValido;
            commandDatabaseintroducirpuntuaciones.CommandTimeout = 60;
            commandConsularJuezValido.CommandTimeout = 60;
            //MySqlDataReader reader;

            // A consultar !

            try
            {
                Models.mensaje mensajje = new Models.mensaje();
                mensajje.tipomensaje = "Envio-mensaje";
                mensajje.dorsal = "10";
                mensajje.puntuacion = "8.5";
                mensajje.nota = "Ahora estamos dentro del try";

                await Clients.All.SendAsync("ReceiveMessage", user, mensajje);

                // Abre la base de datos
                databaseConnection.Open();
                // esccribir el log
                string queryIntroducirLogMensajes = "INSERT INTO Logmensajes(Mensaje) VALUES('" + user + ","+ message + "') ";
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