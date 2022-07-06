"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/inquiryhub").build();
var digit1, digit2, digit3, digit4, digit5, tenhour, hour, tenmin, min, tensec, sec, etenhour, ehour, etenmin, emin, etensec, esec;
var digit1value = 0;
var digit2value = 0;
var digit3value = 0;
var digit4value = 0;
var digit5value = 0;
var clock = {
	tenhourvalue: 0,
	hourvalue: 0,
	tenminvalue: 0,
	minvalue: 0,
	tensecvalue: 0,
	secvalue: 0,
}
connection.on("UpdateData", function (data) {
	//console.dir(data);
	renderPagea(data);
});

connection.start().then(function () {
}).catch(function (err) {
	return console.error(err.toString());
});

var map, groupLayer, $datatable, mapInterval, recordSelected;
var flyflag = true;
var initializeMap = function () {
	clearLayers();
	map = new L.map('inquiryMap', {
		minZoom: 2, preferCanvas: true, maxBounds: [
			[44.828257, 43.594187],
			[15.232563, 64.438063],
		],
	}).setView([32.649931, 51.691835], 5);
	L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
		attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
	}).addTo(map);
	groupLayer = L.markerClusterGroup({
		iconCreateFunction: function (cluster) {
			var children = cluster.getAllChildMarkers();
			var sum = 0;
			for (var i = 0; i < children.length; i++) {
				sum += children[i].options.cnt;
			}
			let c = '';

			if (sum < 10) {
				c += 'small';
			}
			else if (sum < 100) {
				c += 'medium';
			}
			else {
				c += 'large';
			}

			return new L.DivIcon({
				html: '<div class="marker-cluster"><span>' + sum + '</span></div>',
				className: 'marker-cluster marker-cluster-' + c, iconSize: new L.Point(40, 40)
			});
		},
		showCoverageOnHover: true
	});
	map.addLayer(groupLayer);
	var legend = L.control({ position: "topleft" });

	legend.onAdd = function (map) {
		this._div = L.DomUtil.create('div', 'liveMapLegend');
		this.update();
		return this._div;
	};

	legend.update = function (props) {
		this._div.innerHTML = `
                <div class="controlsWrapper">
                    <div class="text-center">
                        <span>کنترل حالت پرواز</span>
                    </div>
                    <div style="margin-right:4px">
                        <a class="controlBtn stopMapBtn" onclick="stopMap()" href="JavaScript:void(0);"><span class="fa fa-pause"></span></a>
                        <a class="controlBtn playMapBtn" onclick="playMap()" href="JavaScript:void(0);"><span class="fa fa-play"></span></a>
                    </div>
                 </div>
                `;
	};

	legend.addTo(map);
};
var clearLayers = function () {
	if (groupLayer) {
		groupLayer.clearLayers();
	}
};
var stopMap = function () {
	$('.liveMapLegend').addClass('stopped');
	flyflag = false;
	map.setView([32.649931, 51.691835], 5);

};
var playMap = function () {
	$('.liveMapLegend').removeClass('stopped');
	flyflag = true;
}
var vaccinIcon = L.icon({
	iconUrl: '/images/5.png',
	shadowUrl: '/lib/leaflet/images/marker-shadow.png',
	iconSize: [10, 50],
	shadowSize: [50, 64],
	iconAnchor: [10, 20],
	shadowAnchor: [20, 40],
	popupAnchor: [-3, 0]
});
var markerList = [];
var addMarker = function (data) {
	var content = '<div class="text-right">'
		+ '<span>تاریخ و زمان تزریق: ' + (data.date ? data.date : 'نامشخص') + ' ' + (data.time ? data.time : 'نامشخص') + '</span><br/>'
		+ '<span>نام دانشگاه: ' + (data.universityName ? data.universityName : 'نامشخص') + '</span><br/>'
		+ '<span>مرکز تزریق: ' + (data.pharmacyName ? data.pharmacyName : 'نامشخص') + '</span><br/>'
		+ '<span>استان: ' + (data.province ? data.province : 'نامشخص') + '</span><br/>'
		+ '<span>شهرستان: ' + (data.county ? data.county : 'نامشخص') + '</span><br/>'
		+ '<span>شهر: ' + (data.city ? data.city : 'نامشخص') + '</span><br/>'
		+ '<span>اسم فرآورده: ' + (data.brand ? data.brand : 'نامشخص') + '</span><br/>'
		+ '<span>نوبت تزریق: ' + (data.injectionTime == 1 ? 'اول' : data.injectionTime == 2 ? 'دوم' : 'نامشخص') + '</span><br/>'
		+ '<span>کد ملی : ' + (data.patientNationalCode ? data.patientNationalCode : 'نامشخص') + '</span><br/>'
		+ '</div>';

	var markerTemp = L.marker([data.latitude, data.longitude], {
		draggable: false,
		zoom: 5,
		cnt: 1,
		icon: vaccinIcon,
	}).bindPopup(content).addTo(groupLayer);
	//if (map.hasLayer(markerTemp)) {
	//	markerTemp.bounce(3);
	//} else {
	let t = L.marker([data.latitude, data.longitude], {
		draggable: false,
		zoom: 5,
		cnt: 1,
		icon: L.icon({
			iconUrl: '/images/5.png',
			shadowUrl: '/lib/leaflet/images/marker-shadow.png',
			iconSize: [10, 50],
			shadowSize: [50, 64],
			iconAnchor: [0, 45],
			shadowAnchor: [10, 60],
			popupAnchor: [-3, 0]
		}),
	}).addTo(map)//.bounce(3);
	setTimeout(function () {
		map.removeLayer(t);
	}, 2400);
	//}
	markerList.push(markerTemp);
	if (flyflag) {
		map.flyTo([data.latitude, data.longitude], 16);
	}
}
function setTime(isInit) {
	var t = new Date();
	let h = t.getHours();
	h = String(h);
	if (h.length == 1) h = '0' + h;

	let m = t.getMinutes();
	m = String(m);
	if (m.length == 1) m = '0' + m;

	let s = t.getSeconds();
	s = String(s);
	if (s.length == 1) s = '0' + s;

	if (isInit) {
		clock.tenhourvalue = parseInt(h.substr(0, 1));
		clock.hourvalue = parseInt(h.substr(1, 1));
		tenhour.flip(clock.tenhourvalue);
		hour.flip(clock.hourvalue);

		clock.tenminvalue = parseInt(m.substr(0, 1));
		clock.minvalue = parseInt(m.substr(1, 1));
		tenmin.flip(clock.tenminvalue);
		min.flip(clock.minvalue);

		clock.tensecvalue = parseInt(s.substr(0, 1));
		clock.secvalue = parseInt(s.substr(1, 1));
		tensec.flip(clock.tensecvalue);
		sec.flip(clock.secvalue);

	} else {
		if (parseInt(h.substr(0, 1)) != clock.tenhourvalue) {
			tenhour.setNumber(clock.tenhourvalue);
			tenhour.flip(parseInt(h.substr(0, 1)) - clock.tenhourvalue);
			clock.tenhourvalue = parseInt(h.substr(0, 1));
		}
		if (parseInt(h.substr(1, 1)) - clock.hourvalue) {
			hour.setNumber(clock.hourvalue);
			hour.flip(1);
			clock.hourvalue = parseInt(h.substr(1, 1));
		}
		if (parseInt(m.substr(0, 1)) != clock.tenminvalue) {
			tenmin.setNumber(clock.tenminvalue);
			tenmin.flip(parseInt(m.substr(0, 1)) - clock.tenminvalue);
			clock.tenminvalue = parseInt(m.substr(0, 1));
		}
		if (parseInt(m.substr(1, 1)) - clock.minvalue) {
			min.setNumber(clock.minvalue);
			min.flip(1);
			clock.minvalue = parseInt(m.substr(1, 1));
		}
		if (parseInt(s.substr(0, 1)) != clock.tensecvalue) {
			tensec.setNumber(clock.tensecvalue);
			tensec.flip(parseInt(s.substr(0, 1)) - clock.tensecvalue);
			clock.tensecvalue = parseInt(s.substr(0, 1));
		}
		sec.setNumber(clock.secvalue);
		sec.flip(1);
		clock.secvalue = parseInt(s.substr(1, 1));
	}
}
var init = function () {
	initializeMap();
	//var m = moment(new Date(), 'YYYY/M/D HH:mm:ss').add(-5, 'minutes');
	//let time = m.format('HH:mm:ss');

	//etenhour = new FlipCounter($('#etenhour'));
	//ehour = new FlipCounter($('#ehour'));
	//etenmin = new FlipCounter($('#etenmin'));
	//emin = new FlipCounter($('#emin'));
	//etensec = new FlipCounter($('#etensec'));
	//esec = new FlipCounter($('#esec'));

	//etenhour.flip(parseInt(time.substr(0, 1)));
	//ehour.flip(parseInt(time.substr(1, 1)));

	//etenmin.flip(parseInt(time.substr(3, 1)));
	//emin.flip(parseInt(time.substr(4, 1)));

	//etensec.flip(parseInt(time.substr(6, 1)));
	//esec.flip(parseInt(time.substr(7, 1)));

	//$datatable = $('#covidVaccinationLiveTable').DataTable({
	//	"bLengthChange": false,
	//	scrollX: true,
	//	scrollCollapse: true,
	//	autoWidth: true,
	//	paging: true,
	//	columnDefs: [
	//		{ orderable: false, "targets": [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12] },
	//		{
	//			"targets": [0],
	//			"visible": false,
	//			"searchable": false
	//		},
	//	],
	//	"language": {
	//		"url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Persian.json"
	//	},
	//	'order': [[0, 'desc']],
	//	"searching": false,
	//});
	//digit1 = new FlipCounter($('#digit1'));

	//tenhour = new FlipCounter($('#tenhour'));
	//hour = new FlipCounter($('#hour'));
	//tenmin = new FlipCounter($('#tenmin'));
	//min = new FlipCounter($('#min'));
	//tensec = new FlipCounter($('#tensec'));
	//sec = new FlipCounter($('#sec'));

	//setTime(true);
	//setInterval(function () {
	//	setTime(false);
	//}, 1000);
	//setTimeout(function () { map.invalidateSize() }, 4);

}
init();

var injectionCount = 0;
var dataSet = []
var renderPage = function (data) {
	var m = moment(data.creationDate, 'YYYY/M/D HH:mm:ss');
	data.date = m.format('jYYYY/jM/jD');
	data.time = m.format('HH:mm:ss');
	//Markers
	addMarker(data);

	//Table
	//var cuurentPage = $datatable.page.info().page;
	//dataSet.unshift(data);
	//if (dataSet.length > 100) {
	//	dataSet.pop();
	//	$datatable.row($("#covidVaccinationLiveTable tbody tr").eq(-1))
	//		.remove()
	//		.draw();
	//}

	//let resultContent = '';
	//if (data.result == 0) {
	//	resultContent = `<i style="color:green;font-size: 14px;font-weight: 800;"  class="fa fa-check" aria-hidden="true"></i>`
	//} else {
	//	resultContent = `<i style="color:red;font-size: 14px;font-weight: 800;"  class="fa fa-remove" aria-hidden="true"></i>`
	//}
	//let injectionTimeTemplate = `<div style="display:flex;justify-content:space-around"><div>`
	//if (data.injectionTime == 1) {
	//	injectionTimeTemplate += `<img class="injectionTimeIcon" style="margin-left: 10px;" src="/images/filled-vial.png">`
	//	injectionTimeTemplate += `<img class="injectionTimeIcon" src="/images/empty-vial.png">`
	//} else if (data.injectionTime == 2) {
	//	injectionTimeTemplate += `<img class="injectionTimeIcon" style="margin-left: 10px;" src="/images/filled-vial.png">`
	//	injectionTimeTemplate += `<img class="injectionTimeIcon" src="/images/filled-vial.png">`
	//}
	//injectionTimeTemplate += `</div></div>`;

	//let datetimeTemplate = `<div style="display:flex;flex-direction:column;justify-content:center;align-items:center"><span>${data.date}</span><span>${data.time}</span></div>`;

	//var row = $(`<tr data-id="${data.id}" data-lat="${data.latitude}" data-lng="${data.longitude}" style="cursor:pointer">`)
	//	.append(`<td>${data.id}</td>`)
	//	.append(`<td>${resultContent}</td>`)
	//	.append(`<td>${datetimeTemplate}</td>`)
	//	.append(`<td>${data.brand ? data.brand : 'نامشخص'}</td>`)
	//	.append(`<td>${data.batchCode ? data.batchCode : 'نامشخص'}</td>`)
	//	.append(`<td>${data.universityName ? data.universityName : 'نامشخص'}</td>`)
	//	.append(`<td>${data.province ? data.province : 'نامشخص'}</td>`)
	//	.append(`<td>${data.county ? data.county : 'نامشخص'}</td>`)
	//	.append(`<td>${data.city ? data.city : 'نامشخص'}</td>`)
	//	.append(`<td>${data.pharmacyName ? data.pharmacyName : 'نامشخص'}</td>`)
	//	.append(`<td>${injectionTimeTemplate}</td>`)
	//	.append(`<td style="direction:ltr">${data.patientNationalCode ? data.patientNationalCode : 'نامشخص'}</td>`)
	//	.append(`<td>${data.textResult ? data.textResult : 'نامشخص'}</td>`);
	//row.on("click", function () {
	//	stopMap();
	//	var $el = $(this);
	//	let lat = $el.data('lat');
	//	let lng = $el.data('lng');
	//	$('.liveMapLegend').addClass('stopped');
	//	flyflag = false;
	//	setTimeout(function () { map.flyTo([lat, lng], 16); }, 500);
	//})
	//$datatable.row.add(row);
	//$('#covidVaccinationLiveTable tbody').prepend(row);

	//if (dataSet.length == 1) {
	//	$datatable.draw();
	//}

	//$datatable.order([0, 'desc']).draw();
	//$datatable.page(cuurentPage).draw(false)
	//injectionCount++;
	//if (injectionCount == 10) {
	//	$("#covidVaccinationInjectionCounter").append(`
	//           <div class="flipper" id="digit2"></div>`
	//	);
	//	digit2 = new FlipCounter($('#digit2'));
	//} else if (injectionCount == 100) {
	//	$("#covidVaccinationInjectionCounter").append(`
	//           <div class="flipper" id="digit3"></div>`
	//	);
	//	digit3 = new FlipCounter($('#digit3'));
	//} else if (injectionCount == 1000) {
	//	$("#covidVaccinationInjectionCounter").append(`
	//           <div class="flipper" id="digit4"></div>`
	//	);
	//	digit4 = new FlipCounter($('#digit4'));
	//} else if (injectionCount == 10000) {
	//	$("#covidVaccinationInjectionCounter").append(`
	//           <div class="flipper" id="digit5"></div>`
	//	);
	//	digit5 = new FlipCounter($('#digit5'));
	//}
	//digit1.increment();
	//digit1value++;
	//if (digit1value == 10) {
	//	digit1value = 0;
	//	digit2.increment();
	//	digit2value++;
	//	if (digit2value == 10) {
	//		digit2value = 0;
	//		digit3.increment();
	//		digit3value++;
	//		if (digit3value == 10) {
	//			digit3value = 0;
	//			digit4.increment();
	//			digit4value++;
	//			if (digit4value == 10) {
	//				digit4value = 0;
	//				digit5.increment();
	//				digit5value++;
	//			}
	//		}

	//	}
	//}
}

var tableLevel = 1;
var exapndTableBtn = $(".exapnd-table");
var collapseTableBtn = $(".collapse-table");
function setTableLevel(dt) {

	if (dt == -1) {
		if (tableLevel == 2) {
			$(".table-wrapper").toggleClass('tableExpand');
			$(".exapnd-table").show();
			setTimeout(function () {
				$datatable.order([0, 'desc']).draw();
			}, 300);
			tableLevel--;
		} else if (tableLevel == 1) {
			$(".table-wrapper").toggleClass('tableCollapse');
			$(".collapse-table").hide();
			setTimeout(function () {
				$datatable.order([0, 'desc']).draw();
			}, 300);
			tableLevel--;
		}
	} else if (dt == 1) {
		if (tableLevel == 1) {
			$(".table-wrapper").toggleClass('tableExpand');
			$(".exapnd-table").hide();

			setTimeout(function () {
				$datatable.order([0, 'desc']).draw();
			}, 300);
			tableLevel++;
		} else if (tableLevel == 0) {
			$(".table-wrapper").toggleClass('tableCollapse');
			$(".collapse-table").show();
			setTimeout(function () {
				$datatable.order([0, 'desc']).draw();
			}, 300);
			tableLevel++;
		}
	}
}
