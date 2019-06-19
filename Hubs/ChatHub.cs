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
            await Clients.All.SendAsync("ReceiveMessage", user, "Mensajerecibido");
            string juez ="";
            string dorsal="";
            string puntuacion="";
            string roljuez="";
            string paneljuez="";
            String[] TagIds = message.Split(",");
            juez = TagIds[0];
            puntuacion = TagIds[1];
            dorsal = TagIds[2];
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
                await Clients.All.SendAsync("ReceiveMessage", user, "intentamos acceder a la base de datos");

                // Abre la base de datos
                databaseConnection.Open();
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
                    await Clients.All.SendAsync("ReceiveMessage", user, "Perfe");
                }
                else
                {
                    await Clients.All.SendAsync("ReceiveMessage", user, "Error");
                }
                //ad
               //commandDatabaseintroducirpuntuaciones.ExecuteNonQuery();
             
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                
            }
            
        }
    }
}