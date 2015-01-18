function hide(id) {
    hideElement(document.getElementById(id));
}

function hideElement(element) {
    if (element != null) {
        if (element.style.display != "none") {
            element.style.display = "none";
        }
        else {
            element.style.display = "inherit";
        }
    }
}

function changeImage(element) {
    if (element != null) {
        if (element.src.indexOf("hide.png") >= 0) {
            element.src = element.src.replace("hide.png", "show.png");
        }
        else {
            element.src = element.src.replace("show.png", "hide.png");
        }
    }
}