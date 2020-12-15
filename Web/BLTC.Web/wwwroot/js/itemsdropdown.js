(() => {
    window.addEventListener('click', function () {
        if (!event.target.matches('.dropbtn')) {

            var dropDown = document.getElementsByClassName("dropdown-content");
            var i;
            for (i = 0; i < dropDown.length; i++) {
                var openSharedown = dropDown[i];
                if (openSharedown.classList.contains('show')) {
                    openSharedown.classList.remove('show');
                }
            }
        }
    });

    var btn = document.getElementById('dropdownBtn');
    btn.addEventListener('click', function () {
        document.getElementById("myDropdown").classList.toggle("show");
    });

    var input = document.getElementById('myInput');
    input.addEventListener('keyup', filterFunction);

    function filterFunction() {
        var input, filter, ul, li, a, i;
        input = document.getElementById("myInput");
        filter = input.value.toUpperCase();
        div = document.getElementById("myDropdown");
        a = div.getElementsByTagName("a");
        for (i = 0; i < a.length; i++) {
            txtValue = a[i].textContent || a[i].innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                a[i].style.display = "";
            } else {
                a[i].style.display = "none";
            }
        }
    }
})();