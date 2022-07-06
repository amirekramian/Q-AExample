$(document).ready(function () {
	$.ajax({
		url: "/api/user",
		type: "GET",
		success: function (data) {
			$.each(data.$values, function (index, item) {
				$('#userId').append(`<option value='${item.Id}'>${item.FullName}</option>`);
			});
		}
	});
});

var table = $("#ordersListTable").DataTable({
	responsive: true,
	language: {
		"processing": getResourceValue('Loading'),
		info: getResourceValue('DataTableInfo'),
		infoEmpty: "",
		zeroRecords: getResourceValue('NotFound'),
		emptyTable: getResourceValue('NotFound'),
		infoFiltered: "",
		search: getResourceValue('Name'),
		paginate: {
			first: getResourceValue('First'),
			last: getResourceValue('Last'),
			next: getResourceValue('Next'),
			previous: getResourceValue('Previous')
		}
	},
	paging: true,
	lengthChange: false,
	"order": [],
	"serverSide": true,
	processing: true,
	"ajax": function (d, callback, settings) {
		var url = '/api/order';
		url += `?Page=${(d.start / d.length) + 1}&PageSize=${d.length}`;

		let hasFilter = false;
		let userId = $("#userId").val();
		let state = $("#state").val();
		if (userId) {
			url += `&Filters=userId==${userId}`;
			hasFilter = true;
		}
		if (state) {
			if (hasFilter === false) {
				url += `&Filters=State==${state}`;
				hasFilter = true;
			} else {
				url += `,State@=${state}`;
			}
		}
		if (d.order.length !== 0) {
			url += "&Sorts=";
			d.order.forEach(function (value, index) {
				if (index > 0 && index !== d.order.length - 1) {
					url += ",";
				}
				if (value.dir === "asc")
					url += d.columns[value.column].data;
				else url += `-${d.columns[value.column].data}`;
			});
		}
		$.ajax({
			url: url,
			type: 'GET',
			contentType: "application/json",
			dataType: "json",
			success: function (result) {
				callback({
					draw: d.draw,
					data: result.$values,
					recordsTotal: result.RecordsTotal,
					recordsFiltered: result.RecordsTotal
				});
			}
		});
	},
	searching: false,
	ordering: true,
	autoWidth: false,
	scrollX: true,
	columnDefs: [
		//{ "className": "dt-center", "targets": "_all" },
		{ width: "250px", targets: 0, "data": "Id", visible: true, orderable: false },
		{
			width: "250px",
			"targets": 1,
			"data": "User.FullName",
			"render": function (data, type, row, meta) {
				return '<td><div class="ltr">' + data + '</div></td>';
			},
			orderable: false
		},
		{
			width: "250px",
			"targets": 2,
			"data": "Count",
			"render": function (data, type, row, meta) {
				return `<td><div class="ltr">${data ? data : getResourceValue('Unknown')}</div></td>`;
			},
			orderable: true
		},
		{
			width: "250px",
			"targets": 3,
			"data": "State",
			"render": function (data, type, row, meta) {
				var stateTitle = "";
				switch (data) {
					case 1:
						stateTitle = getResourceValue('Created');
						break;
					case 2:
						stateTitle = getResourceValue('Accepted');
						break;
					case 3:
						stateTitle = getResourceValue('Rejected');
						break;
					default:
						stateTitle = getResourceValue('Unknown');
				}
				return `<td><div class="ltr">${stateTitle}</div></td>`;
			},
			orderable: false
		},
		{
			width: "250px",
			"targets": 4,
			"data": "Product.Title",
			"render": function (data, type, row, meta) {
				return `<td><div class="ltr">${data ? data : getResourceValue('Unknown')}</div></td>`;
			},
			orderable: true
		},
		{
			width: "250px",
			"targets": 5,
			"data": "Id",
			"render": function (data, type, row, meta) {
				return `<td>
						<a class="btn btn-info" onclick="acceptSwalWithConfirm(${data})">تایید</a>
						<a class="btn btn-danger" onclick="rejectSwalWithConfirm(${data})">رد</a>
					</td>`;
			},
			orderable: false
		}
	]
});

function search() {
	table.ajax.reload();
}

const acceptSwalWithConfirm = function (id) {
	Swal.fire({
		icon: 'info',
		title: `سفارش شماره ${id} تایید شود ؟`,
		timerProgressBar: false,
		showConfirmButton: true,
		confirmButtonText: getResourceValue('Confirm'),
		showCancelButton: true,
		cancelButtonText: getResourceValue('Cancel')
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: '/api/order/accept/' + id,
				type: 'PUT',
				success: function (data) {
					if (data) {
						search();
					}
				}
			});
		}
	});
}

const rejectSwalWithConfirm = function (id) {
	Swal.fire({
		icon: 'info',
		title: `سفارش شماره ${id} رد شود ؟`,
		timerProgressBar: false,
		showConfirmButton: true,
		confirmButtonText: getResourceValue('Confirm'),
		showCancelButton: true,
		cancelButtonText: getResourceValue('Cancel')
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: '/api/order/reject/' + id,
				type: 'PUT',
				success: function (data) {
					if (data) {
						search();
					}
				}
			});
		}
	});
}