//Get the button:
var scrollButton = document.getElementById("scrollButton");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        scrollButton.style.display = "block";
    } else {
        scrollButton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    //document.body.scrollTop = 0; // For Safari
    //document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera

    window.scrollTo({ top: 0, behavior: 'smooth' });
} 

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) {
            end = dc.length;
        }
    }
    // because unescape has been deprecated, replaced with decodeURI
    //return unescape(dc.substring(begin + prefix.length, end));
    return decodeURI(dc.substring(begin + prefix.length, end));
} 

$(document).ready(function () {
	$('.minus').click(function () {
		var $input = $(this).parent().find('input');
		var count = parseInt($input.val()) - 1;
		count = count < 1 ? 1 : count;
		$input.val(count);
		$input.change();
		return false;
	});
	$('.plus').click(function () {
		var $input = $(this).parent().find('input');
		$input.val(parseInt($input.val()) + 1);
		$input.change();
		return false;
	});

	//var myCookie = getCookie("MyCookie");

	if (getCookie("ConsentCookie") == null) {

        $('#modalCookie1').modal('show');
        $('#consentContainer').css('display', 'block');

		var d = new Date();
		d.setTime(d.getTime() + (365 * 24 * 60 * 60 * 1000));
		var expires = "expires=" + d.toUTCString();

		document.cookie = "ConsentCookie=Consented; "+expires;
	}
	
});

function removeConsentContainer() {
    $('#consentContainer').css('display', 'none');
}