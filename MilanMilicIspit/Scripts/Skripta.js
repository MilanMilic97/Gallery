$(document).ready(function () {


    var host = window.location.host;
    var token = null;
    var headers = {};
    var formAction = "Create"
    var picturesEndPoint = "http://" + host + "/api/pictures";
    var galleriesEndPoint = "http://" + host + "/api/galleries";


    $.getJSON(picturesEndPoint, setpictures);
    $.getJSON(galleriesEndPoint, setgalleries);


    //---------------------------------Login and Registration---------------------------------
    $("#btnRegHead").click(function () {
        $("#regDiv").toggleClass()
        $("#regLogHead").addClass("hidden")
    })

    $("#btnLogHead").click(function () {
        $("#logDiv").toggleClass("hidden")
        $("#regLogHead").addClass("hidden")
    })

    $("#goToRegLog").click(function () {
        $("#regDiv").toggleClass()
        $("#regLogHead").removeClass("hidden")
    })

    $("#goToRegLog2").click(function () {
        $("#logDiv").toggleClass("hidden")
        $("#regLogHead").removeClass("hidden")
    })

    $("#jumpOnLog").click(function () {
        $("#logDiv").toggleClass("hidden")
        $("#regDiv").toggleClass()
        $("#jumpOnLog").addClass("hidden")
    })

    $("#regForm").submit(registration);
    $("#logForm").submit(login);
    $("#btnLogout").click(logout);


    //---------------------------------FUNCTIONALITY---------------------------------
    $("#create").submit(createpictures);
    $("body").on("click", "#btnDelete", deletepictures);    
    $("#searchbtn").click(search);
    $("#clearCRForm").click(function () { $("#create").trigger("reset") });


    //---------------------------------REGISTRATION---------------------------------
    function registration(e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var pass1 = $("#regPass").val();
        var pass2 = $("#regPass2").val();


        var sendData = {
            "Email": email,
            "Password": pass1,
            "ConfirmPassword": pass2
        };
        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info").append("Uspešna registracija na sistem.");
            $("#jumpOnLog").removeClass("hidden");

        }).fail(function (data) {
            alert("Error while trying to register");
        });
    };

    //---------------------------------LOGIN---------------------------------
    function login(e) {
        e.preventDefault();

        var email = $("#logEmail").val();
        var pass = $("#logPass").val();


        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": pass
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#accInfo").empty().append("Logged user: <b>" + data.userName + "</b>");
            token = data.access_token;
            $("#logDiv").toggleClass("hidden")
            $("#logout").removeClass("hidden")
            $("td").removeClass("hidden");
            $("#create").removeClass("hidden");
            $("#search").removeClass("hidden");
            $("#tradition").removeClass("hidden");
            $("#tradData").text("");

        }).fail(function (data) {
            alert("Error while trying to login");
        });
    };

    //---------------------------------LOGOUT---------------------------------
    function logout() {
        token = null;
        headers = {};
        $.getJSON(picturesEndPoint, setpictures);
        $("#logout").addClass("hidden");
        $("#accInfo").empty();
        $("#create").addClass("hidden");
        $("#regLogHead").removeClass("hidden");
        $("#search").addClass("hidden");
        $("#tradition").addClass("hidden");
    };

    //---------------------------------CREATE---------------------------------
    function createpictures(e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var httpAction;
        var Name = $("#crName").val();
        var Year = $("#crYear").val();
        var Price = $("#crPrice").val();
        var Author = $("#crAuthor").val();
        var Gallery = $('#crGallery option:selected').val();

        var url;
        if (formAction === "Create") {
            httpAction = "POST";
            url = 'http://' + host + "/api/pictures/";
            var sendData = {
                "Name": Name,
                "Year": Year,
                "Price": Price,
                "Author": Author,
                "GaleryId": Gallery
            };
        }
        else {
            httpAction = "PUT";
            url = "http://" + host + "/api/pictures/" + editingId.toString();
            sendData = {
                "Id": editingId,
                "Name": Name,
                "Year": Year,
                "RoomNumber": Rooms,
                "EmployeeNumber": Employees,
                "ChainId": Chain
            };
        }
        $.ajax({
            "type": httpAction,
            "url": url,
            "data": sendData,
            "headers": headers
        }).done(function (data) {
            console.log(data);
            $.getJSON(picturesEndPoint, setpictures);
            $("#create").trigger("reset");
        }).fail(function (data) {
            alert("Error while trying to create");
        });
    };

    //---------------------------------DELETE---------------------------------
    function deletepictures() {
        var editId = this.name;

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            "url": "http://" + host + "/api/pictures/" + editId.toString(),
            "type": "DELETE",
            "headers": headers
        })
            .done(function (data, status) {
                var picturesEndPoint = "http://" + host + "/api/pictures";
                $.getJSON(picturesEndPoint, setpictures);
            })
            .fail(function (data, status) {

                alert("Desila se greska!");
            });
    };

    //---------------------------------GET pictures FROM BASE TO TABLE---------------------------------
    function setpictures(data, status) {
        console.log("Status: " + status);

        var $container = $("#data");
        $container.empty();

        if (status === "success") {
            console.log(data);

            var div = $("<div></div>");
            var h1 = $("<h1 style='text-align:center'>Pictures</h1>");
            div.append(h1);

            var table = $("<table style='margin:auto' border='1'></table>");
            var header = $("<tr style='background-color:lightblue'><td style='padding:7px; width:200px'>Name</td><td style = 'width:200px; padding:7px'>Author</td><td style = 'width:100px; padding:7px'>Price</td><td style = 'width:200px; padding:7px'>Gallery</td><td class='hidden' style = 'width:auto; padding:7px'>Year</td><td style='width:auto'class =hidden>Delete</td></tr>");
            table.append(header);

            for (i = 0; i < data.length; i++) {
                var row = "<tr>";
                var displayData = "<td style='padding:7px'>" + data[i].Name + "</td><td style='padding:7px'>" + data[i].Author + "</td>" + "<td style='padding:7px'>" + data[i].Price + "</td>" + "<td  style='padding:7px'>" + data[i].GalleryName + "</td>" + "<td class='hidden' style='padding:7px'>" + data[i].Year + "</td>";
                var stringId = data[i].Id.toString();
                var displayDelete = "<td id=deltd class='hidden' style = 'width:60px'><button class='btn' id=btnDelete name=" + stringId + ">Delete</button></td>";
                //  var displayEdit = "<td id=editTd class='hidden' style = 'width:auto'><button class='btn' id=btnEdit name=" + stringId + ">Edit</button></td>";

                row += displayData + displayDelete + /*displayEdit +*/ "</tr>";
                table.append(row);
            }

            div.append(table);
            $container.append(div);

            if (token) {
                $("td").removeClass("hidden");
            }
        }
        else {
            div = $("<div></div>");
            h1 = $("<h1>Error while trying to get pictures!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };

    //---------------------------------GET galleries FROM BASE TO SELECT---------------------------------
    function setgalleries(data, status) {
        console.log("Status: " + status);

        var $container = $("#crGallery");
        $container.empty();

        if (status === "success") {
            console.log(data);

            for (i = 0; i < data.length; i++) {
                var displayData = "<option value=" + data[i].Id + ">" + data[i].Name + "</option>";

                $container.append(displayData);
            }
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja Autora!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };


    //---------------------------------SEARCH---------------------------------
    function search() {
        var min = $("#searchMin").val();
        var max = $("#searchMax").val();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        var url1 = "http://" + host + "/api/search?min=" + min + "&max=" + max;
        $.ajax({
            "type": "POST",
            "url": url1,
            "headers": headers

        }).done(setpictures)

            .fail(function (data) {
                alert("Error while trying to search");
            });
    };

});

