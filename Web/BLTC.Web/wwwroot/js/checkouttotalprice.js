(() => {
    document.getElementById('checkoutBtn').addEventListener('click', function () {
        var totalPrice = document.getElementById('TotalPrice');
        var price = document.querySelector('#finalprice');
        totalPrice.value = parseFloat(price.innerText).toFixed(2);
    });
})();