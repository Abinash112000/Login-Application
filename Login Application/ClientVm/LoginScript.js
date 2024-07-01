var ControllerUrl = 'Controller/LoginVmHandler.ashx?method=';

function LoginFunction() {
    var self = this;
    self.Firstname = ko.observable();
    self.Lastname = ko.observable();
    self.Username = ko.observable();
    self.Password = ko.observable();
    self.WelComeUser = ko.observable();
    self.CheckBox = ko.observable(false);
    self.IsChecked = ko.observable(false);  
    self.SignIn = function () {
        var Username = self.Username();
        var Password = self.Password();
        if (Username == "" || Username == null) {
            alert("Please Enter Username");
        }
        else if (Password == "" || Password == null) {
            alert("Please Enter Password");
        }
        else {
                $.ajax({
                    async: false,
                    url: ControllerUrl + "GetAllData",
                    dataType: "json",
                    type: "GET",
                    success: function (data) {
                        var returndata = JSON.parse(data);
                        
                        for (let i of returndata) {
                            
                            if (self.Username() == "admin" && self.Password() == "admin@123") {
                                alert("Welome Admin");
                                window.location.href = "WelcomePage.html";
                                break;
                            }
                            else if (self.Username() == i.Username && self.Password() == i.Password) {
                                if (!i.IsChecked) {
                                    alert("User Is InActive");
                                    break;
                                }
                                alert("Login Successfull");
                                self.Username("");
                                self.Password("");
                                $("#LoginId").hide();
                                $("#Welcome").show();
                                self.WelComeUser("Hey " + i.Firstname + " Welcome To Dsg");
                                //window.location.href = "WelcomeUser.html";
                                break;
                            }
                            else if (self.Username() == i.Username || self.Password() == i.Password) {
                                alert("Login Failed")
                                break;
                            }
                            else {
                                continue;
                            }
                        }
                    },
                    error: function (err) {
                        alert(err.status + "-" + err.statusText);
                    }
                });
            }
    }
    self.NewUser = function () {
        window.location.href = "CreateAccount.html";
    }
    
    self.Checked = function () {
        debugger;
        var x = document.getElementById("Password");
        if (x.type === "password") {
            x.type = "text";
            self.IsChecked(true);
        } else {
            x.type = "password";
            self.IsChecked(false);
        }
    }
}
$(document).ready(function () {
    var SetupViewModel = new LoginFunction();
    ko.cleanNode(document.getElementById("LoginPage"));
    ko.applyBindings(SetupViewModel, document.getElementById("LoginPage"));
});