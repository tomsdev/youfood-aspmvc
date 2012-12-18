/// <reference path="../Scripts/jquery-1.6.2.js" />
/// <reference path="../Scripts/jquery-ui-1.8.11.js" />
/// <reference path="../Scripts/signalR.js" />

function connectHub(group, elementId) {

    var tmfHub = $.connection.tmfHub;

    function makeOrder(id) {
        /// <returns type="jQuery" />
        var el;
        el = $("<tr />").load("/Orders/GetOrder/" + id);
        return el;
    }

    tmfHub.orderAdded = function (id) {
        makeOrder(id).appendTo("#" + elementId + " > tbody");
    };

    $.connection.hub.start({}, function () {
        tmfHub.join(group, function () {
        });
    });
}