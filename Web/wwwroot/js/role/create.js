$(document).ready(function () {
	initWizard(1);
});

function sendData() {
	let title = $("#title").val();
	let description = $("#description").val();

	if (title === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Title'));
		return;
	}

	if (description === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Description'));
		return;
	}

	let dataToSend = {
		Title: title,
		Description: description
	}

	$.ajax({
		method: "POST",
		url: `/api/Role/`,
		contentType: "application/json",
		dataType: "json",
		data: JSON.stringify(dataToSend),
		success: function (response) {
			if (response.Id !== null) {
				Swal.fire({
					icon: 'success',
					title: `${getResourceValue('Role')} ${getResourceValue('ApiSuccessMessage')}`,
					timer: 2000,
					timerProgressBar: true,
					showConfirmButton: false
				}).then(function () {
					window.location.href = "/Role";
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