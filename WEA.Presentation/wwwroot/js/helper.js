
var ajax = {
    sendRequest: function (actionUrl, method, data, successCallback, failureCallback, async = true) {
        $.ajax(actionUrl,
            {
                method: method,
                data: data,
                success: function (response) {
                    if (successCallback) successCallback(response);
                },
                error: function (er) {
                    console.log(er);
                    if (failureCallback) failureCallback();
                },
                async: async
            });
    }
};
var openedModalId;
var util = {

    guidGenerate: function () {
        var S4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    },
    showModal: function (actionUrl, method, data, target, modalId) {
        ajax.sendRequest(actionUrl, method, data, function (res) {
            $(target).html(res);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
            $(modalId).modal("show")
            openedModalId = modalId;
        });
    },
    hideModal: function () {
        if (openedModalId) {
            $(openedModalId).modal('hide');
            $(".modal-backdrop").remove();
        }
        
    },
    loading: function (status,target) {
        if (!target) {
            target = "body";
        }
        if (status) {
            $(target).append(`<div class="snippet d-flex align-items-center justify-content-center">
                    <div class="stage">
                        <div class="dot-falling"></div>
                    </div>
                </div>`)
        }
        else {
            if (target) {
                $(target + " .snippet").remove();
            }
            else {
                $(".snippet").remove();
            }
        }
    }
};
