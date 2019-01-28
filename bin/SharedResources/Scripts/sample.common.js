function showMessageBox(title, message, callback) {
    var newDiv = $(document.createElement("div"));
    $(newDiv).attr("title", title);
    $(newDiv).html(message);
    $(newDiv).dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");

                if (callback) {
                    callback();
                }
            }
        }
    });
}

function confirmationMessageBox(title, message, callback, callbackParam) {
    var newDiv = $(document.createElement("div"));
    $(newDiv).attr("title", title);
    $(newDiv).html(message);
    $(newDiv).dialog({
        modal: true,
        buttons: {
            Yes: function () {
                $(this).dialog("close");
                callback(true, callbackParam);
            },
            No: function () {
                $(this).dialog("close");
                callback(false, callbackParam);
            }
        }
    });
}

function isNull(value, returnValue) {
    if (!value)
        return returnValue;

    return value;
}

function formatJsonDate(value) {
    if (!value) return '';

    var date = new Date(parseInt(value.substr(6)));
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
}

var defaultPageCount = 12;
function applyPager(listcontainer, pageItem, prevPageAction, nextPageAction, bindingCallback) {
    $("ul.pager").remove();

    var paging = '<ul class="pager">'
    + '<input type="hidden" id="prevpagelink" value="' + pageItem.PrevPageLink + '" />'
    + '<input type="hidden" id="nextpagelink" value="' + pageItem.NextPageLink + '" />';
    if (pageItem.PrevPageLink) {
        paging += '<li class="previous"><a href="#" name="lnkPrevPage">&larr; Previous</a></li>';
    }
    else {
        paging += '<li class="previous disabled"><a href="#" name="lnkPrevPage">&larr; Previous</a></li>';
    }

    if (pageItem.NextPageLink) {
        paging += '<li class="next"><a href="#" name="lnkNextPage">Next &rarr;</a></li>';
    }
    else {
        paging += '<li class="next disabled"><a href="#" name="lnkNextPage">Next &rarr;</a></li>';
    }
    paging += '</ul>';

    $(paging).insertBefore(listcontainer);
    $(paging).insertAfter(listcontainer);


    if (bindingCallback)
        bindingCallback(pageItem.Items);

    $("a[name='lnkPrevPage']").click(function (event) {
        if ($("#nextpagelink").val()) {
            $.ajax(
                {
                    type: 'POST',
                    url: prevPageAction,
                    data: { prevPageLink: $("#prevpagelink").val() }
                }).done(function (data) {
                    if (data.ok == false) {
                        showMessageBox(data.message);
                    } else {
                        applyPager(listcontainer, data, prevPageAction, nextPageAction, bindingCallback);
                    }
                });
        }
    });

    $("a[name='lnkNextPage']").click(function (event) {

        if ($("#nextpagelink").val()) {
            $.ajax(
                {
                    type: 'POST',
                    url: nextPageAction, //"@Url.Action("GetNextPage")",
                    data: { nextPageLink: $("#nextpagelink").val() }
                }).done(function (data) {
                    if (data.ok == false) {
                        showMessageBox(data.message);
                    } else {
                        applyPager(listcontainer, data, prevPageAction, nextPageAction, bindingCallback);
                    }
                });
        }
    });

}