var slideIndex = 1;


try {
	showDivs(slideIndex);
} catch (error) {
	console.error(error);
	// expected output: ReferenceError: nonExistentFunction is not defined
	// Note - error messages will vary depending on browser
}
check(2);

function plusDivs(n) {
	showDivs(slideIndex += n);
}
function check(n) {


	if (n != 1) {

		var x = document.getElementsByClassName("Views");
		for (i = 0; i < x.length; i++) {
			x[i].style.display = 'none';
		}
		var y = document.getElementsByClassName("star");
		for (i = 0; i < x.length; i++) {
			y[i].style.display = 'block';
		}


	} else {
		var x = document.getElementsByClassName("Views");
		for (i = 0; i < x.length; i++) {
			x[i].style.display = 'Block';
		}
		var y = document.getElementsByClassName("star");
		for (i = 0; i < x.length; i++) {
			y[i].style.display = 'none';
		}
	}

	if (n == 2) {
		var x = document.getElementsByClassName("Likesx");
		for (i = 0; i < x.length; i++) {
			x[i].style.display = 'Block';
		}
	} else {
		var x = document.getElementsByClassName("Likesx");
		for (i = 0; i < x.length; i++) {
			x[i].style.display = 'none';
		}
	}


	var x = document.getElementsByClassName("Itemsd");
	for (i = 0; i < x.length; i++) {
		x[i].style.background = "#FFFFFF";
		x[i].style.color = "black";

	}
	try {
		x[n].style.background = "#179685";
		x[n].style.color = "#FFFFFF";
	} catch (error) {

		// expected output: ReferenceError: nonExistentFunction is not defined
		// Note - error messages will vary depending on browser
	}




}
function showDivs(n) {
	if (n > 0) {

		var i;
		var x = document.getElementsByClassName("mySlides");
		if (slideIndex > 1 & x.length - slideIndex < 4) {

			console.log(slideIndex.toString());
			slideIndex = slideIndex - 1;
			return;
		}
		if (n > x.length) { slideIndex = 1 }
		if (n < 1) { slideIndex = x.length }
		for (i = 0; i < x.length; i++) {
			x[i].style.display = "none";
		}

		for (i = slideIndex - 1; i < slideIndex + 4; i++) {
			x[i].style.display = "block";
		}
	} else {
		slideIndex = slideIndex + 1;

	}
}



function setstar(id, number) {
	id = id + 1;
	var name = "div.star" + id + " img";
	var x = document.querySelectorAll(name);


	for (var i = 0; i < x.length; i++) {

		x[i].src = "/images/Vector (2).png";
	}
	for (var i = 0; i < number; i++) {

		x[i].src = "/images/Vector (1).png";
	}

}