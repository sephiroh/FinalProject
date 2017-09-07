var configDefault = {
    data: [],
    async: true,
    type: "GET",
    url: "",
    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
};

var helper = {};

var util = {
    addEvent: function(obj, evtName, func) {
        if (obj.addEventListener) {
            obj.addEventListener(evtName, func, false);

        } else if (obj.attachEvent) {
            obj.attachEvent(evtName, func);
        } else {
            if (this.getAttribute("on" + evtName) !== undefined) {
                obj["on" + evtName] = func;
            } else {
                obj[evtName] = func;
            }
        }
    },
    removeEvent: function(obj, evtName, func) {
        if (obj.removeEventListener) {
            obj.removeEventListener(evtName, func, false);
        } else if (obj.detachEvent) {
            obj.detachEvent(evtName, func);
        } else {
            if (this.getAttribute("on" + evtName) !== undefined) {
                obj["on" + evtName] = null;
            } else {
                obj[evtName] = null;
            }
        }

    },
    getAjaxObject: function () {
        var xhttp = null;
        //XDomainRequest
        if ("XMLHttpRequest" in window) {
            xhttp = new XMLHttpRequest();
        } else {
            // code for IE6, IE5
            xhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        return xhttp;
    }
};

helper.evaluateResponseV2 = function (response) {
    if (response.success) {
        if (response.hasOwnProperty('responseData')) {
            return response.responseData;
        }
        return response;
    } else {
        alert(response.errors[0]);
        return null;
    }
};


helper.evaluateResponse = function (response, toaster) {
    // -- In progress - not yet done
    if (response.data.success) {
        if (response.data.hasOwnProperty('responseData')) {
            return response.data.responseData;
        }
        return response.data;
    } else {
        console.log(response.data.errors[0]);
    }
};


helper.xhrHelper = function  (configuration, callback) {
    var xhr = new XMLHttpRequest();
    var options = $.extend({}, configDefault, configuration);

    xhr.open(options.type, options.url, options.async);
        
    //Send the proper header information along with the request
    xhr.setRequestHeader("Content-type", options.contentType);

    util.addEvent(xhr, "readystatechange", function () {
        var state = xhr.readyState;
        var httpStatus = xhr.status;

        if (state === 4 && httpStatus === 200) {
            callback(xhr.responseText);
        }

    });

    if (options.type.toLowerCase() === 'get') {
        xhr.send(null);
    } else {
        xhr.send(new FormData(options.data));
    }
    
};

