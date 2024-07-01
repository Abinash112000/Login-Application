var ControllerUrl = 'Controller/LoginVmHandler.ashx?method=';
function NewUser() {
    self.Firstname = ko.observable();
    self.Lastname = ko.observable();
    self.Username = ko.observable();
    self.Password = ko.observable();
    self.IsChecked = ko.observable(false);

    self.CreateAcc = function () {
        debugger;

        var Firstname = self.Firstname();
        var Lastname = self.Lastname();
        var Username = self.Username();
        var Password = self.Password();

        if (Firstname == "" || Firstname == null) {
            alert("Fill The First Name")
        }
        else if (Lastname == "" || Lastname == null) {
            alert("Fill The Last Name")
        }
        else if (Username == "" || Username == null) {
            alert("Create an Username")
        }
        else if (Password == "" || Password == null) {
            alert("Please give a Password for your Account.")
        }
        else {
            $.ajax({
                async: false,
                cache: false,
                url: ControllerUrl + "CreateAcc",
                dataType: "json",
                type: "GET",
                data: { Firstname: Firstname, Lastname: Lastname, Username: Username, Password: Password },
                success: function (result) {
                    if (result != false) {
                        alert("Account Created Successfully");
                        self.Firstname("");
                        self.Lastname("");
                        self.Username("");
                        self.Password("");
                    }

                },
                error: function (err) {
                    alert(err.status + "" + err.statusText);
                }

            });

        }
    }
    self.Checked = function () {
        var y = document.getElementById("Password");
        if (y.type === "password") {
            y.type = "text";
            self.IsChecked(true);
        }
        else {
            y.type = "password";
            self.IsChecked(false);
        }
    }
}
$(document).ready(function () {
    var CreateAcc = new NewUser();
    ko.cleanNode(document.getElementById("CreateAcc"));
    ko.applyBindings(CreateAcc, document.getElementById("CreateAcc"));
});