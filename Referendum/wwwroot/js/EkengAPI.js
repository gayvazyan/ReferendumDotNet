
//==============< Ekeng API>=================

function  authRequestProcess() {
    // Token provided by EKENG
    var token = "66a53b58-8bd9-3cc3-9afe-89105c4f6b82";
    // Server side generated user session ID
    var opaque = "1cr8j1po4ejoer6nqm423jdpn3";

    //var opaque = '1cr8j1po4ejoer6nqm423jdpn3';
    $.ajax({
        type: "POST",
        url: "https://eid.ekeng.am/authorize",
        data: {
            token: token,
            opaque: opaque
        },
        async: false,
        timeout: 6000,
        dataType: 'json',
        success: function (result) {

            //alert(opaque);
            //alert(result.data);
           
            $.ajax({
                type: "GET",
                url: '/Enter',
                data: ({ value: result.data }),
            });

            alert("Դուք հաջողությամբ մուտք գործեցիք համակարգ");

        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert("Please insert your eID card into the reader");
        }
    });

}
