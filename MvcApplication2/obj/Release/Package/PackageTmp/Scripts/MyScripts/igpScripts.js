function setCookie(c_name, value, expiredays) {
	var exdate = new Date();
	exdate.setDate(exdate.getDate() + expiredays);
	document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString()) + ";path=/";
}
function getCookie(c_name) {
	if (document.cookie.length > 0) {
		c_start = document.cookie.indexOf(c_name + "=");
		if (c_start != -1) {
			c_start = c_start + c_name.length + 1;
			c_end = document.cookie.indexOf(";", c_start);
			if (c_end == -1) c_end = document.cookie.length;
			return unescape(document.cookie.substring(c_start, c_end));
		}
	}
	return "";
}
function removeCookie(c_name) {
	setCookie(c_name, 'x', -1);
}
function isNumericKey(ctrl, e) {
	var ret;
	var c = e.which || e.keyCode;
	if ((c >= 48 && c <= 57) || c == 8) {
		ret = true;
	}
	else {
		ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100);
		ret = false;
	}
	return ret;
}
function isAlphaNumericKey(ctrl, e) {
	var ret;
	var c = e.which || e.keyCode;
	if ((c >= 48 && c <= 57) || c == 8 || c == 9 || (c >= 65 && c <= 90) || (c >= 97 && c <= 122) || c == 45 || c == 95 || c == 13) ret = true;
	else {
		ret = false;
		ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; ctrl.style.borderStyle = ''; }, 100);
	}
	//var ret;
	//if (e.keyCode != 0 && (e.keyCode == 8)) ret = true;
	//else ret = ((e.charCode >= 48 && e.charCode <= 57) || (e.charCode >= 65 && e.charCode <= 90) || (e.charCode >= 97 && e.charCode <= 122) || e.charCode == 45 || e.charCode == 95);
	//if (ret == false) { ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100); }
	return ret;
}
function isValidEmployeeIDKey(ctrl, e, siteMode) {
	var ret;
	var c = e.which || e.keyCode;
	if (c == 8 || c == 46 || c == 36 || c == 35 || c == 37 || c == 39) ret = true;
	else if (siteMode == 1 || siteMode == 2) ret = ((c >= 48 && c <= 57) || (c >= 65 && c <= 66) || (c >= 97 && c <= 98));
	else ret = ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122));

	if (ret == false) { ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100); }
	return ret;
}

function isValidiGuardExpressEmployeeIDKey(ctrl, e, siteMode) {
	var ret;
	var c = e.which || e.keyCode;
	if (c == 8) ret = true;
	else if (siteMode == 1 || siteMode == 2) ret = ((c >= 48 && c <= 57) || (c >= 65 && c <= 66) || (c >= 97 && c <= 98));
	else ret = (c == 45 || c == 95 || (c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122));
	if (ret == false) { ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100); }
	return ret;
}

function isValidDeptIDKey(ctrl, e) {
	var ret;
	var c = e.which || e.keyCode;
	if (c == 8 || c == 46 || c == 36 || c == 35 || c == 37 || c == 39) ret = true;
	else ret = ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122));
	if (ret == false) { ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100); }
	return ret;
	//var ret;
	//if (e.keyCode != 0 && (e.keyCode == 8 || e.keyCode == 46 || e.keyCode == 36 || e.keyCode == 35 || e.keyCode == 37 || e.keyCode == 39)) ret = true;
	//else ret = ((e.charCode >= 48 && e.charCode <= 57) || (e.charCode >= 65 && e.charCode <= 90) || (e.charCode >= 97 && e.charCode <= 122));
	//if (ret == false) { ctrl.style.borderColor = 'red'; setTimeout(function () { ctrl.style.borderColor = ''; }, 100); }
	//return ret;
}
function restrictKeypadChar(ctl, val) {
	var c = val.charAt(val.length - 1);
	if ((c < '0') || (c > '9' && c != 'A' && c != 'B' && c != 'a' && c != 'b')) {
		ctl.value = val.substring(0, val.length - 1);
		return false;
	}
	else if (c == 'a' || c == 'b') {
		ctl.value = val.toUpperCase();
	}
	return true;
}
function initLayout() {
	//$('.mainMenu > li').hoverIntent(function () { $(this).find('ul').fadeIn("fast"); }, function () { $(this).find('ul').fadeOut("fast"); });
	//$('#masterDialog').dialog({ autoOpen: true, width: 350, modal: true, beforeClose: function (event, ui) { $('#logonUserName').focus(); } });
	//initColumns();
	initBoxes();
}
function myFocus(focusID) {
	$('input[type=text],input[type=password]').blur(function () { $(this).removeClass("highlight"); }).focus(function () { $(this).addClass("highlight"); });
	if (focusID != 'noFocus') {
		if (focusID != null && focusID != "") {
			if ($("#" + focusID).is("input") == true) $("#" + focusID).focus();
			else $("#" + focusID + " input:text,input:password").first().focus();
		}
		else {
			$("input:text,input:password").first().focus();
		}
	}
}
function initBoxes() {
	$('.boxHide').unbind("click").click(hideBox).each(function () {
		var id = $(this).attr('id');
		if (id != '' && getCookie(id) != '') {
			$(this).next('div').hide();
			$(this).find('img').toggle();
			$(this).prev().addClass('opaque');
		}
	});
}
function hideBox() {
	var id = $(this).attr('id');
	$(this).next('div').toggle('fast');
	$(this).find('img').toggle();
	if ($(this).prev().hasClass('opaque')) {
		$(this).prev().removeClass('opaque');
		if (id != '' && getCookie(id) != '') { removeCookie(id); }
	}
	else {
		$(this).prev().addClass('opaque');
		if (id != '') { setCookie(id, 'x', 365); }
	}
	myFocus();
	return false;
}
function initColumns() {
	var c;
	if ((c = getCookie('bigcol')) != null && c != "") {
		if (c == 'right') {
			$('#smallCol').removeClass().addClass('smallColLeft');
			$('#bigCol').removeClass().addClass('bigColRight');
		}
		else {
			$('#smallCol').removeClass().addClass('smallColRight');
			$('#bigCol').removeClass().addClass('bigColLeft');
		}
	}
}
function toggleColumns() {
	var smallCol = $('#smallCol');
	var bigCol = $('#bigCol');
	if (smallCol.hasClass('smallColLeft')) {
		smallCol.removeClass().addClass('smallColRight');
		bigCol.removeClass().addClass('bigColLeft');
		setCookie('bigcol', 'left', 365);
	}
	else {
		smallCol.removeClass().addClass('smallColLeft');
		bigCol.removeClass().addClass('bigColRight');
		setCookie('bigcol', 'right', 365);
	}
	myFocus();
	return false;
}
function setDefaultButton() {
	$('form').each(function () {
		$(this).unbind('keypress').keypress(function (e) {
			if (e.which == 13) {
				$(this).find('input[type=submit]:visible:enabled:first').click();
				return false;
			}
		});
	});
}
function setEnterKey(elemID) {
	$(document).unbind('keypress').keypress(function (e) {
		if (e.which == 13) {
			$(document).unbind('keypress');
			$('#' + elemID).click();
			return false;
		}
	});
}
function resetEnterKey() {
	$(document).unbind('keypress');
}
function showHeaderMsg(message) {
	if (message != null && message.length != 0) {
		var $headerMsg = $('#headerMsg');
		$headerMsg.find('span').html('<b>' + message + '</b>');
		$headerMsg.show(200);
		setTimeout(function () { $headerMsg.hide('slow') }, 2500);
	}
}
function OnSiteChange(elem) {
	var $this = $(elem);
	setCookie('siteIndex', $this.val(), 365);
	window.location.reload();
}

