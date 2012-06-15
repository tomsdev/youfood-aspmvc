/// <reference path="../Scripts/jquery-1.6.2.js" />
/// <reference path="../Scripts/jquery-ui-1.8.11.js" />
/// <reference path="../Scripts/signalR.js" />

$(function () {
    'use strict';

    //test load partial view
    $('#partialtest').load("/Home/Result");

    //

    var tmfHub = $.connection.tmfHub;

    function makeItem(item) {
        /// <returns type="jQuery" />
        var el;
        el = $("<div />");
        
        el
            .css({
                width: 200,
                height: 50
            })
            //.addClass("shape")
            //.addClass(shape.Type)
            .attr("id", "el-" + item.ID)
            .data("itemId", item.ID)
            .append("<div class='user-info' id='u-" + item.ID + "'>" +  item.Title + (tmfHub.user.Name === item.ChangedBy.Name ? "" : " changed by " + item.ChangedBy.Name) + "</div>");
        return el;
    }

    tmfHub.itemAdded = function (item) {
        makeItem(item)
            /*.hide()*/
            .appendTo("#items");
            //.fadeIn();
    };

    //$("#user").change(function () {
    //    tmfHub.changeUserName(shapeShare.user.Name, $(this).val(), function () {
    //        //$.cookie("userName", shapeShare.user.Name, { expires: 30 })
    //        $("#user").val(shapeShare.user.Name);
    //    });
    //});

    $('#create-item').submit(function () {
        var title = $('#new-title').val();

        tmfHub.createItem(title)
            .fail(function (e) {
                addMessage(e, 'error');
            });

        $('#new-title').val('');
        $('#new-title').focus();

        return false;
    });

    function load() {
        tmfHub.getItems(function (items) {
            $.each(items, function () {
                tmfHub.itemAdded(this);
            });
        });
    }

    //{ transport: activeTransport }
    $.connection.hub.start({}, function () {
        tmfHub.join(/*$.cookie("userName")*/"", function () {
            //$.cookie("userName", shapeShare.user.Name, { expires: 30 });
            ///$("#user").val(shapeShare.user.Name);
            load();
        });
    });

});