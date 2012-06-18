/// <reference path="../Scripts/jquery-1.6.2.js" />
/// <reference path="../Scripts/jquery-ui-1.8.11.js" />
/// <reference path="../Scripts/signalR.js" />

function connectHub(group, elementId) {

    var tmfHub = $.connection.tmfHub;

    function makeOrder(id) {
        /// <returns type="jQuery" />
        var el;
        el = $("<tr />").load("/Orders/GetOrder/" + id);
        
        //el
        //    .css({
        //        width: 200,
        //        height: 50
        //    })
        //    //.addClass("shape")
        //    //.addClass(shape.Type)
        //    .attr("id", "el-" + item.ID)
        //    .data("itemId", item.ID)
        //    .append("<div class='user-info' id='u-" + item.ID + "'>" +  item.Title + (tmfHub.user.Name === item.ChangedBy.Name ? "" : " changed by " + item.ChangedBy.Name) + "</div>");
        return el;
    }

    tmfHub.orderAdded = function (id) {
        makeOrder(id)
            /*.hide()*/
            .appendTo("#" + elementId + " > tbody");
            //.fadeIn();
    };

    $.connection.hub.start({}, function () {
        tmfHub.join(group, function () {

        });
    });

}