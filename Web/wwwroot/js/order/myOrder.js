$(document).ready(function () {
	$.ajax({
		url: "/api/order/GetTotalPrice",
		type: "GET",
		success: function (data) {
			$('#totalPrice').append(`${data} هزار تومان`);
			$('#acceptedPrice').append(`${data} هزار تومان`);
			$('#rejectedPrice').append(`${data} هزار تومان`);
			$('#pendedPrice').append(`${data} هزار تومان`);
		}
	});
});

var table = $("#myOrdersListTable").DataTable({
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
		var url = '/api/order/myorder';
		url += `?Page=${(d.start / d.length) + 1}&PageSize=${d.length}`;

		let hasFilter = false;
		let state = $("#state").val();
		if (state) {
			url += `&Filters=State==${state}`;
			hasFilter = true;
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
			"data": "Count",
			"render": function (data, type, row, meta) {
				return `<td><div class="ltr">${data ? data : getResourceValue('Unknown')}</div></td>`;
			},
			orderable: true
		},
		{
			width: "250px",
			"targets": 2,
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
			"targets": 3,
			"data": "Product.Title",
			"render": function (data, type, row, meta) {
				return `<td><div class="ltr">${data ? data : getResourceValue('Unknown')}</div></td>`;
			},
			orderable: true
		},
		{
			width: "250px",
			"targets": 4,
			"data": "Id",
			"render": function (data, type, row, meta) {
				return `<td>
						<a href="/Order/OrderState/${data}" >${getResourceValue('State')}</a>
					</td>`;
			},
			orderable: true
		}
	]
});

function search() {
	table.ajax.reload();
}