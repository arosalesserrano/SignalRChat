"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("http://192.168.0.94:5000/chatHub").build();
var message="";

//Disable send button until connection is established

connection.on("ReceiveMessage", function (user, message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    // segun el tipo de mensaje lo pondremos en la lista messagesList o en la lista logList
    //if(message.tipomensaje)
   // alert(message);
    var encodedMsg = "Timestamp: " + message.timestamp + " Usuario: " + user + " tipo mensaje: " + message.tipomensaje + " Nota " + message.nota;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
        document.getElementById("logList").appendChild(li);
});

connection.start().then(function () {
   /* connection.invoke("SendMessage", "arosalesii", "Inicio").catch(function (err) {
        return console.error(err.toString());
    });*/
 
}).catch(function (err) {
    return console.error(err.toString());
});
