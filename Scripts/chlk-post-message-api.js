var parentURL = "https://beta.chalkable.com";
var CHLK_MESSENGER = (function($){
    var messenger = {

        closeMe : function(data, rURL){
            this.postAction(data, 'closeMe', rURL);
        },

        postAction : function(data, action, rURL){
            var res = data || {};
            $.extend(res, {action: action, isApp : true});
            this.postMessage(res, null, rURL);
        },

        addMe : function(data, rURL){
            this.postAction(data, 'addMe', rURL);
        },

        addApp : function(rWindow, rURL, data){
            var res = data || {};
            $.extend(res, {action: 'addYourself'});
            this.postMessage(res, rWindow, rURL);
        },

        showPlus : function(data, rURL){
            var res = data || {};
            $.extend(res, {action: 'showPlus', isApp : true});
            this.postMessage(res, null, rURL);
        },

        addCallback : function(callback){
            if (document.addEventListener) {
                window.addEventListener("message", callback, false);
            } else if (document.attachEvent) {
                window.attachEvent("onmessage", callback);
            }
        },

        removeCallback : function(callback){
            if (document.removeEventListener) {
                window.removeEventListener("message", callback, false);
            } else if (document.detachEvent) {
                window.detachEvent("onmessage", callback);
            }
        },

        addYourself : function(fn){
            function callback(e){
                if(e.data.action == 'addYourself'){
                    fn(e.data);
                }
            }
            
            if (document.addEventListener) {
                window.addEventListener("message", callback, false);
            } else if (document.attachEvent) {
                window.attachEvent("onmessage", callback);
            }
        },

        postMessage : function(data, rWindow, rURL){
            (rWindow || window.parent).postMessage(data, rURL || parentURL);
        }
    };

    return messenger;
})(jQuery);

