$(document).ready(function () {
	initWizard(2);
});

function sendData() {
	let title = $("#title").val();
	let type = $("#type").val();
	let price = $("#price").val();
	
	if (title === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Title'));
		return;
	}

	if (type === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Type'));
		return;
	}

	if (price === "") {
		simpleErrorSwalWithTimer(generateNotEmptyValidationMessage('Price'));
		return;
	}

	let dataToSend = {
		Title: title,
		Type: type,
		Price: price
	}

	$.ajax({
		method: "POST",
		url: `/api/Product`,
		contentType: "application/json",
		dataType: "json",
		data: JSON.stringify(dataToSend),
		success: function (response) {
			if (response.Id !== null) {
				Swal.fire({
					icon: 'success',
					title: `${getResourceValue('Product')} ${getResourceValue('ApiSuccessMessage')}`,
					timer: 2000,
					timerProgressBar: true,
					showConfirmButton: false
				}).then(function () {
					window.location.href = "/Product";
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