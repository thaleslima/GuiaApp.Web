var tabZoom = {
    //street_address: 15,
    //route: 15,
    //sublocality: 14,
    locality: 16,
    //country: 10
};

var geocoder;
var map;
var marker;

function codeAddress(address) {

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);

            if (tabZoom[results[0].types[0]] != undefined) {
                map.setZoom(tabZoom[results[0].types[0]]);
            }
           
            marker.position = results[0].geometry.location;

            var latitude = results[0].geometry.location.lat();
            var longitude = results[0].geometry.location.lng();
            $("#Latitude").val(latitude);
            $("#Longitude").val(longitude);
        } else {
            //.alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

//function placeMarker(lat, lng) {
//    var lat = parseFloat(lat);
//    var lng = parseFloat(lng);
//    var location = new google.maps.LatLng(lat, lng);
//    map.setCenter(location);
//    var marker = new google.maps.Marker({
//        position: location,
//        map: map,
//        draggable: true,
//        icon: "Imagens/ico_mover.png",
//        title: "Arraste o imóvel para a nova localização"
//    });
//    google.maps.event.addListener(marker, "dragend", function () {
//        var position = marker.getPosition().toUrlValue().split(",");
//        var lat = position[0];
//        var lng = position[1];
//        $("#Latitude").val(position[0]);
//        $("#Longitude").val(position[1]);
//    });
//}


function ValidaBuscaEndereco() {
    codeAddress($("#Address").val());
}

function initialize()
{
    //Sao Paulo
    var latitude = -23.54761;
    var longitude = -46.623822

    initialize2(latitude, longitude, '');
}


function initialize2(lat1, lng1, zoom) {
    geocoder = new google.maps.Geocoder();
    var myLatlng = new google.maps.LatLng(lat1, lng1);

    if (zoom == '')
    {
        zoom = 6;
    }

    var myOptions = {
        zoom: zoom,
        center: myLatlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

    marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        draggable: true,
        // icon: "Imagens/ico_mover.png",
        title: "Arraste para a nova localização"
    });

    google.maps.event.addListener(marker, "dragend", function () {
        var position = marker.getPosition().toUrlValue().split(",");
        var lat = position[0];
        var lng = position[1];
        $("#Latitude").val(position[0]);
        $("#Longitude").val(position[1]);
    });
}


$(function () {

    if ($("#Latitude").val() != "" && $("#Longitude").val() != "") {
        initialize2($("#Latitude").val(), $("#Longitude").val(), 16);
    }
    else
    {
        initialize();
    }

    $("#IdCity").change(function () {
        codeAddress($(this).text());
    });


    $("#file").change(function () {
        var url = window.location.pathname.toLowerCase();
        var url2 = "InsertImage";
        var url3 = "ImagensAux";

        if (url.indexOf("edit") != -1) {
            url2 = "../InsertImage";
            url3 = "../ImagensAux";
        }

        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('file');
        //Iterating through each files selected in fileInput
        for (i = 0; i < fileInput.files.length; i++) {
            //Appending each file to FormData object
            formdata.append(fileInput.files[i].name, fileInput.files[i]);
        }

        //Creating an XMLHttpRequest and sending
        var xhr = new XMLHttpRequest();
        xhr.open('POST', url2);
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.success)
                {
                    console.log(response);

                    $(".image").removeClass("empty");
                    $(".image span").addClass("hide");
                    $("#PathImage").val(response.file);
                    $("#image-local").attr("src", '../' + url3 + '/' + response.file).removeClass("hide");
                    $("#file").val("");
                    

                    //var oImg = document.createElement("img");
                    //oImg.setAttribute('src', );
                    //oImg.setAttribute('alt', 'na');
                    //oImg.setAttribute('width', '275');
                    //$(".image").append(oImg);

                    //var input = document.createElement("input");
                    //input.setAttribute("type", "hidden");
                    //input.setAttribute("name", "PathImage");
                    //input.setAttribute("value", response.file);

                    //$(".image").append(input);

                    
                }
                else
                {
                    alert(response.messagem);
                }
            }
        }
    });

});