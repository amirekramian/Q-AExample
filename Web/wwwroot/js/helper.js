let globalResourceHelperTemp = [];
resources.forEach(function (value, index) {
	globalResourceHelperTemp[value.Name] = value.Value;
});
const globalResourceHelper = globalResourceHelperTemp;

function getResourceValue(key) {
	return resources.find(x => x.Name.toLowerCase() === key.toLowerCase()).Value;
}

function generateNotEmptyValidationMessage(entityKey) {
	var resource = resources.find(x => x.Name.toLowerCase() === 'NotEmptyValidation'.toLowerCase()).Value;
	var entity = resources.find(x => x.Name.toLowerCase() === entityKey.toLowerCase()).Value;
	return resource.replace('@Entity', entity);
}

function generateMaxLengthValidationMessage(entityKey, size) {
	var resource = resources.find(x => x.Name.toLowerCase() === 'MaxLengthValidation'.toLowerCase()).Value;
	var entity = resources.find(x => x.Name.toLowerCase() === entityKey.toLowerCase()).Value;
	return resource.replace('@Entity', entity).replace('@Size', size);
}

function generateFixedLengthValidationMessage(entityKey, size) {
	var resource = resources.find(x => x.Name.toLowerCase() === 'FixedLengthValidation'.toLowerCase()).Value;
	var entity = resources.find(x => x.Name.toLowerCase() === entityKey.toLowerCase()).Value;
	return resource.replace('@Entity', entity).replace('@Size', size);
}

function generateDeleteConfirmMessage(id) {
	var resource = resources.find(x => x.Name.toLowerCase() === 'DeleteConfirmMessage'.toLowerCase()).Value;
	return resource.replace('@Id', id);
}

//Wizard Methods
function nextTab() {
	var $active = $('.wizard .nav-tabs li.active');
	$active.next().removeClass('disabled');
	$active.removeClass('active');
	$active.next().addClass('active');
	$active.next().find('a[data-toggle="tab"]').click();
}

function prevTab() {
	var $active = $('.wizard .nav-tabs li.active');
	$active.removeClass('active');
	$active.prev().addClass('active');

	$active.prev().find('a[data-toggle="tab"]').click();
}

const initWizard = function (steps) {
	$(".wizard .nav-tabs li").width(`${100 / steps}%`);
	$(".connecting-line").width(`${100 - (100 / steps)}%`);

	$('.nav-tabs > li a[title]').tooltip();

	$('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
		var $target = $(e.target);
		if ($target.parent().hasClass('disabled')) {
			return false;
		} else {
			var $active = $('.wizard .nav-tabs li.active');
			$active.removeClass('active');
			$target.parent().addClass('active');
		}
	});
}

//SweetAlert simple messages

const simpleErrorSwalWithTimer = function (message) {
	Swal.fire({
		icon: 'error',
		title: message,
		timer: 2000,
		timerProgressBar: true,
		showConfirmButton: false
	});
}

const simpleSuccessSwalWithTimer = function (message) {
	Swal.fire({
		icon: 'success',
		title: message,
		timer: 2000,
		timerProgressBar: true,
		showConfirmButton: false
	});
}

const deleteSwalWithConfirm = function (id, url) {
	Swal.fire({
		icon: 'info',
		title: generateDeleteConfirmMessage(id),
		timerProgressBar: false,
		showConfirmButton: true,
		confirmButtonText: getResourceValue('Confirm'),
		showCancelButton: true,
		cancelButtonText: getResourceValue('Cancel')
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				url: url + '/' + id,
				type: 'DELETE',
				success: function (data) {
					if (data.IsSuccess) {
						search();
					}
				}
			});
		}
	});
}

//Persian Number to English Number

var
	persianNumbers = [/۰/g, /۱/g, /۲/g, /۳/g, /۴/g, /۵/g, /۶/g, /۷/g, /۸/g, /۹/g],
	arabicNumbers = [/٠/g, /١/g, /٢/g, /٣/g, /٤/g, /٥/g, /٦/g, /٧/g, /٨/g, /٩/g],
	fixNumbers = function (str) {
		if (typeof str === 'string') {
			for (var i = 0; i < 10; i++) {
				str = str.replace(persianNumbers[i], i).replace(arabicNumbers[i], i);
			}
		}
		return str;
	};

//Date Converter
const jalaliToPersian = function (data, showTime) {
	if (data && moment(data).format("YYYY/MM/DD").split("/")[0] != "0001") {
		let output = moment(data).locale("fa").format("YYYY/MM/DD");
		showTime ? output += ` - ${data.split('T')[1].slice(0, -1)}` : null;
		return output;
	}
	return "تاریخ ورودی نامعتبر است";
}

//Datatables
const removeThisRowFromTable = function (e, table) {
	var row = $(event.target).parents('tr');
	if ($(row).hasClass('child')) {
		table.row($(row).prev('tr')).remove().draw();
	} else {
		table.row($(event.target).parents('tr')).remove().draw();
	}
}