import * as signalR from "@microsoft/signalr";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();

notificationHub = connection.createHubProxy('NotificationHub');

connection.on("ReceiveMessage", function (user, message) {
    console.log('Inside ReceiveMessage');

    notificationHub.invoke('BroadcastTaskResult', message);
});

connection.start().then(function () {
    console.log('Connection start');
}).catch(function (err) {
    return console.log(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.log(err.toString());
//    });
//    event.preventDefault();
//});
