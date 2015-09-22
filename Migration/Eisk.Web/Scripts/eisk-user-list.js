$(document).ready(function () {

    //**************************************************************************
    // Defining Data Binding Methods
    //**************************************************************************

    function BindData() {
        User_Service_Proxy.GetUsersByAjax(BindDataOnSuccessCallback, failureCallback);
    }

    var BindDataOnSuccessCallback = function (responseData) {
        // On success, 'data' contains a list of users.
        $.each(responseData, function (key, val) {

            if (val.UserRole == null) val.UserRole = "None, but an authorized user!";

            // Format the text to display.
            var str = val.UserName + ' - Role: ' + val.UserRole;

            // Add a list item for the user.
            $('<li/>', { html: str }).appendTo($('#users'));

        });
    }

    var failureCallback = function (data) {
        alert('Request failure: ' + data.message);
    };

    //**************************************************************************
    // Calling data binding method
    //**************************************************************************

    BindData();

}); //$(document).ready ends

//**************************************************************************
//service proxy for ajax call
//**************************************************************************
var User_Service_Proxy = {
    GetUsersByAjax: function (successCallback, failureCallback) {
        var url = "GetAllUsers/";
        $.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            statusCode: {
                200: function (accounts) {
                    successCallback(accounts);
                },
                404: function () {
                    failureCallback();
                }
            }
        });
    }
}
