var ControllerUrl = 'Controller/AdminSetupController.ashx?method=';

function adminFunc() {
    var self = this;
    self.LoginData = ko.observableArray([]);
    self.StartEdit = ko.observable(false);
    GetAllData = function () {
        $.ajax({
            async: false,
            url: ControllerUrl + "GetAllData",
            dataType: "json",
            type: "GET",
            success: function (result) {
                var Rdata = JSON.parse(result);
                //for (let i of Rdata) {
                //    if (i.Username == "admin" && i.Password == "admin@123") {
                //        self.LoginData(Rdata);
                       
                //    }
                //}
                self.LoginData(Rdata);
            },
            error: function (err) {
                alert(err.status + "-" + err.statusText);
            }
        });
    }
    self.SaveData = function () {
        $.ajax({
            async: false,
            cache: false,
            dataType: "json",
            url: ControllerUrl + "UpdateData",
            type: "POST",
            data: { "SaveData": JSON.stringify(self.LoginData()) },
            success: function (result) {
                if (result != false) {
                    alert("Data Saved Successfully")
                    GetAllData()
                }
                else {
                    alert("Failed")
                }
            },
            error: function (err) {
                alert(err.status + "-" + err.statusText);
            }
        });
    }

    self.YesNoForSpecificPage = function (EmpId) {
        if (self.LoginData().length > 0) {
            for (let i = 0; i < self.LoginData().length ; i++) {
                if (self.LoginData()[i].EmployeeId == EmpId) {
                    if (self.LoginData()[i].IsChecked) {
                        self.LoginData()[i].IsActive = '2';
                    }
                    else {
                        self.LoginData()[i].IsActive = '0';
                    }
                }
            }
            self.SaveData();
            GetAllData();
        }
    }
    self.LogOut = function () {
        self.LoginData('');
        $("#Welocomeadmin").hide();
        window.location.href = "LoginPage.html";

    }
    GetAllData();
}
$(document).ready(function () {
    var adminFunction = new adminFunc();
    ko.cleanNode(document.getElementById("Welocomeadmin"));
    ko.applyBindings(adminFunction, document.getElementById("Welocomeadmin"));

})