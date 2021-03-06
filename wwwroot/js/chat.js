﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://192.168.0.94:5000/chatHub").build();
var message="";

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    // segun el tipo de mensaje lo pondremos en la lista messagesList o en la lista logList
    //if(message.tipomensaje)
   // alert(message);
    var inputuser = document.getElementById("userInput").value;
    var encodedMsg = "Timestamp: " + message.timestamp + " Usuario: " + user + " tipo mensaje: " + message.tipomensaje + ". Puntua " + message.nota;
    //document.getElementById("dorsalInput").value = encodedMsg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    if (user == inputuser || user== "difusion_jueces") {
        document.getElementById("messagesList").appendChild(li);

    } else {
        document.getElementById("logList").appendChild(li);
}
});

connection.start().then(function () {
   /* connection.invoke("SendMessage", "arosalesii", "Inicio").catch(function (err) {
        return console.error(err.toString());
    });*/
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var puntuacion = document.getElementById("enteroInput").value + "." + document.getElementById("decimaInput").value;
    var timestamp = Math.floor(Date.now() / 1000);
    var message = timestamp + "," + "PUNTUACION" + "," + user + "," + puntuacion;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});