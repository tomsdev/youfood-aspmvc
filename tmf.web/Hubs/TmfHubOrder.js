/// <reference path="../Scripts/jquery-1.6.2.js" />
/// <reference path="../Scripts/jquery-ui-1.8.11.js" />
/// <reference path="../Scripts/signalR.js" />

$(function () {
    'use strict';

    var tmfHub = $.connection.tmfHub;

    function makeOrder(id) {
        /// <returns type="jQuery" />
        var el;
        el = $("<div />").load("/Orders/GetOrder/" + id);
        
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
        return el.unwrap();
    }

    tmfHub.orderAdded = function (id) {
        makeOrder(id)
            /*.hide()*/
            .appendTo("#orders");
            //.fadeIn();
    };

    $.connection.hub.start({}, function () {
        tmfHub.join("placed", function () {

        });
    });

});