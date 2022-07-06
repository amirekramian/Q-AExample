﻿$(document).ready(function () {
	initWizard(3);
	$.ajax({
		url: '/api/Role',
		type: 'GET',
		success: function (data) {
			$.each(data.Data.$values, function (index, item) {
				$('#roleId').append(`
					<option value='${item.Id}'>${item.Title}</option>
				`);
			});
		}
	});
});

function sendData() {
	let fullName = $("#fullName").val();
	let username = $("#username").val();
	let phone = $("#phone").val();
	let email = $("#email").val();
	let password = $("#password").val();

	if (fullName === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('FullName'));
		return;
	}

	if (username === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Username'));
		return;
	}

	if (password === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Password'));
		return;
	}

	if (phone === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Phone'));
		return;
	}

	if (email === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Email'));
		return;
	}

	if (fullName.length > 20) {
		simpleErrorSwalWithTimer(generateMaxLengthValidationMessage('FullName', 20));
		return;
	}

	if (phone.length !== 11) {
		simpleErrorSwalWithTimer(generateFixedLengthValidationMessage('Phone', 11));
		return;
	}

	let dataToSend = {
		FullName: fullName,
		Username: username,
		Phone: phone,
		Email: email,
		RoleId: $('#roleId').val(),
		Password: password
	}

	$.ajax({
		method: "POST",
		url: `/api/User/Add`,
		contentType: "application/json",
		dataType: "json",
		data: JSON.stringify(dataToSend),
		success: function (response) {
			if (response.IsSuccess) {
				Swal.fire({
					icon: 'success',
					title: `${getResourceValue('User')} ${getResourceValue('ApiSuccessMessage')}`,
					timer: 2000,
					timerProgressBar: true,
					showConfirmButton: false
				}).then(function () {
					window.location.href = "/Admin/User";
				});
			}
			else {
				simpleErrorSwalWithTimer(response.Message
					? response.Message
					: getResourceValue('ApiResponseError'));
				return;
			}
		},
		error: function (error) {
			simpleErrorSwalWithTimer(error.Message ? error.Message : getResourceValue('ApiResponseError'));
		}
	});
}