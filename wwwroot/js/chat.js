"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://192.168.0.94:5000/chatHub").build();
var message="";

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    // segun el tipo de mensaje lo pondremos en la lista messagesList o en la lista logList
    //if(message.tipomensaje)
    alert(message);
    var msg = message.nota;
    var encodedMsg = "Timestamp: "+ message.timestamp + "Usuario: " + user + " tipo mensaje: " + message.tipomensaje + ". Puntua " + msg;
    document.getElementById("dorsalInput").value = encodedMsg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    if (message.tipomensaje == "Bienvenida" || message.tipomensaje == "Control") {
        document.getElementById("logList").appendChild(li);

    } else {
        document.getElementById("messagesList").appendChild(li);
}
});

connection.start().then(function () {
    connection.invoke("SendMessage", "arosalesii", "Inicio").catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var puntuacion = document.getElementById("enteroInput").value + "." + document.getElementById("decimaInput").value;
    var dorsal = document.getElementById("dorsalInput").value;
    var message = user + "," + puntuacion + "," + dorsal;
    //message.tipomensaje = "Control";
    //message.nota = "Enviado desde javascript";
    //message.usuario = document.getElementById("userInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});