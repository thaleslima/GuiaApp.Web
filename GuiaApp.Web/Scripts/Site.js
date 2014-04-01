loadMenu();

function loadMenu()
{
    var url = window.location.pathname.toLowerCase();

    if (url.indexOf("user") != -1) {
        $("#item1").addClass("active");
 
    }
    else
        if (url.indexOf("city") != -1) {
            $("#item2").addClass("active");
    
        }
        else
            if (url.indexOf("menu") != -1) {
                $("#item3").addClass("active");

            }
            else
                if (url.indexOf("local") != -1) {
                    $("#item4").addClass("active");
    
                }
}