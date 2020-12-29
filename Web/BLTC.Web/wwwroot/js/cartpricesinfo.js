(() => {
    window.onload = function () {
        var itemsInfo = document.querySelectorAll('#itemInfo');

        var subTotalPrice = document.getElementById('subTotal');
        var subPriceSpan = subTotalPrice.querySelector('span');

        var taxesPrice = document.getElementById('taxes');
        var taxesSpan = taxesPrice.querySelector('span');

        var totalPrice = document.getElementById('totalPrice');
        var totalSpan = totalPrice.querySelector('span');

        var total = 0.0;
        for (var i = 0; i < itemsInfo.length; i++) {
            var quantity = itemsInfo[i].querySelector('#amount'); // 3
            var fullPrice = itemsInfo[i].querySelector('#fullPrice');
            var currentPrice = parseFloat(itemsInfo[i].querySelector('#currentPrice').innerText) * parseFloat(quantity.innerText); // 66.50 * 3 = 198.50

            fullPrice.innerText = currentPrice.toFixed(2) + '\u20ac';

            total += parseFloat(currentPrice);
        }

        taxesSpan.innerText = (total * 0.2).toFixed(2);
        subPriceSpan.innerText = (total - parseFloat(taxesSpan.innerText)).toFixed(2);
        totalSpan.innerText = total.toFixed(2);
    };
})();