(() => {
    document.addEventListener('readystatechange', function () {
        var checkElem = document.getElementById('chkBox');
        var checkText = document.getElementById('shippingInfo');

        checkElem.addEventListener('change', function () {
            if (checkElem.checked == true) {
                document.getElementById('additionalData').style.display = 'none';
                checkText.classList.value = 'text-secondary';
                checkText.innerHTML = "Shipping address same as billing"
            }
            else {
                document.getElementById('additionalData').style.display = 'block';
                checkText.classList.value = 'text-info';
                checkText.innerHTML = "Shipping address NOT same as billing"
            }
        });
    });
})();