$(document).ready(function () {
	initWizard(1);
	$.ajax({
		url: '/api/Product',
		type: 'GET',
		success: function (data) {
			$.each(data.$values, function (index, item) {
				$('#productId').append(`
					<option value='${item.Id}'>${item.Title}</option>
				`);
			});
		}
	});
});

function sendData() {
	let productId = $("#productId").val();
	let count = $("#count").val();

	if (count < 0) {
		simpleErrorSwalWithTimer('تعداد نمی تواند منفی باشد');
		return;
	}

	let dataToSend = {
		ProductId: productId,
		Count: count
	}

	$.ajax({
		method: "POST",
		url: `/api/Order`,
		contentType: "application/json",
		dataType: "json",
		data: JSON.stringify(dataToSend),
		success: function (response) {
			if (response.Id !== null) {
				Swal.fire({
					icon: 'success',
					title: `سفارش با موفقیت ثبت شد`,
					timer: 2000,
					timerProgressBar: true,
					showConfirmButton: false
				}).then(function () {
					window.location.href = "/Order/MyOrder";
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